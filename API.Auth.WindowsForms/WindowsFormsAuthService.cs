using DTOs;
using API.Clients;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Auth.WindowsForms
{
    public class WindowsFormsAuthService : IAuthService
    {
        private static string? _currentToken;
        private static DateTime _tokenExpiration;
        private static string? _currentUsername;

        public event Action<bool>? AuthenticationStateChanged;

        public async Task<bool> IsAuthenticatedAsync()
        {
            return !string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow < _tokenExpiration;
        }

        public async Task<string?> GetTokenAsync()
        {
            var isAuth = await IsAuthenticatedAsync();
            return isAuth ? _currentToken : null;
        }

        public async Task<string?> GetUsernameAsync()
        {
            var isAuth = await IsAuthenticatedAsync();
            return isAuth ? _currentUsername : null;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {            
            var request = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var authClient = new AuthApiClient();
            var response = await authClient.LoginAsync(request);

            if (response != null)
            {
                _currentToken = response.Token;
                _tokenExpiration = response.ExpiresAt;
                _currentUsername = response.Username;

                AuthenticationStateChanged?.Invoke(true);
                return true;
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            _currentToken = null;
            _tokenExpiration = default;
            _currentUsername = null;

            AuthenticationStateChanged?.Invoke(false);
        }

        public async Task CheckTokenExpirationAsync()
        {
            if (!string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow >= _tokenExpiration)
            {
                await LogoutAsync();
            }
        }

        private static readonly Dictionary<string, string[]> _permissionRoles = new(StringComparer.OrdinalIgnoreCase)
        {
            // Turnos / Agenda
            { "turnos.leer", new[] { "Administrativo", "Profesional" } },
            { "TurnosLeer", new[] { "Administrativo", "Profesional" } },
            { "turnos.agregar", new[] { "Administrativo" } },
            { "TurnosAgregar", new[] { "Administrativo" } },
            { "turnos.actualizar", new[] { "Administrativo" } },
            { "TurnosActualizar", new[] { "Administrativo" } },
            { "turnos.eliminar", new[] { "Administrativo" } },
            { "TurnosEliminar", new[] { "Administrativo" } },
            { "turnos.atender", new[] { "Profesional", "Administrativo" } },
            { "TurnosAtender", new[] { "Profesional", "Administrativo" } },

            // Pacientes
            { "pacientes.leer", new[] { "Administrativo", "Profesional" } },
            { "PacientesLeer", new[] { "Administrativo", "Profesional" } },
            { "pacientes.agregar", new[] { "Administrativo" } },
            { "PacientesAgregar", new[] { "Administrativo" } },
            { "pacientes.actualizar", new[] { "Administrativo" } },
            { "PacientesActualizar", new[] { "Administrativo" } },
            { "pacientes.eliminar", new[] { "Administrativo" } },
            { "PacientesEliminar", new[] { "Administrativo" } },

            // Historias Clínicas
            { "historiasclinicas.leer", new[] { "Profesional" } },
            { "HistoriasClinicasLeer", new[] { "Profesional" } },
            { "historias.leer", new[] { "Profesional" } },
            { "historiasclinicas.agregar", new[] { "Profesional" } },
            { "HistoriasClinicasAgregar", new[] { "Profesional" } },
            { "historiasclinicas.actualizar", new[] { "Profesional" } },
            { "HistoriasClinicasActualizar", new[] { "Profesional" } },

            // Facturación
            { "facturas.leer", new[] { "Administrativo" } },
            { "FacturasLeer", new[] { "Administrativo" } },
            { "facturas.agregar", new[] { "Administrativo" } },
            { "FacturasAgregar", new[] { "Administrativo" } },
            { "facturas.actualizar", new[] { "Administrativo" } },
            { "FacturasActualizar", new[] { "Administrativo" } },

            // Datos Maestros (Especialidades, Profesionales, Usuarios)
            { "datosmaestros.leer", new[] { "Administrativo" } },
            { "especialidades.leer", new[] { "Administrativo", "Profesional" } },
            { "EspecialidadesLeer", new[] { "Administrativo", "Profesional" } },
            { "especialidades.agregar", new[] { "Administrativo" } },
            { "EspecialidadesAgregar", new[] { "Administrativo" } },
            { "especialidades.actualizar", new[] { "Administrativo" } },
            { "EspecialidadesActualizar", new[] { "Administrativo" } },
            { "especialidades.eliminar", new[] { "Administrativo" } },
            { "EspecialidadesEliminar", new[] { "Administrativo" } },

            { "profesionales.leer", new[] { "Administrativo", "Profesional" } },
            { "ProfesionalesLeer", new[] { "Administrativo", "Profesional" } },
            { "profesionales.agregar", new[] { "Administrativo" } },
            { "ProfesionalesAgregar", new[] { "Administrativo" } },
            { "profesionales.actualizar", new[] { "Administrativo" } },
            { "ProfesionalesActualizar", new[] { "Administrativo" } },
            { "profesionales.eliminar", new[] { "Administrativo" } },
            { "ProfesionalesEliminar", new[] { "Administrativo" } },

            { "usuarios.leer", new[] { "Administrativo" } },
            { "UsuariosLeer", new[] { "Administrativo" } },
            { "usuarios.agregar", new[] { "Administrativo" } },
            { "UsuariosAgregar", new[] { "Administrativo" } },
            { "usuarios.actualizar", new[] { "Administrativo" } },
            { "UsuariosActualizar", new[] { "Administrativo" } },
            { "usuarios.eliminar", new[] { "Administrativo" } },
            { "UsuariosEliminar", new[] { "Administrativo" } }
        };

        public async Task<bool> HasPermissionAsync(string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
                return false;

            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);

                // 1. Si el JWT contiene claims explícitos "permission", verificar directamente
                var permissionClaims = jsonToken.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value);

                if (permissionClaims.Contains(permission, StringComparer.OrdinalIgnoreCase))
                    return true;

                // 2. Obtener rol actual
                var role = await GetRoleAsync();
                if (string.IsNullOrEmpty(role))
                    return false;

                // 3. Chequeo directo por nombre de rol (ej. HasPermissionAsync("Administrativo"))
                if (string.Equals(role, permission, StringComparison.OrdinalIgnoreCase))
                    return true;

                // 4. Mapeo de política / permiso a roles permitidos
                if (_permissionRoles.TryGetValue(permission, out var allowedRoles))
                {
                    return allowedRoles.Any(r => string.Equals(r, role, StringComparison.OrdinalIgnoreCase));
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetRoleAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);

                var roleClaim = jsonToken.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.Role ||
                    c.Type == "role" ||
                    c.Type.EndsWith("/role", StringComparison.OrdinalIgnoreCase));

                return roleClaim?.Value;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int?> GetPersonaIdAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);

                var personaClaim = jsonToken.Claims.FirstOrDefault(c =>
                    string.Equals(c.Type, "PersonaId", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.Type, "persona_id", StringComparison.OrdinalIgnoreCase));

                if (personaClaim != null && int.TryParse(personaClaim.Value, out var personaId))
                    return personaId;

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsInRoleAsync(string role)
        {
            var userRole = await GetRoleAsync();
            return string.Equals(userRole, role, StringComparison.OrdinalIgnoreCase);
        }
    }
}