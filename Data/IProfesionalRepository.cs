using Domain.Model;

namespace Data
{
    public interface IProfesionalRepository
    {
        Task<IEnumerable<Profesional>> GetAllAsync();
        Task<Profesional?> GetByIdAsync(int id);
        Task AddAsync(Profesional profesional);
        Task<Profesional?> UpdateAsync(Profesional profesional);
        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsWithEspecialidadAsync(int especialidadId);
        Task<bool> MatriculaExistsAsync(string matricula, int? excludeId = null);
        Task<bool> DocumentoExistsAsync(string nroDocumento, int? excludeId = null);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
        Task<bool> HasTurnosAsync(int profesionalId);
        Task<bool> HasRegistrosClinicosAsync(int profesionalId);
    }
}
