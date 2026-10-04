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
    Task<TurnoDTO> AsignarAsync(int id, int pacienteId, string? motivo = null, string? observaciones = null);
    Task<TurnoDTO> LiberarAsync(int id);
    Task<TurnoDTO> RegistrarLlegadaAsync(int id);
    Task<TurnoDTO> RevertirLlegadaAsync(int id);
    Task<TurnoDTO> MarcarAusenteAsync(int id);
    Task<TurnoDTO> AtenderAsync(int id);
  }
}