using Domain.Model;

namespace Data
{
    public interface IHistoriaClinicaRepository
    {
        Task<HistoriaClinica?> GetByIdAsync(int id, bool includeRegistros = true);
        Task<HistoriaClinica?> GetByPacienteIdAsync(int pacienteId, bool includeRegistros = true);
        Task AddAsync(HistoriaClinica historiaClinica);
        Task UpdateAsync(HistoriaClinica historiaClinica);
        Task<bool> ExistsForPacienteAsync(int pacienteId);
        Task AddRegistroClinicoAsync(RegistroClinico registro);
    }
}

