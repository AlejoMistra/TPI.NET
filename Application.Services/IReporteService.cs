using DTOs;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IReporteService
    {
        Task<ReporteTurnosDatosDto> ObtenerDatosTurnosAsync(ReporteTurnosFiltro filtro, int? personaIdProfesionalAutenticado = null);
        Task<byte[]> GenerarReporteTurnosPdfAsync(ReporteTurnosFiltro filtro, int? personaIdProfesionalAutenticado = null);

        Task<FacturacionReporteDto> ObtenerDatosFacturacionAsync(ReporteFacturacionFiltro filtro, int? personaIdProfesionalAutenticado = null);
        Task<byte[]> GenerarReporteFacturacionPdfAsync(ReporteFacturacionFiltro filtro, int? personaIdProfesionalAutenticado = null);
    }
}

