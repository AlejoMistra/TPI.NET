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

      // Valida que se ingreso un profesional y es válido
      if (turnoDto.ProfesionalId <= 0)
        throw new ArgumentException("El turno requiere un profesional válido.", nameof(turnoDto.ProfesionalId));

      var profesional = await _profesionalRepository.GetByIdAsync(turnoDto.ProfesionalId);
      if (profesional == null)
        throw new ArgumentException($"No se encontró el profesional con Id {turnoDto.ProfesionalId}.", nameof(turnoDto.ProfesionalId));

      // Si se ingreso un paciente, valida que sea válido
      if (turnoDto.PacienteId.HasValue)
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

      // valida el estado del turno sea válido
      if (!Enum.TryParse<Turno.EstadosTurno>(turnoDto.EstadoTurno, ignoreCase: true, out var estado))
        throw new ArgumentException($"El estado del turno '{turnoDto.EstadoTurno}' no es válido.", nameof(turnoDto.EstadoTurno));

      // Valida que el profesional sea válido
      var profesional = await _profesionalRepository.GetByIdAsync(turnoDto.ProfesionalId);
      if (profesional == null)
        throw new ArgumentException($"No se encontró el profesional con Id {turnoDto.ProfesionalId}.", nameof(turnoDto.ProfesionalId));

      // Si se ingreso un paciente, valida que sea válido
      if (turnoDto.PacienteId.HasValue)
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

      return await _turnoRepository.DeleteAsync(id);
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