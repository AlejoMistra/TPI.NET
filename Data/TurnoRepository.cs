using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
  public class TurnoRepository : ITurnoRepository
  {
    private readonly TPIContext _context;

    public TurnoRepository(TPIContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Turno>> GetAllAsync()
    {
      return await _context.Turnos.ToListAsync();
    }

    public async Task<Turno?> GetByIdAsync(int id)
    {
      return await _context.Turnos.FindAsync(id);
    }

    public async Task AddAsync(Turno turno)
    {
      await _context.Turnos.AddAsync(turno);
      await _context.SaveChangesAsync();
    }

    public async Task<Turno?> UpdateAsync(Turno turno)
    {
      var existingTurno = await _context.Turnos.FindAsync(turno.Id);
      if (existingTurno == null)
      {
        return null;
      }
      _context.Entry(existingTurno).CurrentValues.SetValues(turno);
      await _context.SaveChangesAsync();
      return existingTurno;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var turno = await _context.Turnos.FindAsync(id);
      if (turno == null)
      {
        return false;
      }
      _context.Turnos.Remove(turno);
      await _context.SaveChangesAsync();
      return true;
    }

    // Verifica si existe algún turno que se superponga con el horario indicado para un profesional, excluyendo un turno específico si se proporciona (para update el mismo turno se excluye a sí mismo).
    public async Task<bool> ExisteSuperposicionAsync(int profesionalId, DateTime fechaHoraInicio, DateTime fechaHoraFin, int? excludeTurnoId = null)
    {
      return await _context.Turnos.AnyAsync(t =>
          t.ProfesionalId == profesionalId &&
          t.EstadoTurno != Turno.EstadosTurno.Cancelado &&
          (excludeTurnoId == null || t.Id != excludeTurnoId) &&
          t.FechaHoraInicio < fechaHoraFin &&
          t.FechaHoraFin > fechaHoraInicio
      );
    }
  }
}