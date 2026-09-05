using DTOs;
using System.Net;

namespace API.Clients
{
    public class EspecialidadApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.GetAsync("especialidades");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<EspecialidadDTO>>() ?? Enumerable.Empty<EspecialidadDTO>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener especialidades. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener especialidades: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"La solicitud para obtener especialidades fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
            }
        }

        public static async Task<EspecialidadDTO?> GetAsync(int id)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.GetAsync($"especialidades/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<EspecialidadDTO>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener especialidad con id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener especialidad con id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"La solicitud para obtener especialidad con id {id} fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
            }
        }

        public static async Task<EspecialidadDTO> AddAsync(EspecialidadDTO especialidad)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.PostAsJsonAsync("especialidades", especialidad);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<EspecialidadDTO>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al agregar especialidad. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al agregar especialidad: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"La solicitud para agregar especialidad fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
            }
        }

        public static async Task<EspecialidadDTO?> UpdateAsync(EspecialidadDTO especialidad)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.PutAsJsonAsync($"especialidades/{especialidad.Id}", especialidad);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<EspecialidadDTO>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar especialidad con id {especialidad.Id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al actualizar especialidad con id {especialidad.Id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"La solicitud para actualizar especialidad con id {especialidad.Id} fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.DeleteAsync($"especialidades/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return; // 204 No Content — success
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    // FK constraint: the especialidad has associated profesionales
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new InvalidOperationException(
                        "No se puede eliminar la especialidad porque tiene profesionales asociados.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception($"La especialidad con id {id} no fue encontrada.");
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al eliminar especialidad con id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (InvalidOperationException)
            {
                throw; // re-throw FK constraint exceptions as-is so callers can handle them specifically
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al eliminar especialidad con id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"La solicitud para eliminar especialidad con id {id} fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
            }
        }
    }
}
