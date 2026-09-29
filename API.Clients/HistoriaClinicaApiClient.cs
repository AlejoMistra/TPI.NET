using DTOs;

namespace API.Clients
{
    public class HistoriaClinicaApiClient : BaseApiClient
    {
        public static Task<HistoriaClinicaDTO?> GetByIdAsync(int id)
            => SendGetOrDefaultAsync<HistoriaClinicaDTO>($"historias-clinicas/{id}");

        public static Task<HistoriaClinicaDTO?> GetByPacienteIdAsync(int pacienteId)
            => SendGetOrDefaultAsync<HistoriaClinicaDTO>($"historias-clinicas/paciente/{pacienteId}");

        public static Task<HistoriaClinicaDTO> CreateAsync(HistoriaClinicaCreateDTO dto)
            => SendPostAsync<HistoriaClinicaCreateDTO, HistoriaClinicaDTO>("historias-clinicas", dto);

        public static Task<HistoriaClinicaDTO> UpdateGrupoSanguineoAsync(int id, HistoriaClinicaUpdateGrupoDTO dto)
            => SendPutAsync<HistoriaClinicaUpdateGrupoDTO, HistoriaClinicaDTO>($"historias-clinicas/{id}/grupo-sanguineo", dto);

        public static Task<RegistroClinicoDTO> AddRegistroAsync(int historiaClinicaId, RegistroClinicoCreateDTO dto)
            => SendPostAsync<RegistroClinicoCreateDTO, RegistroClinicoDTO>($"historias-clinicas/{historiaClinicaId}/registros", dto);
    }
}
