using DTOs;
namespace API.Clients
{
  public class ProfesionalApiClient : BaseApiClient
  {
    public static async Task<ProfesionalDTO> GetAsync(int id)
    {
      try
      {
        using var client = await CreateHttpClientAsync();
        HttpResponseMessage response = await client.GetAsync($"profesionales/{id}");

        if (response.IsSuccessStatusCode)
        {
          return await response.Content.ReadAsAsync<ProfesionalDTO>();
        }
        else
        {
          string errorContent = await response.Content.ReadAsStringAsync();
          throw new Exception($"Error al obtener profesional con id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
        }
      }
      catch (HttpRequestException ex)
      {
        throw new Exception($"Error de conexión al obtener profesional con id {id}: {ex.Message}", ex);
      }
      catch (TaskCanceledException ex)
      {
        throw new Exception($"La solicitud para obtener profesional con id {id} fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
      }
    }

    public static async Task<IEnumerable<ProfesionalDTO>> GetAllAsync()
    {
      try
      {
        using var client = await CreateHttpClientAsync();
        HttpResponseMessage response = await client.GetAsync("profesionales");

        if (response.IsSuccessStatusCode)
        {
          return await response.Content.ReadAsAsync<IEnumerable<ProfesionalDTO>>();
        }
        else
        {
          string errorContent = await response.Content.ReadAsStringAsync();
          throw new Exception($"Error al obtener la lista de profesionales. Status: {response.StatusCode}, Detalle: {errorContent}");
        }
      }
      catch (HttpRequestException ex)
      {
        throw new Exception($"Error de conexión al obtener la lista de profesionales: {ex.Message}", ex);
      }
      catch (TaskCanceledException ex)
      {
        throw new Exception($"La solicitud para obtener la lista de profesionales fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
      }
    }

    public static async Task AddAsync(ProfesionalDTO profesional)
    {
      try
      {
        using var client = await CreateHttpClientAsync();
        HttpResponseMessage response = await client.PostAsJsonAsync("profesionales", profesional);

        if (!response.IsSuccessStatusCode)
        {
          string errorContent = await response.Content.ReadAsStringAsync();
          throw new Exception($"Error al agregar profesional. Status: {response.StatusCode}, Detalle: {errorContent}");
        }
      }
      catch (HttpRequestException ex)
      {
        throw new Exception($"Error de conexión al agregar profesional: {ex.Message}", ex);
      }
      catch (TaskCanceledException ex)
      {
        throw new Exception($"La solicitud para agregar profesional fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
      }
    }

    public static async Task DeleteAsync(int id)
    {
      try
      {
        using var client = await CreateHttpClientAsync();
        HttpResponseMessage response = await client.DeleteAsync($"profesionales/{id}");

        if (!response.IsSuccessStatusCode)
        {
          string errorContent = await response.Content.ReadAsStringAsync();
          throw new Exception($"Error al eliminar profesional con id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
        }
      }
      catch (HttpRequestException ex)
      {
        throw new Exception($"Error de conexión al eliminar profesional con id {id}: {ex.Message}", ex);
      }
      catch (TaskCanceledException ex)
      {
        throw new Exception($"La solicitud para eliminar profesional con id {id} fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
      }
    }

    public static async Task<IEnumerable<ProfesionalDTO>> GetByEspecialidadAsync(string especialidad)
    {
      try
      {
        using var client = await CreateHttpClientAsync();
        HttpResponseMessage response = await client.GetAsync($"profesionales/especialidad?especialidad={especialidad}");

        if (response.IsSuccessStatusCode)
        {
          return await response.Content.ReadAsAsync<IEnumerable<ProfesionalDTO>>();
        }
        else
        {
          string errorContent = await response.Content.ReadAsStringAsync();
          throw new Exception($"Error al obtener profesionales por especialidad. Status: {response.StatusCode}, Detalle: {errorContent}");
        }
      }
      catch (HttpRequestException ex)
      {
        throw new Exception($"Error de conexión al obtener profesionales por especialidad: {ex.Message}", ex);
      }
      catch (TaskCanceledException ex)
      {
        throw new Exception($"La solicitud para obtener profesionales por especialidad fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
      }
    }

    public static async Task UpdateAsync(ProfesionalDTO profesional)
    {
      try
      {
        using var client = await CreateHttpClientAsync();
        HttpResponseMessage response = await client.PutAsJsonAsync($"profesionales/{profesional.Id}", profesional);

        if (!response.IsSuccessStatusCode)
        {
          string errorContent = await response.Content.ReadAsStringAsync();
          throw new Exception($"Error al actualizar profesional con id {profesional.Id}. Status: {response.StatusCode}, Detalle: {errorContent}");
        }
      }
      catch (HttpRequestException ex)
      {
        throw new Exception($"Error de conexión al actualizar profesional con id {profesional.Id}: {ex.Message}", ex);
      }
      catch (TaskCanceledException ex)
      {
        throw new Exception($"La solicitud para actualizar profesional con id {profesional.Id} fue cancelada o excedió el tiempo de espera: {ex.Message}", ex);
      }
    }
  }
}