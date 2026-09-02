using Domain.Model;

namespace Data
{
  public interface IPacienteRepository
  {
    Task AddAsync(Paciente paciente);
    Task<bool> UpdateAsync(Paciente paciente);
    Task<Paciente?> GetByIdAsync(int id);
    Task<IEnumerable<Paciente>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<bool> EmailExistsAsync(string email, int? excludeId = null);
  }
}