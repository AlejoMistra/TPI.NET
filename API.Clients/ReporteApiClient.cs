using DTOs;
using System.Threading.Tasks;

namespace API.Clients
{
    public class ReporteApiClient : BaseApiClient
    {
        public static Task<ReporteTurnosDatosDto> GetTurnosDatosAsync(ReporteTurnosFiltro filtro)
        {
            string url = $"reportes/turnos/datos?desde={filtro.Desde:yyyy-MM-dd}&hasta={filtro.Hasta:yyyy-MM-dd}";
            if (filtro.ProfesionalId.HasValue && filtro.ProfesionalId.Value > 0)
                url += $"&profesionalId={filtro.ProfesionalId.Value}";
            if (filtro.EspecialidadId.HasValue && filtro.EspecialidadId.Value > 0)
                url += $"&especialidadId={filtro.EspecialidadId.Value}";

            return SendGetAsync<ReporteTurnosDatosDto>(url);
        }

        public static Task<byte[]> GetTurnosPdfAsync(ReporteTurnosFiltro filtro)
        {
            string url = $"reportes/turnos/pdf?desde={filtro.Desde:yyyy-MM-dd}&hasta={filtro.Hasta:yyyy-MM-dd}";
            if (filtro.ProfesionalId.HasValue && filtro.ProfesionalId.Value > 0)
                url += $"&profesionalId={filtro.ProfesionalId.Value}";
            if (filtro.EspecialidadId.HasValue && filtro.EspecialidadId.Value > 0)
                url += $"&especialidadId={filtro.EspecialidadId.Value}";

            return SendDownloadAsync(url);
        }

        public static Task<FacturacionReporteDto> GetFacturacionDatosAsync(ReporteFacturacionFiltro filtro)
        {
            string url = $"reportes/facturacion/datos?desde={filtro.Desde:yyyy-MM-dd}&hasta={filtro.Hasta:yyyy-MM-dd}&estado={filtro.Estado}";
            if (filtro.ProfesionalId.HasValue && filtro.ProfesionalId.Value > 0)
                url += $"&profesionalId={filtro.ProfesionalId.Value}";

            return SendGetAsync<FacturacionReporteDto>(url);
        }

        public static Task<byte[]> GetFacturacionPdfAsync(ReporteFacturacionFiltro filtro)
        {
            string url = $"reportes/facturacion/pdf?desde={filtro.Desde:yyyy-MM-dd}&hasta={filtro.Hasta:yyyy-MM-dd}&estado={filtro.Estado}";
            if (filtro.ProfesionalId.HasValue && filtro.ProfesionalId.Value > 0)
                url += $"&profesionalId={filtro.ProfesionalId.Value}";

            return SendDownloadAsync(url);
        }
    }
}

