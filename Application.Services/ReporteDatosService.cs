using Data;
using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReporteDatosService : IReporteDatosService
    {
        public const decimal ArancelReferenciaPorDefecto = 25000m;

        private readonly IReporteRepository _reporteRepository;

        public ReporteDatosService(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<ReporteTurnosDatosDto> ObtenerDatosTurnosAsync(ReporteTurnosFiltro filtro, int? profesionalIdAutenticado = null)
        {
            ValidarRangoFechas(filtro.Desde, filtro.Hasta);

            // Regla de autorización: Si el usuario es un profesional, se fuerza su propio profesionalId
            if (profesionalIdAutenticado.HasValue && profesionalIdAutenticado.Value > 0)
            {
                filtro = filtro with { ProfesionalId = profesionalIdAutenticado.Value };
            }

            var desdeDateTime = filtro.Desde.ToDateTime(TimeOnly.MinValue);
            var hastaDateTime = filtro.Hasta.ToDateTime(TimeOnly.MaxValue);

            var turnos = await _reporteRepository.GetTurnosParaReporteAsync(
                desdeDateTime,
                hastaDateTime,
                filtro.ProfesionalId,
                filtro.EspecialidadId);

            int totalAgenda = turnos.Count;
            var turnosOcupados = turnos.Where(t => t.EstadoTurno != Turno.EstadosTurno.Libre).ToList();
            int totalTurnos = turnosOcupados.Count;

            int atendidos = turnosOcupados.Count(t => t.EstadoTurno == Turno.EstadosTurno.Atendido);
            int ausentes = turnosOcupados.Count(t => t.EstadoTurno == Turno.EstadosTurno.Ausente);
            int asignados = turnosOcupados.Count(t => t.EstadoTurno == Turno.EstadosTurno.Asignado);
            int presentes = turnosOcupados.Count(t => t.EstadoTurno == Turno.EstadosTurno.Presente);

            decimal pctAtendidos = totalTurnos > 0 ? Math.Round((decimal)atendidos / totalTurnos * 100m, 2) : 0m;
            decimal pctAusentes = totalTurnos > 0 ? Math.Round((decimal)ausentes / totalTurnos * 100m, 2) : 0m;
            decimal pctCancelados = 0m;
            decimal ocupacion = totalAgenda > 0 ? Math.Round((decimal)totalTurnos / totalAgenda * 100m, 2) : 0m;

            var porEstado = new List<EstadoCantidadDto>
            {
                new("Atendido", atendidos),
                new("Ausente", ausentes),
                new("Asignado", asignados),
                new("Presente", presentes)
            };

            var resumen = new TurnosResumenDto(
                TotalTurnos: totalTurnos,
                PorcentajeAtendidos: pctAtendidos,
                PorcentajeAusentes: pctAusentes,
                PorcentajeCancelados: pctCancelados,
                Ocupacion: ocupacion,
                PorEstado: porEstado
            );

            // Agrupación por Profesional
            var porProfesional = turnosOcupados
                .GroupBy(t => t.ProfesionalId)
                .Select(g =>
                {
                    var prof = g.First().Profesional;
                    string nombreCompleto = prof != null ? $"Dr(a). {prof.Apellido}, {prof.Nombre}" : $"Profesional #{g.Key}";
                    int profAtendidos = g.Count(t => t.EstadoTurno == Turno.EstadosTurno.Atendido);
                    int profAusentes = g.Count(t => t.EstadoTurno == Turno.EstadosTurno.Ausente);
                    int profTotal = g.Count();

                    return new TurnosPorProfesionalDto(
                        ProfesionalId: g.Key,
                        Profesional: nombreCompleto,
                        Atendidos: profAtendidos,
                        Ausentes: profAusentes,
                        Cancelados: 0,
                        Total: profTotal
                    );
                })
                .OrderByDescending(p => p.Total)
                .ToList();

            // Evolución Mensual
            var evolucionMensual = turnosOcupados
                .GroupBy(t => new { t.FechaHoraInicio.Year, t.FechaHoraInicio.Month })
                .Select(g => new TurnosMesDto(
                    Anio: g.Key.Year,
                    Mes: g.Key.Month,
                    Total: g.Count(),
                    Atendidos: g.Count(t => t.EstadoTurno == Turno.EstadosTurno.Atendido),
                    Ausentes: g.Count(t => t.EstadoTurno == Turno.EstadosTurno.Ausente),
                    Cancelados: 0
                ))
                .OrderBy(m => m.Anio)
                .ThenBy(m => m.Mes)
                .ToList();

            // Detalle de turnos
            var detalle = turnosOcupados
                .Select(t => new TurnoDetalleDto(
                    Id: t.Id,
                    FechaHoraInicio: t.FechaHoraInicio,
                    Profesional: t.Profesional != null ? $"{t.Profesional.Apellido}, {t.Profesional.Nombre}" : "Sin asignar",
                    Especialidad: t.Profesional?.Especialidad?.Nombre ?? "Sin especialidad",
                    Paciente: t.Paciente != null ? $"{t.Paciente.Apellido}, {t.Paciente.Nombre}" : "Sin paciente",
                    Estado: t.EstadoTurno.ToString(),
                    Motivo: t.Motivo
                ))
                .ToList();

            return new ReporteTurnosDatosDto(
                Resumen: resumen,
                PorProfesional: porProfesional,
                EvolucionMensual: evolucionMensual,
                Detalle: detalle
            );
        }

        public async Task<FacturacionReporteDto> ObtenerDatosFacturacionAsync(ReporteFacturacionFiltro filtro, int? profesionalIdAutenticado = null)
        {
            ValidarRangoFechas(filtro.Desde, filtro.Hasta);

            if (profesionalIdAutenticado.HasValue && profesionalIdAutenticado.Value > 0)
            {
                filtro = filtro with { ProfesionalId = profesionalIdAutenticado.Value };
            }

            var desdeDateTime = filtro.Desde.ToDateTime(TimeOnly.MinValue);
            var hastaDateTime = filtro.Hasta.ToDateTime(TimeOnly.MaxValue);

            // Obtenemos los turnos del período
            var turnos = await _reporteRepository.GetTurnosParaReporteAsync(
                desdeDateTime,
                hastaDateTime,
                filtro.ProfesionalId,
                especialidadId: null);

            // Consideramos turnos en estado Atendido
            var turnosAtendidos = turnos
                .Where(t => t.EstadoTurno == Turno.EstadosTurno.Atendido)
                .ToList();

            var filasPorProfesional = turnosAtendidos
                .GroupBy(t => t.ProfesionalId)
                .Select(g =>
                {
                    var prof = g.First().Profesional;
                    string nombreProf = prof != null ? $"Dr(a). {prof.Apellido}, {prof.Nombre}" : $"Profesional #{g.Key}";
                    string especialidad = prof?.Especialidad?.Nombre ?? "General";

                    int cantAtendidos = g.Count();

                    // Facturado real: suma del MontoTotal de Facturas activas (no canceladas)
                    decimal facturadoReal = g
                        .Where(t => t.FacturaId.HasValue && t.Factura != null && t.Factura.EstadoFactura != Factura.EstadosFactura.Cancelada)
                        .Sum(t => t.Factura!.MontoTotal);

                    // Turnos atendidos pendientes de facturar
                    var turnosPendientes = g
                        .Where(t => !t.FacturaId.HasValue || t.Factura == null || t.Factura.EstadoFactura == Factura.EstadosFactura.Cancelada)
                        .ToList();

                    // Importe pendiente calculado dinámicamente según el Precio de la Especialidad (con fallback al arancel por defecto)
                    decimal importePendiente = turnosPendientes.Sum(t =>
                        (t.Profesional?.Especialidad != null && t.Profesional.Especialidad.Precio > 0)
                            ? t.Profesional.Especialidad.Precio
                            : ArancelReferenciaPorDefecto);

                    // Importe a Facturar = Facturado Real + Importe Pendiente
                    decimal importeAFacturar = facturadoReal + importePendiente;
                    decimal diferencia = importeAFacturar - facturadoReal;

                    return new FacturacionProfesionalDto(
                        ProfesionalId: g.Key,
                        Profesional: nombreProf,
                        Especialidad: especialidad,
                        TurnosAtendidos: cantAtendidos,
                        ImporteAFacturar: importeAFacturar,
                        Facturado: facturadoReal,
                        Diferencia: diferencia
                    );
                })
                .ToList();

            // Filtrado según EstadoFacturacion (Todos, Facturado, Pendiente)
            var filasFiltradas = filtro.Estado switch
            {
                EstadoFacturacion.Facturado => filasPorProfesional.Where(f => f.Facturado > 0).ToList(),
                EstadoFacturacion.Pendiente => filasPorProfesional.Where(f => f.Diferencia > 0).ToList(),
                _ => filasPorProfesional
            };

            filasFiltradas = filasFiltradas.OrderByDescending(f => f.ImporteAFacturar).ToList();

            // Totales
            var totales = new FacturacionTotalesDto(
                TotalTurnosAtendidos: filasFiltradas.Sum(f => f.TurnosAtendidos),
                TotalImporteAFacturar: filasFiltradas.Sum(f => f.ImporteAFacturar),
                TotalFacturado: filasFiltradas.Sum(f => f.Facturado),
                TotalDiferencia: filasFiltradas.Sum(f => f.Diferencia)
            );

            // Evolución Mensual de Facturación
            var evolucionMensual = turnosAtendidos
                .GroupBy(t => new { t.FechaHoraInicio.Year, t.FechaHoraInicio.Month })
                .Select(g =>
                {
                    decimal facturado = g
                        .Where(t => t.FacturaId.HasValue && t.Factura != null && t.Factura.EstadoFactura != Factura.EstadosFactura.Cancelada)
                        .Sum(t => t.Factura!.MontoTotal);

                    var turnosPendientesMes = g
                        .Where(t => !t.FacturaId.HasValue || t.Factura == null || t.Factura.EstadoFactura == Factura.EstadosFactura.Cancelada)
                        .ToList();

                    decimal pendienteMonto = turnosPendientesMes.Sum(t =>
                        (t.Profesional?.Especialidad != null && t.Profesional.Especialidad.Precio > 0)
                            ? t.Profesional.Especialidad.Precio
                            : ArancelReferenciaPorDefecto);

                    return new FacturacionMesDto(
                        Anio: g.Key.Year,
                        Mes: g.Key.Month,
                        Facturado: facturado,
                        Pendiente: pendienteMonto
                    );
                })
                .OrderBy(m => m.Anio)
                .ThenBy(m => m.Mes)
                .ToList();

            return new FacturacionReporteDto(
                Filas: filasFiltradas,
                Totales: totales,
                EvolucionMensual: evolucionMensual
            );
        }

        private static void ValidarRangoFechas(DateOnly desde, DateOnly hasta)
        {
            if (desde > hasta)
            {
                throw new ArgumentException("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.");
            }

            if (hasta.ToDateTime(TimeOnly.MinValue) > desde.ToDateTime(TimeOnly.MinValue).AddYears(2))
            {
                throw new ArgumentException("El rango de fechas no puede exceder el límite máximo de 2 años.");
            }
        }
    }
}
