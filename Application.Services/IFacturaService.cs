using DTOs;

namespace Application.Services
{
    public interface IFacturaService
    {
        Task<IEnumerable<FacturaDTO>> GetAllAsync();
        Task<FacturaDTO?> GetByIdAsync(int id);
        Task<FacturaDTO?> GetByTurnoIdAsync(int turnoId);
        Task<FacturaDTO> CreateAsync(FacturaCreateDTO dto);
        Task<FacturaDTO> AnularAsync(int id);
    }
}
