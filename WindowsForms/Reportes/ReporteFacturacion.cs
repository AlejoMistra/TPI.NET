using API.Clients;
using DTOs;
using System.Diagnostics;
using WindowsForms.Helpers;

namespace WindowsForms.Reportes
{
    public partial class ReporteFacturacion : UserControl
    {
        private List<ProfesionalDTO> _profesionales = new();
        private bool _isWebViewInitialized = false;

        private byte[]? _currentPdfBytes = null;
        private string? _currentTempFile = null;
        private readonly List<string> _tempFiles = new();

        public ReporteFacturacion()
        {
            InitializeComponent();
            ConfigurarFiltrosIniciales();
        }

        private void ConfigurarFiltrosIniciales()
        {
            var hoy = DateTime.Today;
            var primerDiaMes = new DateTime(hoy.Year, hoy.Month, 1);

            fechaDesdeDateTimePicker.Value = primerDiaMes;
            fechaHastaDateTimePicker.Value = hoy;

            estadoComboBox.Items.Clear();
            estadoComboBox.Items.AddRange(new object[]
            {
                "Todos",
                "Facturado",
                "Pendiente"
            });
            estadoComboBox.SelectedIndex = 0;
        }

        private async void ReporteFacturacion_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await InicializarWebViewAsync();
                await CargarProfesionalesAsync();
                await GenerarReporteAsync();
            }, generarReporteButton, "Error al cargar el Reporte de Facturación");
        }

        private async Task InicializarWebViewAsync()
        {
            if (!_isWebViewInitialized)
            {
                await webViewReporte.EnsureCoreWebView2Async(null);
                webViewReporte.MouseEnter += (s, e) => webViewReporte.Focus();
                _isWebViewInitialized = true;
            }
        }

        private async Task CargarProfesionalesAsync()
        {
            var profesionales = await ProfesionalApiClient.GetAllAsync();
            _profesionales = profesionales.OrderBy(p => p.Apellido).ThenBy(p => p.Nombre).ToList();

            var lista = new List<ProfesionalItem>
            {
                new ProfesionalItem { Id = 0, DisplayName = "Todos los profesionales" }
            };
            lista.AddRange(_profesionales.Select(p => new ProfesionalItem
            {
                Id = p.Id,
                DisplayName = $"{p.Apellido}, {p.Nombre}"
            }));

            profesionalComboBox.DataSource = lista;
            profesionalComboBox.DisplayMember = "DisplayName";
            profesionalComboBox.ValueMember = "Id";
        }

        private async void generarReporteButton_Click(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await GenerarReporteAsync();
            }, generarReporteButton, "Error al generar el reporte de facturación");
        }

        private async Task GenerarReporteAsync()
        {
            var desde = DateOnly.FromDateTime(fechaDesdeDateTimePicker.Value);
            var hasta = DateOnly.FromDateTime(fechaHastaDateTimePicker.Value);

            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.",
                    "Rango de Fechas Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? profesionalId = null;
            if (profesionalComboBox.SelectedValue is int profId && profId > 0)
            {
                profesionalId = profId;
            }

            EstadoFacturacion estado = estadoComboBox.SelectedIndex switch
            {
                1 => EstadoFacturacion.Facturado,
                2 => EstadoFacturacion.Pendiente,
                _ => EstadoFacturacion.Todos
            };

            var filtro = new ReporteFacturacionFiltro(desde, hasta, profesionalId, estado);

            byte[] pdfBytes = await ReporteApiClient.GetFacturacionPdfAsync(filtro);
            _currentPdfBytes = pdfBytes;

            await MostrarPdfEnWebViewAsync(pdfBytes);
            descargarPdfButton.Enabled = (_currentPdfBytes != null && _currentPdfBytes.Length > 0);
        }

        private async Task MostrarPdfEnWebViewAsync(byte[] pdfBytes)
        {
            await InicializarWebViewAsync();

            // Liberar temporal anterior en WebView2
            webViewReporte.CoreWebView2.Navigate("about:blank");
            await Task.Delay(50);

            string tempPath = Path.Combine(Path.GetTempPath(), $"reporte_facturacion_{Guid.NewGuid():N}.pdf");
            await File.WriteAllBytesAsync(tempPath, pdfBytes);

            _currentTempFile = tempPath;
            _tempFiles.Add(tempPath);

            webViewReporte.CoreWebView2.Navigate(tempPath);
            webViewReporte.Focus();

            // Limpieza diferida de temporales antiguos
            LimpiarArchivosTemporales(forzar: false);
        }

        private void descargarPdfButton_Click(object? sender, EventArgs e)
        {
            if (_currentPdfBytes == null || _currentPdfBytes.Length == 0)
            {
                MessageBox.Show("No hay reporte generado disponible para descargar.",
                    "Descargar PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var desde = DateOnly.FromDateTime(fechaDesdeDateTimePicker.Value);
            var hasta = DateOnly.FromDateTime(fechaHastaDateTimePicker.Value);
            string sugerenciaNombre = $"reporte_facturacion_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";

            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*",
                Title = "Guardar Reporte de Facturación",
                FileName = sugerenciaNombre
            };

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(saveFileDialog.FileName, _currentPdfBytes);

                    var respuesta = MessageBox.Show(
                        "El reporte ha sido guardado exitosamente.\n\n¿Desea abrir el archivo generado ahora?",
                        "Descarga Exitosa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(saveFileDialog.FileName)
                        {
                            UseShellExecute = true
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al guardar el archivo:\n{ex.Message}",
                        "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void LimpiarArchivosTemporales(bool forzar)
        {
            try
            {
                if (forzar && webViewReporte?.CoreWebView2 != null)
                {
                    webViewReporte.CoreWebView2.Navigate("about:blank");
                }
            }
            catch
            {
                // Ignorar error al desacoplar WebView2 en Dispose
            }

            foreach (var path in _tempFiles.ToList())
            {
                if (!forzar && path == _currentTempFile)
                {
                    continue; // No borrar el archivo actualmente visible
                }

                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    _tempFiles.Remove(path);
                }
                catch
                {
                    // Si el archivo todavía está retenido por el visor, se mantendrá en _tempFiles
                }
            }
        }

        private class ProfesionalItem
        {
            public int Id { get; set; }
            public string DisplayName { get; set; } = string.Empty;
        }
    }
}

