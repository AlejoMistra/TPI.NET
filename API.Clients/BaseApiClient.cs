using System.Net;
using System.Net.Http.Headers;

namespace API.Clients
{
    public abstract class BaseApiClient
    {
        // Un solo HttpClient para toda la vida de la app: crear uno por request agota los
        // sockets TCP disponibles (quedan en TIME_WAIT) y termina colgando las siguientes llamadas.
        private static readonly Lazy<HttpClient> _sharedClient = new(CreateSharedClient);

        private static HttpClient CreateSharedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(GetBaseUrlFromConfig()),
                Timeout = TimeSpan.FromSeconds(30)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        protected static async Task<HttpClient> CreateHttpClientAsync()
        {
            var client = _sharedClient.Value;

            // Refrescar el Bearer token en cada uso porque el cliente es compartido/de larga vida
            await AddAuthorizationHeaderAsync(client);
            return client;
        }

        private static string GetBaseUrlFromConfig()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Intentando leer configuración...");

                // 1. Primero revisar variable de entorno
                string? envUrl = Environment.GetEnvironmentVariable("TPI_API_BASE_URL");
                if (!string.IsNullOrEmpty(envUrl))
                {
                    System.Diagnostics.Debug.WriteLine($"[DEBUG] URL desde variable de entorno: {envUrl}");
                    return envUrl;
                }

                // 2. Detectar si estamos en Android por el runtime
                string runtimeInfo = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier;
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Runtime: {runtimeInfo}");

                if (runtimeInfo.StartsWith("android"))
                {
                    System.Diagnostics.Debug.WriteLine($"[DEBUG] Detectado Android - usando IP de emulador");
                    return "http://10.0.2.2:5054/";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Error detectando plataforma: {ex.Message}");
            }

            // URL por defecto para Windows/otras plataformas
            string defaultUrl = "http://localhost:5054/";
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Usando URL por defecto: {defaultUrl}");
            return defaultUrl;
        }

        protected static async Task AddAuthorizationHeaderAsync(HttpClient client)
        {
            var authService = AuthServiceProvider.Instance;

            // Verificar expiración antes de usar el token
            await authService.CheckTokenExpirationAsync();

            var token = await authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        protected static async Task EnsureAuthenticatedAsync()
        {
            var authService = AuthServiceProvider.Instance;

            // Verificar expiración primero
            await authService.CheckTokenExpirationAsync();

            if (!await authService.IsAuthenticatedAsync())
            {
                throw new UnauthorizedAccessException("Su sesión ha expirado.");
            }
        }

        protected static async Task HandleUnauthorizedResponseAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Limpiar sesión actual
                var authService = AuthServiceProvider.Instance;
                await authService.LogoutAsync();

                // Lanzar excepción con mensaje simple
                throw new UnauthorizedAccessException("Su sesión ha expirado.");
            }
        }
    }
}
