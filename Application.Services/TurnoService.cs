using Data;
using DTOs;
using Domain.Model;

namespace Application.Services
{
  public class TurnoService : ITurnoService
  {
    private readonly ITurnoRepository _turnoRepository;
    private readonly IProfesionalRepository _profesionalRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public TurnoService(ITurnoRepository turnoRepository, IProfesionalRepository profesionalRepository, IPacienteRepository pacienteRepository)
    {
      _turnoRepository = turnoRepository;
      _profesionalRepository = profesionalRepository;
      _pacienteRepository = pacienteRepository;
    }

    public async Task<IEnumerable<TurnoDTO>> GetAllAsync()
    {
      var turnos = await _turnoRepository.GetAllAsync();
      return turnos.Select(MapToDTO).ToList();
    }

    public async Task<TurnoDTO?> GetByIdAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

      var turno = await _turnoRepository.GetByIdAsync(id);

      if (turno == null)
        return null;

      return MapToDTO(turno);
    }

    public async Task<TurnoDTO> AddAsync(TurnoDTO turnoDto)
    {
      // Valida que el estado de turno sea válido
      if (!Enum.TryParse<Turno.EstadosTurno>(turnoDto.EstadoTurno, ignoreCase: true, out var estado))
        throw new ArgumentException("Estado de turno inválido.", nameof(turnoDto.EstadoTurno));

      // Valida que se ingreso un profesional y es válido y activo
      if (turnoDto.ProfesionalId <= 0)
        throw new ArgumentException("El turno requiere un profesional válido.", nameof(turnoDto.ProfesionalId));

      var profesional = await _profesionalRepository.GetByIdAsync(turnoDto.ProfesionalId);
      if (profesional == null)
        throw new ArgumentException($"No se encontró el profesional con Id {turnoDto.ProfesionalId}.", nameof(turnoDto.ProfesionalId));

      if (profesional.Estado != Profesional.EstadoProfesional.Activo)
        throw new InvalidOperationException($"El profesional '{profesional.Apellido}, {profesional.Nombre}' está inactivo y no puede recibir turnos.");

      // Reglas de coherencia entre estado y paciente
      ValidarCoherenciaEstadoYPaciente(estado, turnoDto.PacienteId);

      // Si se ingreso un paciente, valida que sea válido
      if (turnoDto.PacienteId.HasValue && turnoDto.PacienteId.Value > 0)
      {
        var paciente = await _pacienteRepository.GetByIdAsync(turnoDto.PacienteId.Value);
        if (paciente == null)
          throw new ArgumentException($"No se encontró el paciente con Id {turnoDto.PacienteId.Value}.", nameof(turnoDto.PacienteId));
      }

      // Validacion fechas
      if (turnoDto.FechaHoraInicio >= turnoDto.FechaHoraFin)
        throw new ArgumentException("La fecha y hora de inicio debe ser anterior a la fecha y hora de fin.", nameof(turnoDto.FechaHoraInicio));

      var solapado = await _turnoRepository.ExisteSuperposicionAsync(turnoDto.ProfesionalId, turnoDto.FechaHoraInicio, turnoDto.FechaHoraFin);
      if (solapado)
        throw new ArgumentException("El profesional ya tiene un turno que se superpone con el horario ingresado.", nameof(turnoDto.FechaHoraInicio));

      Turno turno = new Turno(
          id: 0,
          fechaHoraInicio: turnoDto.FechaHoraInicio,
          fechaHoraFin: turnoDto.FechaHoraFin,
          motivo: turnoDto.Motivo,
          estadoTurno: estado,
          observaciones: turnoDto.Observaciones,
          facturaId: turnoDto.FacturaId,
          profesionalId: turnoDto.ProfesionalId,
          pacienteId: turnoDto.PacienteId
      );

      await _turnoRepository.AddAsync(turno);
      return MapToDTO(turno);
    }

