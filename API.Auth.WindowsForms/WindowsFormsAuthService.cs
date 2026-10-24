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

        public async Task<bool> HasPermissionAsync(string permission)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);

                // Buscar claims de "permission"
                var permissionClaims = jsonToken.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value);

                return permissionClaims.Contains(permission);
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

                var roleClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "group" || c.Type == ClaimTypes.Role || c.Type == "role");
                return roleClaim?.Value;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsInRoleAsync(string role)
        {
            var currentRole = await GetRoleAsync();
            if (string.IsNullOrEmpty(currentRole))
                return false;

            return currentRole.Equals(role, StringComparison.OrdinalIgnoreCase);
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

                var personaClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "PersonaId");
                if (personaClaim != null && int.TryParse(personaClaim.Value, out int personaId))
                {
                    return personaId;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}