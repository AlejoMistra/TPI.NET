using DTOs;

namespace Application.Services
{
    public interface IEspecialidadService
    {
        Task<EspecialidadDTO> AddAsync(EspecialidadDTO especialidadDto);
        Task<IEnumerable<EspecialidadDTO>> GetAllAsync();
        Task<EspecialidadDTO?> GetByIdAsync(int id);
        Task<EspecialidadDTO?> UpdateAsync(EspecialidadDTO especialidadDto);
        Task<bool> DeleteAsync(int id);
    }
}
