using DTOs;

namespace API.Clients
{
    public class PacienteApiClient : BaseApiClient
    {
        public static Task<IEnumerable<PacienteDTO>> GetAllAsync()
            => SendGetAsync<IEnumerable<PacienteDTO>>("pacientes");

        public static Task<PacienteDTO?> GetAsync(int id)
            => SendGetOrDefaultAsync<PacienteDTO>($"pacientes/{id}");

        public static Task<PacienteDTO> AddAsync(PacienteDTO paciente)
            => SendPostAsync<PacienteDTO, PacienteDTO>("pacientes", paciente);

        public static Task<PacienteDTO> UpdateAsync(PacienteDTO paciente)
            => SendPutAsync<PacienteDTO, PacienteDTO>($"pacientes/{paciente.Id}", paciente);

        public static Task DeleteAsync(int id)
            => SendDeleteAsync($"pacientes/{id}");
    }
}
