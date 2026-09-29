using DTOs;

namespace Application.Services
{
    public interface IPacienteService
    {
        Task<IEnumerable<PacienteDTO>> GetAllAsync();
        Task<PacienteDTO?> GetByIdAsync(int id);
        Task<PacienteDTO> AddAsync(PacienteDTO dto);
        Task<PacienteDTO?> UpdateAsync(PacienteDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