    public async Task<TurnoDTO?> UpdateAsync(TurnoDTO turnoDto)
    {
      // valida que el Id del turno sea válido
      if (turnoDto.Id <= 0)
        throw new ArgumentException("El Id del turno no es válido.", nameof(turnoDto.Id));

      var existingTurno = await _turnoRepository.GetByIdAsync(turnoDto.Id);
      if (existingTurno == null)
        return null;

      // Proteger estados: Atendido y Cancelado no se pueden modificar
      if (existingTurno.EstadoTurno == Turno.EstadosTurno.Atendido)
        throw new InvalidOperationException("No se puede modificar un turno que ya ha sido Atendido.");

      if (existingTurno.EstadoTurno == Turno.EstadosTurno.Cancelado)
        throw new InvalidOperationException("No se puede modificar un turno en estado Cancelado.");

      // valida el estado del turno sea válido
      if (!Enum.TryParse<Turno.EstadosTurno>(turnoDto.EstadoTurno, ignoreCase: true, out var estado))
        throw new ArgumentException($"El estado del turno '{turnoDto.EstadoTurno}' no es válido.", nameof(turnoDto.EstadoTurno));

      // Valida que el profesional sea válido y activo
      var profesional = await _profesionalRepository.GetByIdAsync(turnoDto.ProfesionalId);
      if (profesional == null)
        throw new ArgumentException($"No se encontró el profesional con Id {turnoDto.ProfesionalId}.", nameof(turnoDto.ProfesionalId));

      if (profesional.Estado != Profesional.EstadoProfesional.Activo)
        throw new InvalidOperationException($"El profesional '{profesional.Apellido}, {profesional.Nombre}' está inactivo y no puede recibir turnos.");

      // Reglas de coherencia entre estado del turno y si tiene o no paciente
      ValidarCoherenciaEstadoYPaciente(estado, turnoDto.PacienteId);

      // Si se ingreso un paciente, valida que sea válido
      if (turnoDto.PacienteId.HasValue && turnoDto.PacienteId.Value > 0)
      {
        var paciente = await _pacienteRepository.GetByIdAsync(turnoDto.PacienteId.Value);
        if (paciente == null)
          throw new ArgumentException($"No se encontró el paciente con Id {turnoDto.PacienteId.Value}.", nameof(turnoDto.PacienteId));
      }

      // Validacion fechas
      if (turnoDto.FechaHoraInicio >= turnoDto.FechaHoraFin)
        throw new ArgumentException("La fecha y hora de inicio debe ser anterior a la fecha y hora de fin.", nameof(turnoDto.FechaHoraInicio));

      var solapado = await _turnoRepository.ExisteSuperposicionAsync(turnoDto.ProfesionalId, turnoDto.FechaHoraInicio, turnoDto.FechaHoraFin, turnoDto.Id);
      if (solapado)
        throw new ArgumentException("El profesional ya tiene un turno que se superpone con el horario ingresado.", nameof(turnoDto.FechaHoraInicio));

      Turno turno = new Turno(
          id: turnoDto.Id,
          fechaHoraInicio: turnoDto.FechaHoraInicio,
          fechaHoraFin: turnoDto.FechaHoraFin,
          motivo: turnoDto.Motivo,
          estadoTurno: estado,
          observaciones: turnoDto.Observaciones,
          facturaId: turnoDto.FacturaId,
          profesionalId: turnoDto.ProfesionalId,
          pacienteId: turnoDto.PacienteId
      );

      var updatedTurno = await _turnoRepository.UpdateAsync(turno);
      return updatedTurno is not null ? MapToDTO(updatedTurno) : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("El Id del turno no es válido.", nameof(id));

      var existingTurno = await _turnoRepository.GetByIdAsync(id);
      if (existingTurno == null)
        return false;

      if (existingTurno.EstadoTurno == Turno.EstadosTurno.Atendido)
        throw new InvalidOperationException("No se puede eliminar un turno que ya ha sido Atendido.");

      return await _turnoRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Valida que el estado del turno sea coherente con la presencia o ausencia de un paciente asignado.
    /// Estado libre; PacienteId debe ser nulo. Estado Asignado, Confirmado o Atendido; se exige PacienteId. Estado Cancelado; admite tener PacienteId (turno reservado que se canceló) o nulo(turno libre cancelado).
    /// </summary>
    private static void ValidarCoherenciaEstadoYPaciente(Turno.EstadosTurno estado, int? pacienteId)
    {
      if (estado == Turno.EstadosTurno.Libre)
      {
        if (pacienteId.HasValue && pacienteId.Value > 0)
          throw new ArgumentException("Un turno en estado 'Libre' no debe tener un paciente asignado.", nameof(pacienteId));
      }
      else if (estado == Turno.EstadosTurno.Asignado || estado == Turno.EstadosTurno.Confirmado || estado == Turno.EstadosTurno.Atendido)
      {
        if (!pacienteId.HasValue || pacienteId.Value <= 0)
          throw new ArgumentException($"Un turno en estado '{estado}' requiere un paciente asignado.", nameof(pacienteId));
      }
    }

    private static TurnoDTO MapToDTO(Turno t) => new TurnoDTO
    {
      Id = t.Id,
      FechaHoraInicio = t.FechaHoraInicio,
      FechaHoraFin = t.FechaHoraFin,
      Motivo = t.Motivo,
      EstadoTurno = t.EstadoTurno.ToString(),
      Observaciones = t.Observaciones,
      FacturaId = t.FacturaId,
      ProfesionalId = t.ProfesionalId,
      PacienteId = t.PacienteId
    };
  }
}