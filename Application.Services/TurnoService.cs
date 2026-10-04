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
          pacienteId: turnoDto.PacienteId,
          fechaHoraLlegada: turnoDto.FechaHoraLlegada
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

      // Proteger estados: Atendido y Ausente no se pueden modificar directamente
      if (existingTurno.EstadoTurno == Turno.EstadosTurno.Atendido)
        throw new InvalidOperationException("No se puede modificar un turno que ya ha sido Atendido.");

      if (existingTurno.EstadoTurno == Turno.EstadosTurno.Ausente)
        throw new InvalidOperationException("No se puede modificar un turno en estado Ausente.");

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
          pacienteId: turnoDto.PacienteId,
          fechaHoraLlegada: turnoDto.FechaHoraLlegada ?? existingTurno.FechaHoraLlegada
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

    public async Task<TurnoDTO> AsignarAsync(int id, int pacienteId, string? motivo = null, string? observaciones = null)
    {
      if (id <= 0)
        throw new ArgumentException("El Id del turno no es válido.", nameof(id));

      if (pacienteId <= 0)
        throw new ArgumentException("El Id del paciente debe ser mayor que cero.", nameof(pacienteId));

      var turno = await _turnoRepository.GetByIdAsync(id);
      if (turno == null)
        throw new KeyNotFoundException($"No se encontró el turno con Id {id}.");

      var paciente = await _pacienteRepository.GetByIdAsync(pacienteId);
      if (paciente == null)
        throw new ArgumentException($"No se encontró el paciente con Id {pacienteId}.", nameof(pacienteId));

      turno.Asignar(pacienteId, motivo, observaciones);
      await _turnoRepository.UpdateAsync(turno);
      return MapToDTO(turno);
    }

    public async Task<TurnoDTO> LiberarAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("El Id del turno no es válido.", nameof(id));

      var turno = await _turnoRepository.GetByIdAsync(id);
      if (turno == null)
        throw new KeyNotFoundException($"No se encontró el turno con Id {id}.");

      turno.Liberar();
      await _turnoRepository.UpdateAsync(turno);
      return MapToDTO(turno);
    }

    public async Task<TurnoDTO> RegistrarLlegadaAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("El Id del turno no es válido.", nameof(id));

      var turno = await _turnoRepository.GetByIdAsync(id);
      if (turno == null)
        throw new KeyNotFoundException($"No se encontró el turno con Id {id}.");

      turno.RegistrarLlegada();
      await _turnoRepository.UpdateAsync(turno);
      return MapToDTO(turno);
    }

    public async Task<TurnoDTO> RevertirLlegadaAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("El Id del turno no es válido.", nameof(id));

      var turno = await _turnoRepository.GetByIdAsync(id);
      if (turno == null)
        throw new KeyNotFoundException($"No se encontró el turno con Id {id}.");

      turno.RevertirLlegada();
      await _turnoRepository.UpdateAsync(turno);
      return MapToDTO(turno);
    }

    public async Task<TurnoDTO> MarcarAusenteAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("El Id del turno no es válido.", nameof(id));

      var turno = await _turnoRepository.GetByIdAsync(id);
      if (turno == null)
        throw new KeyNotFoundException($"No se encontró el turno con Id {id}.");

      turno.MarcarAusente();
      await _turnoRepository.UpdateAsync(turno);
      return MapToDTO(turno);
    }

    public async Task<TurnoDTO> AtenderAsync(int id)
    {
      if (id <= 0)
        throw new ArgumentException("El Id del turno no es válido.", nameof(id));

      var turno = await _turnoRepository.GetByIdAsync(id);
      if (turno == null)
        throw new KeyNotFoundException($"No se encontró el turno con Id {id}.");

      turno.Atender();
      await _turnoRepository.UpdateAsync(turno);
      return MapToDTO(turno);
    }

    /// <summary>
    /// Valida que el estado del turno sea coherente con la presencia o ausencia de un paciente asignado.
    /// Estado Libre: PacienteId debe ser nulo.
    /// Estado Asignado, Presente, Atendido o Ausente: se exige PacienteId.
    /// </summary>
    private static void ValidarCoherenciaEstadoYPaciente(Turno.EstadosTurno estado, int? pacienteId)
    {
      if (estado == Turno.EstadosTurno.Libre)
      {
        if (pacienteId.HasValue && pacienteId.Value > 0)
          throw new ArgumentException("Un turno en estado 'Libre' no debe tener un paciente asignado.", nameof(pacienteId));
      }
      else if (estado == Turno.EstadosTurno.Asignado ||
               estado == Turno.EstadosTurno.Presente ||
               estado == Turno.EstadosTurno.Atendido ||
               estado == Turno.EstadosTurno.Ausente)
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
      PacienteId = t.PacienteId,
      FechaHoraLlegada = t.FechaHoraLlegada
    };
  }
}