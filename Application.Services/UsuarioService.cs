using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProfesionalRepository _profesionalRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IProfesionalRepository profesionalRepository)
        {
            _usuarioRepository = usuarioRepository;
            _profesionalRepository = profesionalRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            return usuarios.Select(usuario => new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Rol = usuario.Rol.ToString(),
                PersonaId = usuario.PersonaId,
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
                Rol = usuario.Rol.ToString(),
                PersonaId = usuario.PersonaId,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            };
        }

        public async Task<UsuarioDTO> AddAsync(UsuarioCreateDTO createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Rol))
                throw new ArgumentException("El campo Rol es requerido.", nameof(createDto.Rol));

            if (!Enum.TryParse<Usuario.Roles>(createDto.Rol, ignoreCase: true, out var rol))
                throw new ArgumentException($"El rol '{createDto.Rol}' no es válido. Los roles disponibles son: {string.Join(", ", Enum.GetNames<Usuario.Roles>())}.", nameof(createDto.Rol));

            if (rol == Usuario.Roles.Profesional)
            {
                if (!createDto.PersonaId.HasValue || createDto.PersonaId.Value <= 0)
                    throw new ArgumentException("El campo PersonaId es requerido y debe ser mayor a 0 para el rol Profesional.", nameof(createDto.PersonaId));

                var profesional = await _profesionalRepository.GetByIdAsync(createDto.PersonaId.Value);
                if (profesional == null)
                    throw new ArgumentException($"No existe un profesional con el Id {createDto.PersonaId.Value}.", nameof(createDto.PersonaId));
            }

            var fechaCreacion = DateTime.Now;
            Usuario usuario = new Usuario(
                0,
                createDto.Username,
                createDto.Email,
                createDto.Password,
                fechaCreacion,
                rol,
                true,
                createDto.PersonaId);

            await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Rol = usuario.Rol.ToString(),
                PersonaId = usuario.PersonaId,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            };
        }

        public async Task<bool> UpdateAsync(UsuarioUpdateDTO updateDto)
        {
            var usuario = await _usuarioRepository.GetAsync(updateDto.Id);
            if (usuario == null)
                return false;

            if (string.IsNullOrWhiteSpace(updateDto.Rol))
                throw new ArgumentException("El campo Rol es requerido.", nameof(updateDto.Rol));

            if (!Enum.TryParse<Usuario.Roles>(updateDto.Rol, ignoreCase: true, out var rol))
                throw new ArgumentException($"El rol '{updateDto.Rol}' no es válido. Los roles disponibles son: {string.Join(", ", Enum.GetNames<Usuario.Roles>())}.", nameof(updateDto.Rol));

            if (rol == Usuario.Roles.Profesional)
            {
                if (!updateDto.PersonaId.HasValue || updateDto.PersonaId.Value <= 0)
                    throw new ArgumentException("El campo PersonaId es requerido y debe ser mayor a 0 para el rol Profesional.", nameof(updateDto.PersonaId));

                var profesional = await _profesionalRepository.GetByIdAsync(updateDto.PersonaId.Value);
                if (profesional == null)
                    throw new ArgumentException($"No existe un profesional con el Id {updateDto.PersonaId.Value}.", nameof(updateDto.PersonaId));
            }

            usuario.SetUsername(updateDto.Username);
            usuario.SetEmail(updateDto.Email);
            usuario.SetActivo(updateDto.Activo);
            usuario.SetRol(rol);
            usuario.SetPersonaId(updateDto.PersonaId);

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