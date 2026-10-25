using System;
using System.Collections.Generic;

namespace DTOs
{
    // ==========================================
    // Filtros de entrada
    // ==========================================
    public enum EstadoFacturacion
    {
        Todos,
        Facturado,
        Pendiente
    }

    public record ReporteTurnosFiltro(
        DateOnly Desde,
        DateOnly Hasta,
        int? ProfesionalId = null,
        int? EspecialidadId = null
    );

    public record ReporteFacturacionFiltro(
        DateOnly Desde,
        DateOnly Hasta,
        int? ProfesionalId = null,
        EstadoFacturacion Estado = EstadoFacturacion.Todos
    );

    // ==========================================
    // Reporte 1: Tablero de Turnos
    // ==========================================
    public record EstadoCantidadDto(string Estado, int Cantidad);

    public record TurnosResumenDto(
        int TotalTurnos,
        decimal PorcentajeAtendidos,
        decimal PorcentajeAusentes,
        decimal PorcentajeCancelados,
        decimal Ocupacion,
        IReadOnlyList<EstadoCantidadDto> PorEstado
    );

    public record TurnosPorProfesionalDto(
        int ProfesionalId,
        string Profesional,
        int Atendidos,
        int Ausentes,
        int Cancelados,
        int Total
    );

    public record TurnosMesDto(
        int Anio,
        int Mes,
        int Total,
        int Atendidos,
        int Ausentes,
        int Cancelados
    );

    public record TurnoDetalleDto(
        int Id,
        DateTime FechaHoraInicio,
        string Profesional,
        string Especialidad,
        string Paciente,
        string Estado,
        string? Motivo
    );

    public record ReporteTurnosDatosDto(
        TurnosResumenDto Resumen,
        IReadOnlyList<TurnosPorProfesionalDto> PorProfesional,
        IReadOnlyList<TurnosMesDto> EvolucionMensual,
        IReadOnlyList<TurnoDetalleDto> Detalle
    );

    // ==========================================
    // Reporte 2: Facturación
    // ==========================================
    public record FacturacionProfesionalDto(
        int ProfesionalId,
        string Profesional,
        string Especialidad,
        int TurnosAtendidos,
        decimal ImporteAFacturar,
        decimal Facturado,
        decimal Diferencia
    );

    public record FacturacionTotalesDto(
        int TotalTurnosAtendidos,
        decimal TotalImporteAFacturar,
        decimal TotalFacturado,
        decimal TotalDiferencia
    );

    public record FacturacionMesDto(
        int Anio,
        int Mes,
        decimal Facturado,
        decimal Pendiente
    );

    public record TurnoFacturacionPendienteDto(
        int TurnoId,
        DateTime FechaHoraInicio,
        string Profesional,
        string Paciente,
        decimal ImporteEstimado
    );

    public record FacturacionReporteDto(
        IReadOnlyList<FacturacionProfesionalDto> Filas,
        FacturacionTotalesDto Totales,
        IReadOnlyList<FacturacionMesDto> EvolucionMensual
    );
}

