using DTOs;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteDatosService _datosService;
        private readonly IReportePdfService _pdfService;

        public ReporteService(IReporteDatosService datosService, IReportePdfService pdfService)
        {
            _datosService = datosService;
            _pdfService = pdfService;
        }

        public Task<ReporteTurnosDatosDto> ObtenerDatosTurnosAsync(ReporteTurnosFiltro filtro, int? personaIdProfesionalAutenticado = null)
        {
            return _datosService.ObtenerDatosTurnosAsync(filtro, personaIdProfesionalAutenticado);
        }

        public async Task<byte[]> GenerarReporteTurnosPdfAsync(ReporteTurnosFiltro filtro, int? personaIdProfesionalAutenticado = null)
        {
            var datos = await _datosService.ObtenerDatosTurnosAsync(filtro, personaIdProfesionalAutenticado);
            return _pdfService.GenerarReporteTurnosPdf(datos, filtro);
        }

        public Task<FacturacionReporteDto> ObtenerDatosFacturacionAsync(ReporteFacturacionFiltro filtro, int? personaIdProfesionalAutenticado = null)
        {
            return _datosService.ObtenerDatosFacturacionAsync(filtro, personaIdProfesionalAutenticado);
        }

        public async Task<byte[]> GenerarReporteFacturacionPdfAsync(ReporteFacturacionFiltro filtro, int? personaIdProfesionalAutenticado = null)
        {
            var datos = await _datosService.ObtenerDatosFacturacionAsync(filtro, personaIdProfesionalAutenticado);
            return _pdfService.GenerarReporteFacturacionPdf(datos, filtro);
        }
    }
}

