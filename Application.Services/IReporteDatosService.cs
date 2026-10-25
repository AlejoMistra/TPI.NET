using DTOs;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IReporteDatosService
    {
        Task<ReporteTurnosDatosDto> ObtenerDatosTurnosAsync(ReporteTurnosFiltro filtro, int? profesionalIdAutenticado = null);
        Task<FacturacionReporteDto> ObtenerDatosFacturacionAsync(ReporteFacturacionFiltro filtro, int? profesionalIdAutenticado = null);
    }
}

