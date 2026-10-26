using DTOs;
using OxyPlot;
using OxyPlot.Series;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Application.Services
{
    public class ReportePdfService : IReportePdfService
    {
        public byte[] GenerarReporteTurnosPdf(ReporteTurnosDatosDto datos, ReporteTurnosFiltro filtro)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Element(header =>
                    {
                        header.Column(col =>
                        {
                            col.Item().Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("CLÍNICA MÉDICA").FontSize(16).Bold().FontColor(Colors.Blue.Darken2);
                                    c.Item().Text("Tablero y Reporte de Gestión de Turnos").FontSize(12).SemiBold().FontColor(Colors.Grey.Darken2);
                                });

                                row.ConstantItem(180).AlignRight().Column(c =>
                                {
                                    c.Item().Text($"Emisión: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(8).FontColor(Colors.Grey.Darken1);
                                    c.Item().Text($"Período: {filtro.Desde:dd/MM/yyyy} a {filtro.Hasta:dd/MM/yyyy}").FontSize(8).FontColor(Colors.Grey.Darken1);
                                });
                            });

                            col.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                        });
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        if (datos.Resumen.TotalTurnos == 0)
                        {
                            col.Item().Padding(20).AlignCenter().Background(Colors.Grey.Lighten4).Text("Sin registros de turnos para el período y filtros seleccionados.").Italic();
                            return;
                        }

                        // Tarjetas de KPIs
                        col.Item().PaddingBottom(10).Row(row =>
                        {
                            row.Spacing(8);

                            row.RelativeItem().Element(c => RenderKpiCard(c, "Total Turnos", datos.Resumen.TotalTurnos.ToString(), Colors.Blue.Darken1));
                            row.RelativeItem().Element(c => RenderKpiCard(c, "% Atendidos", $"{datos.Resumen.PorcentajeAtendidos}%", Colors.Green.Darken1));
                            row.RelativeItem().Element(c => RenderKpiCard(c, "% Ausentes", $"{datos.Resumen.PorcentajeAusentes}%", Colors.Red.Darken1));
                            row.RelativeItem().Element(c => RenderKpiCard(c, "Ocupación", $"{datos.Resumen.Ocupacion}%", Colors.Orange.Darken1));
                        });

                        // Gráfico OxyPlot (renderizado a SVG e incrustado en el PDF)
                        string chartSvg = GenerarGraficoEstadosSvg(datos.Resumen.PorEstado);
                        if (!string.IsNullOrWhiteSpace(chartSvg))
                        {
                            col.Item().PaddingBottom(10).AlignCenter().Width(450).Height(180).Svg(chartSvg);
                        }

                        // Tabla de Detalle de Turnos
                        col.Item().PaddingBottom(5).Text("Detalle de Turnos del Período").FontSize(11).Bold().FontColor(Colors.Blue.Darken2);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(75);  // Fecha/Hora
                                columns.RelativeColumn(3);   // Profesional
                                columns.RelativeColumn(3);   // Especialidad
                                columns.RelativeColumn(3);   // Paciente
                                columns.ConstantColumn(65);  // Estado
                                columns.RelativeColumn(3);   // Motivo
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(EstiloCeldaEncabezado).Text("Fecha/Hora");
                                header.Cell().Element(EstiloCeldaEncabezado).Text("Profesional");
                                header.Cell().Element(EstiloCeldaEncabezado).Text("Especialidad");
                                header.Cell().Element(EstiloCeldaEncabezado).Text("Paciente");
                                header.Cell().Element(EstiloCeldaEncabezado).Text("Estado");
                                header.Cell().Element(EstiloCeldaEncabezado).Text("Motivo");
                            });

                            int index = 0;
                            foreach (var turno in datos.Detalle)
                            {
                                var bgColor = (index % 2 == 0) ? Colors.White : Colors.Grey.Lighten4;

                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).Text(turno.FechaHoraInicio.ToString("dd/MM/yy HH:mm"));
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).Text(turno.Profesional);
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).Text(turno.Especialidad);
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).Text(turno.Paciente);
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).Text(turno.Estado);
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).Text(turno.Motivo ?? "-");

                                index++;
                            }
                        });
                    });

                    page.Footer().Element(footer =>
                    {
                        footer.Row(row =>
                        {
                            row.RelativeItem().Text("Sistema de Gestión de Turnos - TPI").FontSize(8).FontColor(Colors.Grey.Medium);
                            row.RelativeItem().AlignRight().Text(text =>
                            {
                                text.Span("Página ").FontSize(8).FontColor(Colors.Grey.Medium);
                                text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                                text.Span(" de ").FontSize(8).FontColor(Colors.Grey.Medium);
                                text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                            });
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }

        public byte[] GenerarReporteFacturacionPdf(FacturacionReporteDto datos, ReporteFacturacionFiltro filtro)
        {
            var cultureInfo = new CultureInfo("es-AR");

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Element(header =>
                    {
                        header.Column(col =>
                        {
                            col.Item().Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("CLÍNICA MÉDICA").FontSize(16).Bold().FontColor(Colors.Blue.Darken2);
                                    c.Item().Text("Reporte de Facturación y Liquidación").FontSize(12).SemiBold().FontColor(Colors.Grey.Darken2);
                                });

                                row.ConstantItem(180).AlignRight().Column(c =>
                                {
                                    c.Item().Text($"Emisión: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(8).FontColor(Colors.Grey.Darken1);
                                    c.Item().Text($"Período: {filtro.Desde:dd/MM/yyyy} a {filtro.Hasta:dd/MM/yyyy}").FontSize(8).FontColor(Colors.Grey.Darken1);
                                    c.Item().Text($"Estado: {filtro.Estado}").FontSize(8).FontColor(Colors.Grey.Darken1);
                                });
                            });

                            col.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                        });
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        if (datos.Filas.Count == 0)
                        {
                            col.Item().Padding(20).AlignCenter().Background(Colors.Grey.Lighten4).Text("Sin registros de facturación para el período y filtros seleccionados.").Italic();
                            return;
                        }

                        // Resumen de Totales
                        col.Item().PaddingBottom(10).Row(row =>
                        {
                            row.Spacing(8);

                            row.RelativeItem().Element(c => RenderKpiCard(c, "Atendidos", datos.Totales.TotalTurnosAtendidos.ToString(), Colors.Blue.Darken1));
                            row.RelativeItem().Element(c => RenderKpiCard(c, "Total a Facturar", datos.Totales.TotalImporteAFacturar.ToString("C0", cultureInfo), Colors.Teal.Darken1));
                            row.RelativeItem().Element(c => RenderKpiCard(c, "Facturado Real", datos.Totales.TotalFacturado.ToString("C0", cultureInfo), Colors.Green.Darken1));
                            row.RelativeItem().Element(c => RenderKpiCard(c, "Diferencia Pendiente", datos.Totales.TotalDiferencia.ToString("C0", cultureInfo), Colors.Orange.Darken1));
                        });

                        // Tabla Principal agrupada por Profesional
                        col.Item().PaddingBottom(5).Text("Facturación Agrupada por Profesional").FontSize(11).Bold().FontColor(Colors.Blue.Darken2);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);   // Profesional
                                columns.RelativeColumn(2);   // Especialidad
                                columns.ConstantColumn(65);  // Turnos Atendidos
                                columns.ConstantColumn(85);  // Imp. a Facturar
                                columns.ConstantColumn(85);  // Facturado
                                columns.ConstantColumn(85);  // Diferencia
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(EstiloCeldaEncabezado).Text("Profesional");
                                header.Cell().Element(EstiloCeldaEncabezado).Text("Especialidad");
                                header.Cell().Element(EstiloCeldaEncabezado).AlignRight().Text("Atendidos");
                                header.Cell().Element(EstiloCeldaEncabezado).AlignRight().Text("A Facturar");
                                header.Cell().Element(EstiloCeldaEncabezado).AlignRight().Text("Facturado");
                                header.Cell().Element(EstiloCeldaEncabezado).AlignRight().Text("Diferencia");
                            });

                            int index = 0;
                            foreach (var fila in datos.Filas)
                            {
                                var bgColor = (index % 2 == 0) ? Colors.White : Colors.Grey.Lighten4;

                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).Text(fila.Profesional);
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).Text(fila.Especialidad);
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).AlignRight().Text(fila.TurnosAtendidos.ToString());
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).AlignRight().Text(fila.ImporteAFacturar.ToString("C0", cultureInfo));
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).AlignRight().Text(fila.Facturado.ToString("C0", cultureInfo));
                                table.Cell().Element(c => EstiloCeldaCuerpo(c, bgColor)).AlignRight().Text(fila.Diferencia.ToString("C0", cultureInfo));

                                index++;
                            }

                            // Fila de Totales
                            table.Cell().Element(EstiloCeldaTotal).Text("TOTALES");
                            table.Cell().Element(EstiloCeldaTotal).Text(string.Empty);
                            table.Cell().Element(EstiloCeldaTotal).AlignRight().Text(datos.Totales.TotalTurnosAtendidos.ToString());
                            table.Cell().Element(EstiloCeldaTotal).AlignRight().Text(datos.Totales.TotalImporteAFacturar.ToString("C0", cultureInfo));
                            table.Cell().Element(EstiloCeldaTotal).AlignRight().Text(datos.Totales.TotalFacturado.ToString("C0", cultureInfo));
                            table.Cell().Element(EstiloCeldaTotal).AlignRight().Text(datos.Totales.TotalDiferencia.ToString("C0", cultureInfo));
                        });
                    });

                    page.Footer().Element(footer =>
                    {
                        footer.Row(row =>
                        {
                            row.RelativeItem().Text("Sistema de Gestión de Turnos - TPI").FontSize(8).FontColor(Colors.Grey.Medium);
                            row.RelativeItem().AlignRight().Text(text =>
                            {
                                text.Span("Página ").FontSize(8).FontColor(Colors.Grey.Medium);
                                text.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                                text.Span(" de ").FontSize(8).FontColor(Colors.Grey.Medium);
                                text.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                            });
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }

        private static void RenderKpiCard(IContainer container, string titulo, string valor, string colorHex)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Background(Colors.Grey.Lighten5)
                .Padding(8)
                .Column(col =>
                {
                    col.Item().Text(titulo).FontSize(8).SemiBold().FontColor(Colors.Grey.Darken1);
                    col.Item().Text(valor).FontSize(14).Bold().FontColor(colorHex);
                });
        }

        private static string GenerarGraficoEstadosSvg(IReadOnlyList<EstadoCantidadDto> porEstado)
        {
            try
            {
                var plotModel = new PlotModel
                {
                    Title = "Distribución de Turnos por Estado",
                    TitleFontSize = 12,
                    Background = OxyColors.White,
                    PlotAreaBorderColor = OxyColors.Transparent
                };

                var pieSeries = new PieSeries
                {
                    StrokeThickness = 1.0,
                    InsideLabelPosition = 0.75,
                    AngleSpan = 360,
                    StartAngle = 0,
                    FontSize = 10
                };

                foreach (var estado in porEstado)
                {
                    if (estado.Cantidad > 0)
                    {
                        var slice = new PieSlice(estado.Estado, estado.Cantidad)
                        {
                            IsExploded = false
                        };
                        pieSeries.Slices.Add(slice);
                    }
                }

                if (pieSeries.Slices.Count == 0)
                    return string.Empty;

                plotModel.Series.Add(pieSeries);

                var exporter = new SvgExporter { Width = 450, Height = 180 };
                return exporter.ExportToString(plotModel);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static IContainer EstiloCeldaEncabezado(IContainer container)
        {
            return container
                .Background(Colors.Blue.Darken2)
                .Padding(4)
                .DefaultTextStyle(x => x.SemiBold().FontColor(Colors.White).FontSize(8));
        }

        private static IContainer EstiloCeldaCuerpo(IContainer container, string bgColor)
        {
            return container
                .Background(bgColor)
                .Padding(4)
                .BorderBottom(0.5f)
                .BorderColor(Colors.Grey.Lighten2)
                .DefaultTextStyle(x => x.FontSize(8));
        }

        private static IContainer EstiloCeldaTotal(IContainer container)
        {
            return container
                .Background(Colors.Grey.Lighten3)
                .Padding(4)
                .BorderTop(1.5f)
                .BorderColor(Colors.Blue.Darken2)
                .DefaultTextStyle(x => x.Bold().FontSize(8).FontColor(Colors.Blue.Darken3));
        }
    }
}
