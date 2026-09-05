using System;
using System.Threading.Tasks;
using API.Clients;

namespace WindowsForms
{
    /// <summary>
    /// TEMPORAL (solo desarrollo): stub de IAuthService para bypassear la capa
    /// de autenticación mientras otro desarrollador la implementa.
    /// Eliminar este archivo junto con la línea AuthServiceProvider.Register(...)
    /// en Program.cs cuando el login real esté integrado.
    /// </summary>
    internal class DevAuthService : IAuthService
    {
        public event Action<bool>? AuthenticationStateChanged;

        // Siempre autenticado, sin token (los endpoints abiertos no lo necesitan)
        public Task<bool> IsAuthenticatedAsync() => Task.FromResult(true);

        public Task<string?> GetTokenAsync() => Task.FromResult<string?>(null);

        public Task<string?> GetUsernameAsync() => Task.FromResult<string?>("dev-user");

        public Task<bool> LoginAsync(string username, string password) => Task.FromResult(true);

        public Task LogoutAsync() => Task.CompletedTask;

        public Task CheckTokenExpirationAsync() => Task.CompletedTask;

        // Todos los permisos concedidos en desarrollo
        public Task<bool> HasPermissionAsync(string permission) => Task.FromResult(true);
    }
}
