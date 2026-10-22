using DTOs;

namespace API.Clients
{
    public class UsuarioApiClient : BaseApiClient
    {
        public static Task<IEnumerable<UsuarioDTO>> GetAllAsync()
            => SendGetAsync<IEnumerable<UsuarioDTO>>("usuarios");

        public static Task<UsuarioDTO?> GetAsync(int id)
            => SendGetOrDefaultAsync<UsuarioDTO>($"usuarios/{id}");

        public static Task<UsuarioDTO> AddAsync(UsuarioCreateDTO usuario)
            => SendPostAsync<UsuarioCreateDTO, UsuarioDTO>("usuarios", usuario);

        public static Task UpdateAsync(UsuarioUpdateDTO usuario)
            => SendPutAsync("usuarios", usuario);

        public static Task DeleteAsync(int id)
            => SendDeleteAsync($"usuarios/{id}");
    }
}
