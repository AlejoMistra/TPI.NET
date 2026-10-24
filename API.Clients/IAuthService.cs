namespace API.Clients
{
    public interface IAuthService
    {
        event Action<bool>? AuthenticationStateChanged;
        
        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetTokenAsync();
        Task<string?> GetUsernameAsync();
        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task CheckTokenExpirationAsync();
        Task<bool> HasPermissionAsync(string permission);
        Task<string?> GetRoleAsync();
        Task<bool> IsInRoleAsync(string role);
        Task<int?> GetPersonaIdAsync();
    }
}