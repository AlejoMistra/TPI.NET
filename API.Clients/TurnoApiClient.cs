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

        public static Task<TurnoDTO> AsignarAsync(int id, int pacienteId, string? motivo = null, string? observaciones = null)
            => SendPostAsync<AsignarTurnoRequestDTO, TurnoDTO>($"turnos/{id}/asignar", new AsignarTurnoRequestDTO { PacienteId = pacienteId, Motivo = motivo, Observaciones = observaciones });

        public static Task<TurnoDTO> LiberarAsync(int id)
            => SendPostAsync<object, TurnoDTO>($"turnos/{id}/liberar", new { });

        public static Task<TurnoDTO> RegistrarLlegadaAsync(int id)
            => SendPostAsync<object, TurnoDTO>($"turnos/{id}/llegada", new { });

        public static Task<TurnoDTO> RevertirLlegadaAsync(int id)
            => SendPostAsync<object, TurnoDTO>($"turnos/{id}/revertir-llegada", new { });

        public static Task<TurnoDTO> MarcarAusenteAsync(int id)
            => SendPostAsync<object, TurnoDTO>($"turnos/{id}/ausente", new { });

        public static Task<TurnoDTO> AtenderAsync(int id)
            => SendPostAsync<object, TurnoDTO>($"turnos/{id}/atender", new { });
    }
}