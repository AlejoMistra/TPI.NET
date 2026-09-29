using DTOs;

namespace API.Clients
{
  public class TurnoApiClient : BaseApiClient
  {
    public static Task<IEnumerable<TurnoDTO>> GetAllAsync()
        => SendGetAsync<IEnumerable<TurnoDTO>>("turnos");

    public static Task<TurnoDTO> GetAsync(int id)
        => SendGetAsync<TurnoDTO>($"turnos/{id}");

    public static Task<TurnoDTO> AddAsync(TurnoDTO turno)
        => SendPostAsync<TurnoDTO, TurnoDTO>("turnos", turno);

    public static Task<TurnoDTO> UpdateAsync(TurnoDTO turno)
        => SendPutAsync<TurnoDTO, TurnoDTO>($"turnos/{turno.Id}", turno);

    public static Task DeleteAsync(int id)
        => SendDeleteAsync($"turnos/{id}");
  }
}