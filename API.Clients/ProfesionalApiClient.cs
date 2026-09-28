using DTOs;

namespace API.Clients
{
    public class ProfesionalApiClient : BaseApiClient
    {
        public static Task<ProfesionalDTO> GetAsync(int id)
            => SendGetAsync<ProfesionalDTO>($"profesionales/{id}");

        public static Task<IEnumerable<ProfesionalDTO>> GetAllAsync()
            => SendGetAsync<IEnumerable<ProfesionalDTO>>("profesionales");

        public static Task AddAsync(ProfesionalDTO profesional)
            => SendPostAsync("profesionales", profesional);

        public static Task DeleteAsync(int id)
            => SendDeleteAsync($"profesionales/{id}");

        public static Task<IEnumerable<ProfesionalDTO>> GetByEspecialidadAsync(string especialidad)
            => SendGetAsync<IEnumerable<ProfesionalDTO>>($"profesionales/especialidad?especialidad={Uri.EscapeDataString(especialidad)}");

        public static Task UpdateAsync(ProfesionalDTO profesional)
            => SendPutAsync($"profesionales/{profesional.Id}", profesional);
    }
}