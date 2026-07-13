using DTOs;

namespace Application.Services
{
    public interface IEspecialidadService
    {
        Task<IEnumerable<EspecialidadDTO>> GetAllAsync();
    }
}
