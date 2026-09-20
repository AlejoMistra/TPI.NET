using Domain.Model;
using DTOs;

namespace Application.Services
{
  public interface ITurnoService
  {
    Task<IEnumerable<TurnoDTO>> GetAllAsync();
    Task<TurnoDTO?> GetByIdAsync(int id);
    Task<TurnoDTO> AddAsync(TurnoDTO turno);
    Task<TurnoDTO?> UpdateAsync(TurnoDTO turno);
    Task<bool> DeleteAsync(int id);

  }
}