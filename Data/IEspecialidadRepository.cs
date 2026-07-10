using Model.Domain;

namespace Data
{
    public interface IEspecialidadRepository
    {
        Task<IEnumerable<Especialidad>> GetAllAsync();
        Task<Especialidad?> GetByIdAsync(int id);
        Task AddAsync(Especialidad especialidad);
        Task<Especialidad?> UpdateAsync(Especialidad especialidad);
        Task<bool> DeleteAsync(int id);
    }
}
