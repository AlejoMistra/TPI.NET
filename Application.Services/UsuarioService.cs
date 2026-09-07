using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            return usuarios.Select(usuario => new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            });
        }

        public async Task<UsuarioDTO?> GetAsync(int id)
        {
            Usuario? usuario = await _usuarioRepository.GetAsync(id);

            if (usuario == null)
                return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            };
        }

        public async Task<UsuarioDTO> AddAsync(UsuarioCreateDTO createDto)
        {
            var fechaCreacion = DateTime.Now;
            Usuario usuario = new Usuario(0, createDto.Username, createDto.Email, createDto.Password, fechaCreacion,
                Usuario.Roles.Administrativo, true);

            await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            };
        }

        public async Task<bool> UpdateAsync(UsuarioUpdateDTO updateDto)
        {
            var usuario = await _usuarioRepository.GetAsync(updateDto.Id);
            if (usuario == null)
                return false;

            usuario.SetUsername(updateDto.Username);
            usuario.SetEmail(updateDto.Email);
            usuario.SetActivo(updateDto.Activo);

            // Solo actualizar contraseña si se proporciona
            if (!string.IsNullOrWhiteSpace(updateDto.Password))
            {
                usuario.SetPassword(updateDto.Password);
            }

            return await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _usuarioRepository.DeleteAsync(id);
        }
    }
}