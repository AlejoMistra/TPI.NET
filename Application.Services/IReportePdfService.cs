using DTOs;

namespace Application.Services
{
    public interface IReportePdfService
    {
        byte[] GenerarReporteTurnosPdf(ReporteTurnosDatosDto datos, ReporteTurnosFiltro filtro);
        byte[] GenerarReporteFacturacionPdf(FacturacionReporteDto datos, ReporteFacturacionFiltro filtro);
    }
}

