using Domain.Model;

namespace Data
{
  public interface ITurnoRepository
  {
    Task<IEnumerable<Turno>> GetAllAsync();
    Task<Turno?> GetByIdAsync(int id);
    Task AddAsync(Turno turno);
    Task<Turno?> UpdateAsync(Turno turno);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExisteSuperposicionAsync(int profesionalId, DateTime fechaHoraInicio, DateTime fechaHoraFin, int? excludeTurnoId = null);
  }
}