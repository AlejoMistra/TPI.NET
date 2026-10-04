using DTOs;

namespace API.Clients
{
    public class FacturaApiClient : BaseApiClient
    {
        public static Task<IEnumerable<FacturaDTO>> GetAllAsync()
            => SendGetAsync<IEnumerable<FacturaDTO>>("facturas");

        public static Task<FacturaDTO> GetAsync(int id)
            => SendGetAsync<FacturaDTO>($"facturas/{id}");

        public static Task<FacturaDTO> GetByTurnoIdAsync(int turnoId)
            => SendGetAsync<FacturaDTO>($"facturas/turno/{turnoId}");

        public static Task<FacturaDTO> CreateAsync(FacturaCreateDTO dto)
            => SendPostAsync<FacturaCreateDTO, FacturaDTO>("facturas", dto);

        public static Task<FacturaDTO> AnularAsync(int id)
            => SendPostAsync<object, FacturaDTO>($"facturas/{id}/anular", new { });
    }
}

