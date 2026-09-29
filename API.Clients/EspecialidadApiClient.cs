using DTOs;

namespace API.Clients
{
    public class EspecialidadApiClient : BaseApiClient
    {
        public static Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
            => SendGetAsync<IEnumerable<EspecialidadDTO>>("especialidades");

        public static Task<EspecialidadDTO?> GetAsync(int id)
            => SendGetOrDefaultAsync<EspecialidadDTO>($"especialidades/{id}");

        public static Task<EspecialidadDTO> AddAsync(EspecialidadDTO especialidad)
            => SendPostAsync<EspecialidadDTO, EspecialidadDTO>("especialidades", especialidad);

        public static Task<EspecialidadDTO> UpdateAsync(EspecialidadDTO especialidad)
            => SendPutAsync<EspecialidadDTO, EspecialidadDTO>($"especialidades/{especialidad.Id}", especialidad);

        public static Task DeleteAsync(int id)
            => SendDeleteAsync($"especialidades/{id}");
    }
}
