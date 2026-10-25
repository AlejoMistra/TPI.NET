using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace WebAPI
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/reportes")
                .WithTags("Reportes")
                .WithOpenApi();

            // ==========================================
            // Reporte 1: Tablero de Turnos
            // ==========================================

            // GET: /reportes/turnos/datos
            group.MapGet("/turnos/datos", async (
                [FromQuery] DateOnly desde,
                [FromQuery] DateOnly hasta,
                [FromQuery] int? profesionalId,
                [FromQuery] int? especialidadId,
                ClaimsPrincipal user,
                IReporteService reporteService) =>
            {
                var filtro = new ReporteTurnosFiltro(desde, hasta, profesionalId, especialidadId);
                int? profesionalAutenticadoId = ObtenerProfesionalIdSiAplica(user);

                var datos = await reporteService.ObtenerDatosTurnosAsync(filtro, profesionalAutenticadoId);
                return Results.Ok(datos);
            })
            .WithName("GetReporteTurnosDatos")
            .Produces<ReporteTurnosDatosDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .RequireAuthorization("ReportesTurnosLeer");

            // GET: /reportes/turnos/pdf
            group.MapGet("/turnos/pdf", async (
                [FromQuery] DateOnly desde,
                [FromQuery] DateOnly hasta,
                [FromQuery] int? profesionalId,
                [FromQuery] int? especialidadId,
                ClaimsPrincipal user,
                IReporteService reporteService) =>
            {
                var filtro = new ReporteTurnosFiltro(desde, hasta, profesionalId, especialidadId);
                int? profesionalAutenticadoId = ObtenerProfesionalIdSiAplica(user);

                var pdfBytes = await reporteService.GenerarReporteTurnosPdfAsync(filtro, profesionalAutenticadoId);
                string fileName = $"turnos_{filtro.Desde:yyyy-MM-dd}_{filtro.Hasta:yyyy-MM-dd}.pdf";

                return Results.File(pdfBytes, "application/pdf", fileName);
            })
            .WithName("GetReporteTurnosPdf")
            .Produces(StatusCodes.Status200OK, contentType: "application/pdf")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .RequireAuthorization("ReportesTurnosLeer");

            // ==========================================
            // Reporte 2: Facturación
            // ==========================================

            // GET: /reportes/facturacion/datos
            group.MapGet("/facturacion/datos", async (
                [FromQuery] DateOnly desde,
                [FromQuery] DateOnly hasta,
                [FromQuery] int? profesionalId,
                [FromQuery] EstadoFacturacion? estado,
                ClaimsPrincipal user,
                IReporteService reporteService) =>
            {
                var filtro = new ReporteFacturacionFiltro(desde, hasta, profesionalId, estado ?? EstadoFacturacion.Todos);
                int? profesionalAutenticadoId = ObtenerProfesionalIdSiAplica(user);

                var datos = await reporteService.ObtenerDatosFacturacionAsync(filtro, profesionalAutenticadoId);
                return Results.Ok(datos);
            })
            .WithName("GetReporteFacturacionDatos")
            .Produces<FacturacionReporteDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .RequireAuthorization("ReportesFacturacionLeer");

            // GET: /reportes/facturacion/pdf
            group.MapGet("/facturacion/pdf", async (
                [FromQuery] DateOnly desde,
                [FromQuery] DateOnly hasta,
                [FromQuery] int? profesionalId,
                [FromQuery] EstadoFacturacion? estado,
                ClaimsPrincipal user,
                IReporteService reporteService) =>
            {
                var filtro = new ReporteFacturacionFiltro(desde, hasta, profesionalId, estado ?? EstadoFacturacion.Todos);
                int? profesionalAutenticadoId = ObtenerProfesionalIdSiAplica(user);

                var pdfBytes = await reporteService.GenerarReporteFacturacionPdfAsync(filtro, profesionalAutenticadoId);
                string fileName = $"facturacion_{filtro.Desde:yyyy-MM-dd}_{filtro.Hasta:yyyy-MM-dd}.pdf";

                return Results.File(pdfBytes, "application/pdf", fileName);
            })
            .WithName("GetReporteFacturacionPdf")
            .Produces(StatusCodes.Status200OK, contentType: "application/pdf")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .RequireAuthorization("ReportesFacturacionLeer");
        }

        private static int? ObtenerProfesionalIdSiAplica(ClaimsPrincipal user)
        {
            if (user.IsInRole("Profesional"))
            {
                var personaClaim = user.FindFirstValue("PersonaId");
                if (int.TryParse(personaClaim, out var personaId))
                {
                    return personaId;
                }
            }

            return null;
        }
    }
}

