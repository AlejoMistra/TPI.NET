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
                Activo = usuario.Activo,
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
                Activo = usuario.Activo,
            };
        }

        public async Task<UsuarioDTO> AddAsync(UsuarioCreateDTO createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Rol))
                throw new ArgumentException("El campo Rol es requerido.", nameof(createDto.Rol));

            var rol = ParseRol(createDto.Rol);
            var personaId = await ValidarPersonaAsync(rol, createDto.PersonaId, usuarioId: null);

            var fechaCreacion = DateTime.Now;
            Usuario usuario = new Usuario(
                0,
                createDto.Username,
                createDto.Email,
                createDto.Password,
                fechaCreacion,
                rol,
                true,
                personaId);
            usuario.SetGrupo(await ObtenerGrupoDelRolAsync(rol));

            await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Rol = usuario.Rol.ToString(),
                PersonaId = usuario.PersonaId,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo,
            };
        }

        public async Task<bool> UpdateAsync(UsuarioUpdateDTO updateDto)
        {
            var usuario = await _usuarioRepository.GetAsync(updateDto.Id);
            if (usuario == null)
                return false;

            if (string.IsNullOrWhiteSpace(updateDto.Rol))
                throw new ArgumentException("El campo Rol es requerido.", nameof(updateDto.Rol));

            var rol = ParseRol(updateDto.Rol);
            var personaId = await ValidarPersonaAsync(rol, updateDto.PersonaId, usuario.Id);

            usuario.SetUsername(updateDto.Username);
            usuario.SetEmail(updateDto.Email);
            usuario.SetActivo(updateDto.Activo);
            usuario.SetRol(rol);
            usuario.SetPersonaId(personaId);
            usuario.SetGrupo(await ObtenerGrupoDelRolAsync(rol));

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

        // Los permisos salen del grupo: cada rol usa el grupo que crea el seed (el rol Paciente no tiene)
        private async Task<GrupoPermiso?> ObtenerGrupoDelRolAsync(Usuario.Roles rol)
        {
            var nombreGrupo = rol switch
            {
                Usuario.Roles.Administrativo => "Administrador",
                Usuario.Roles.Profesional => "Profesional",
                _ => null
            };

            return nombreGrupo != null ? await _usuarioRepository.GetGrupoByNombreAsync(nombreGrupo) : null;
        }

        // Rol del usuario; si no se indica, Administrativo
        private static Usuario.Roles ParseRol(string? rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
                return Usuario.Roles.Administrativo;

            if (!Enum.TryParse<Usuario.Roles>(rol, ignoreCase: true, out var resultado) || !Enum.IsDefined(resultado))
                throw new ArgumentException($"El rol '{rol}' no es válido. Los roles disponibles son: {string.Join(", ", Enum.GetNames<Usuario.Roles>())}.", nameof(rol));

            return resultado;
        }

        // Un usuario Profesional tiene que estar vinculado a un profesional existente que no tenga otro usuario
        private async Task<int?> ValidarPersonaAsync(Usuario.Roles rol, int? personaId, int? usuarioId)
        {
            if (rol != Usuario.Roles.Profesional)
                return null;

            if (personaId is null or <= 0)
                throw new ArgumentException("El campo PersonaId es requerido y debe ser mayor a 0 para el rol Profesional.", nameof(personaId));

            if (await _profesionalRepository.GetByIdAsync(personaId.Value) == null)
                throw new ArgumentException($"No existe un profesional con el Id {personaId}.", nameof(personaId));

            var usuarios = await _usuarioRepository.GetAllAsync();
            if (usuarios.Any(u => u.PersonaId == personaId && u.Id != usuarioId))
                throw new ArgumentException("El profesional ya tiene un usuario asignado.", nameof(personaId));

            return personaId;
        }
    }
}
