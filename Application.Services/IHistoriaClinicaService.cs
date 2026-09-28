using DTOs;

namespace Application.Services
{
    public interface IHistoriaClinicaService
    {
        Task<HistoriaClinicaDTO?> GetByIdAsync(int id);
        Task<HistoriaClinicaDTO?> GetByPacienteIdAsync(int pacienteId);
        Task<HistoriaClinicaDTO> CreateAsync(HistoriaClinicaCreateDTO dto);
        Task<HistoriaClinicaDTO?> UpdateGrupoSanguineoAsync(int id, HistoriaClinicaUpdateGrupoDTO dto);
        Task<RegistroClinicoDTO> AddRegistroAsync(int historiaClinicaId, RegistroClinicoCreateDTO dto);
    }
}

