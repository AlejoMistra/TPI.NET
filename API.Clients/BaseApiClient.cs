using API.Clients.Exceptions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

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
            else
            {
                client.DefaultRequestHeaders.Authorization = null;
            }
        }

        protected static async Task EnsureAuthenticatedAsync()
        {
            var authService = AuthServiceProvider.Instance;

            // Verificar expiración primero
            await authService.CheckTokenExpirationAsync();

            if (!await authService.IsAuthenticatedAsync())
            {
                throw new UnauthorizedApiException("Su sesión ha expirado.");
            }
        }

        protected static async Task HandleUnauthorizedResponseAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Limpiar sesión actual
                var authService = AuthServiceProvider.Instance;
                await authService.LogoutAsync();
            }
        }

        // ==========================================
        // MÉTODOS GENÉRICOS REUTILIZABLES DE PETICIÓN
        // ==========================================

        protected static async Task<HttpResponseMessage> ExecuteRequestAsync(Func<HttpClient, Task<HttpResponseMessage>> requestFunc)
        {
            HttpResponseMessage response;
            try
            {
                var client = await CreateHttpClientAsync();
                response = await requestFunc(client);
            }
            catch (HttpRequestException ex)
            {
                throw new NetworkApiException("No se pudo conectar con el servidor. Compruebe si la API está en ejecución o su conexión de red.", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new NetworkApiException("La solicitud al servidor excedió el tiempo de espera (timeout).", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }

            return response;
        }

        protected static async Task<T> SendGetAsync<T>(string endpoint)
        {
            var response = await ExecuteRequestAsync(client => client.GetAsync(endpoint));
            var result = await response.Content.ReadFromJsonAsync<T>();
            if (result == null)
            {
                throw new ApiException($"La respuesta del servidor para '{endpoint}' fue nula o no se pudo deserializar.", response.StatusCode);
            }
            return result;
        }

        protected static async Task<T?> SendGetOrDefaultAsync<T>(string endpoint)
        {
            try
            {
                var response = await ExecuteRequestAsync(client => client.GetAsync(endpoint));
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (NotFoundApiException)
            {
                return default;
            }
        }

        protected static async Task<TResponse> SendPostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await ExecuteRequestAsync(client => client.PostAsJsonAsync(endpoint, data));
            var result = await response.Content.ReadFromJsonAsync<TResponse>();
            if (result == null)
            {
                throw new ApiException($"La respuesta del servidor para '{endpoint}' fue nula o no se pudo deserializar.", response.StatusCode);
            }
            return result;
        }

        protected static async Task SendPostAsync<TRequest>(string endpoint, TRequest data)
        {
            await ExecuteRequestAsync(client => client.PostAsJsonAsync(endpoint, data));
        }

        protected static async Task<TResponse> SendPutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await ExecuteRequestAsync(client => client.PutAsJsonAsync(endpoint, data));
            var result = await response.Content.ReadFromJsonAsync<TResponse>();
            if (result == null)
            {
                throw new ApiException($"La respuesta del servidor para '{endpoint}' fue nula o no se pudo deserializar.", response.StatusCode);
            }
            return result;
        }

        protected static async Task SendPutAsync<TRequest>(string endpoint, TRequest data)
        {
            await ExecuteRequestAsync(client => client.PutAsJsonAsync(endpoint, data));
        }

        protected static async Task SendDeleteAsync(string endpoint)
        {
            await ExecuteRequestAsync(client => client.DeleteAsync(endpoint));
        }

        // ==========================================
        // PROCESAMIENTO CENTRALIZADO DE ERRORES HTTP
        // ==========================================

        private static async Task HandleResponseErrorAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await HandleUnauthorizedResponseAsync(response);
                throw new UnauthorizedApiException();
            }

            string rawContent = await response.Content.ReadAsStringAsync();
            string? title = null;
            string? detail = null;
            IDictionary<string, string[]>? errors = null;

            if (!string.IsNullOrWhiteSpace(rawContent))
            {
                try
                {
                    using var doc = JsonDocument.Parse(rawContent);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("title", out var titleProp) && titleProp.ValueKind == JsonValueKind.String)
                    {
                        title = titleProp.GetString();
                    }

                    if (root.TryGetProperty("detail", out var detailProp) && detailProp.ValueKind == JsonValueKind.String)
                    {
                        detail = detailProp.GetString();
                    }

                    if (root.TryGetProperty("message", out var msgProp) && msgProp.ValueKind == JsonValueKind.String)
                    {
                        detail ??= msgProp.GetString();
                    }

                    if (root.TryGetProperty("error", out var errProp) && errProp.ValueKind == JsonValueKind.String)
                    {
                        detail ??= errProp.GetString();
                    }

                    if (root.TryGetProperty("errors", out var errorsProp) && errorsProp.ValueKind == JsonValueKind.Object)
                    {
                        var dict = new Dictionary<string, string[]>();
                        var errorList = new List<string>();

                        foreach (var prop in errorsProp.EnumerateObject())
                        {
                            if (prop.Value.ValueKind == JsonValueKind.Array)
                            {
                                var messages = prop.Value.EnumerateArray()
                                    .Where(x => x.ValueKind == JsonValueKind.String)
                                    .Select(x => x.GetString()!)
                                    .ToArray();
                                dict[prop.Name] = messages;
                                errorList.AddRange(messages);
                            }
                        }

                        errors = dict;
                        if (errorList.Count > 0 && string.IsNullOrWhiteSpace(detail))
                        {
                            detail = string.Join(" ", errorList);
                        }
                    }
                }
                catch
                {
                    // se mantiene rawContent como detalle
                    detail = rawContent;
                }
            }

            string cleanMessage = !string.IsNullOrWhiteSpace(detail)
                ? detail
                : (!string.IsNullOrWhiteSpace(title) ? title : $"Error HTTP {(int)response.StatusCode} ({response.ReasonPhrase})");

            throw response.StatusCode switch
            {
                HttpStatusCode.BadRequest => new ValidationApiException(cleanMessage, title, detail, errors),
                HttpStatusCode.NotFound => new NotFoundApiException(cleanMessage, title, detail),
                HttpStatusCode.Conflict => new ConflictApiException(cleanMessage, title, detail),
                HttpStatusCode.Unauthorized => new UnauthorizedApiException(cleanMessage),
                _ => new ApiException(cleanMessage, response.StatusCode, title, detail, errors)
            };
        }
    }
}
