using API.Clients;
using DTOs;
using System.Data;
using WindowsForms.Helpers;

namespace WindowsForms
{
    public partial class Facturacion : UserControl
    {
        private List<FacturacionGridRow> _allTurnosFacturables = new();
        private List<ProfesionalDTO> _profesionales = new();
        private List<EspecialidadDTO> _especialidades = new();
        private List<PacienteDTO> _pacientes = new();
        private Dictionary<int, FacturaDTO> _facturasPorId = new();
        private bool _isUpdatingBusquedaCombos = false;

        private ContextMenuStrip _facturacionContextMenu = null!;
        private ToolStripMenuItem _facturarMenuItem = null!;
        private ToolStripMenuItem _verFacturaMenuItem = null!;
        private ToolStripMenuItem _detalleTurnoMenuItem = null!;

        public Facturacion()
        {
            InitializeComponent();
            ConfigurarFiltrosIniciales();
            ConfigurarColumnas();
            ConfigurarContextMenu();
        }

        private void ConfigurarFiltrosIniciales()
        {
            // Configurar selectores de rango de fecha (por defecto hoy activado)
            fechaDesdeDateTimePicker.ShowCheckBox = true;
            fechaDesdeDateTimePicker.Checked = true;
            fechaDesdeDateTimePicker.Value = DateTime.Today;

            fechaHastaDateTimePicker.ShowCheckBox = true;
            fechaHastaDateTimePicker.Checked = true;
            fechaHastaDateTimePicker.Value = DateTime.Today;

            _isUpdatingBusquedaCombos = true;
            estadoFacturacionComboBox.Items.Clear();
            estadoFacturacionComboBox.Items.AddRange(new object[]
            {
                "Todos",
                "Pendientes de Facturar",
                "Facturados"
            });
            estadoFacturacionComboBox.SelectedIndex = 0;
            _isUpdatingBusquedaCombos = false;
        }

        private void ConfigurarColumnas()
        {
            turnosDataGridView.AutoGenerateColumns = false;
            turnosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            turnosDataGridView.MultiSelect = false;
            turnosDataGridView.ReadOnly = true;
            turnosDataGridView.AllowUserToAddRows = false;
            turnosDataGridView.AllowUserToDeleteRows = false;
            turnosDataGridView.RowHeadersVisible = false;

            turnosDataGridView.EnableHeadersVisualStyles = false;
            turnosDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = turnosDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            turnosDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = turnosDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            turnosDataGridView.Columns.Clear();

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idColumn",
                HeaderText = "N° Turno",
                DataPropertyName = nameof(FacturacionGridRow.Id),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fechaColumn",
                HeaderText = "Fecha",
                DataPropertyName = nameof(FacturacionGridRow.Fecha),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horarioColumn",
                HeaderText = "Horario",
                DataPropertyName = nameof(FacturacionGridRow.Horario),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "pacienteColumn",
                HeaderText = "Paciente",
                DataPropertyName = nameof(FacturacionGridRow.PacienteNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "obraSocialColumn",
                HeaderText = "Obra Social",
                DataPropertyName = nameof(FacturacionGridRow.ObraSocial),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "especialidadColumn",
                HeaderText = "Especialidad",
                DataPropertyName = nameof(FacturacionGridRow.EspecialidadNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "profesionalColumn",
                HeaderText = "Profesional",
                DataPropertyName = nameof(FacturacionGridRow.ProfesionalNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "estadoTurnoColumn",
                HeaderText = "Estado Turno",
                DataPropertyName = nameof(FacturacionGridRow.EstadoTurno),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "estadoFacturacionColumn",
                HeaderText = "Facturación",
                DataPropertyName = nameof(FacturacionGridRow.EstadoFacturacionTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "montoTotalColumn",
                HeaderText = "Monto Total",
                DataPropertyName = nameof(FacturacionGridRow.MontoTotalTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "accionFacturarColumn",
                HeaderText = "",
                DataPropertyName = nameof(FacturacionGridRow.AccionBotonTexto),
                UseColumnTextForButtonValue = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });
        }

        // ==========================================
        // CARGA ASINCRÓNICA DE DATOS
        // ==========================================

        private async void Facturacion_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await CargarEspecialidadesAsync();
                await CargarProfesionalesAsync();
                await CargarPacientesAsync();
                await CargarTurnosFacturablesAsync();
            }, this, "Error al cargar el módulo de facturación");
        }

        private async Task CargarEspecialidadesAsync()
        {
            var especialidades = await EspecialidadApiClient.GetAllAsync();
            _especialidades = especialidades.OrderBy(e => e.Nombre).ToList();

            var listaBusqueda = new List<EspecialidadDTO>
            {
                new EspecialidadDTO { Id = 0, Nombre = "Todas las especialidades" }
            };
            listaBusqueda.AddRange(_especialidades);

            _isUpdatingBusquedaCombos = true;
            busquedaEspecialidadComboBox.DataSource = listaBusqueda;
            busquedaEspecialidadComboBox.DisplayMember = "Nombre";
            busquedaEspecialidadComboBox.ValueMember = "Id";
            _isUpdatingBusquedaCombos = false;
        }

        private async Task CargarProfesionalesAsync()
        {
            var profesionales = await ProfesionalApiClient.GetAllAsync();
            _profesionales = profesionales.OrderBy(p => p.Apellido).ThenBy(p => p.Nombre).ToList();

            PoblarComboProfesionalesBusqueda(0);
        }

        private void PoblarComboProfesionalesBusqueda(int especialidadIdFiltrar)
        {
            var filtrados = especialidadIdFiltrar > 0
                ? _profesionales.Where(p => p.EspecialidadId == especialidadIdFiltrar).ToList()
                : _profesionales;

            var lista = new List<ProfesionalItem>
            {
                new ProfesionalItem { Id = 0, DisplayName = "Todos los profesionales" }
            };
            lista.AddRange(filtrados.Select(p => new ProfesionalItem
            {
                Id = p.Id,
                DisplayName = $"{p.Apellido}, {p.Nombre}"
            }));

            _isUpdatingBusquedaCombos = true;
            busquedaProfesionalComboBox.DataSource = lista;
            busquedaProfesionalComboBox.DisplayMember = "DisplayName";
            busquedaProfesionalComboBox.ValueMember = "Id";
            _isUpdatingBusquedaCombos = false;
        }

        private async Task CargarPacientesAsync()
        {
            var pacientes = await PacienteApiClient.GetAllAsync();
            _pacientes = pacientes.ToList();
        }

        private async Task CargarTurnosFacturablesAsync()
        {
            var turnos = await TurnoApiClient.GetAllAsync();
            var facturas = await FacturaApiClient.GetAllAsync();
            _facturasPorId = facturas.ToDictionary(f => f.Id, f => f);

            // Solo turnos en estados facturables: Asignado, Presente o Atendido
            var turnosFacturables = turnos.Where(t =>
                string.Equals(t.EstadoTurno, "Asignado", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(t.EstadoTurno, "Presente", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(t.EstadoTurno, "Atendido", StringComparison.OrdinalIgnoreCase));

            _allTurnosFacturables = turnosFacturables.Select(t =>
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == t.ProfesionalId);
                var pac = t.PacienteId.HasValue ? _pacientes.FirstOrDefault(p => p.Id == t.PacienteId.Value) : null;
                var esp = prof != null ? _especialidades.FirstOrDefault(e => e.Id == prof.EspecialidadId) : null;
                FacturaDTO? factura = t.FacturaId.HasValue && _facturasPorId.TryGetValue(t.FacturaId.Value, out var f) ? f : null;

                return new FacturacionGridRow
                {
                    Id = t.Id,
                    Fecha = t.FechaHoraInicio.ToString("dd/MM/yyyy"),
                    Horario = $"{t.FechaHoraInicio:HH:mm} - {t.FechaHoraFin:HH:mm}",
                    ProfesionalId = t.ProfesionalId,
                    ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : $"ID {t.ProfesionalId}",
                    EspecialidadId = prof?.EspecialidadId,
                    EspecialidadNombre = esp?.Nombre ?? "—",
                    PacienteId = t.PacienteId,
                    PacienteNombre = pac != null ? $"{pac.Apellido}, {pac.Nombre} (DNI: {pac.NroDocumento})" : "— Sin Asignar —",
                    ObraSocial = pac != null && !string.IsNullOrWhiteSpace(pac.ObraSocial) ? pac.ObraSocial : "Particular",
                    EstadoTurno = t.EstadoTurno,
                    FacturaId = t.FacturaId,
                    MontoTotal = factura?.MontoTotal,
                    MetodoPago = factura?.MetodoPago ?? string.Empty,
                    Motivo = t.Motivo,
                    Observaciones = t.Observaciones,
                    FechaHoraInicio = t.FechaHoraInicio,
                    FechaHoraFin = t.FechaHoraFin
                };
            }).OrderByDescending(t => t.FechaHoraInicio).ToList();

            AplicarFiltros();
        }

        // ==========================================
        // FILTROS Y COMBOS SINCRONIZADOS
        // ==========================================

        private void BusquedaEspecialidadComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingBusquedaCombos) return;

            var especialidadId = ObtenerComboSelectedId(busquedaEspecialidadComboBox);
            var currentProfId = ObtenerComboSelectedId(busquedaProfesionalComboBox);

            PoblarComboProfesionalesBusqueda(especialidadId);

            if (currentProfId > 0)
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == currentProfId);
                if (prof != null && (especialidadId == 0 || prof.EspecialidadId == especialidadId))
                {
                    _isUpdatingBusquedaCombos = true;
                    busquedaProfesionalComboBox.SelectedValue = currentProfId;
                    _isUpdatingBusquedaCombos = false;
                }
            }
        }

        private void BusquedaProfesionalComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingBusquedaCombos) return;

            var profesionalId = ObtenerComboSelectedId(busquedaProfesionalComboBox);
            if (profesionalId > 0)
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == profesionalId);
                if (prof != null && prof.EspecialidadId > 0)
                {
                    _isUpdatingBusquedaCombos = true;
                    busquedaEspecialidadComboBox.SelectedValue = prof.EspecialidadId;
                    _isUpdatingBusquedaCombos = false;
                }
            }
        }

        private static int ObtenerComboSelectedId(ComboBox comboBox)
        {
            if (comboBox.SelectedValue is int id) return id;
            if (comboBox.SelectedValue is EspecialidadDTO esp) return esp.Id;
            if (comboBox.SelectedValue is ProfesionalItem prof) return prof.Id;
            return 0;
        }

        private void FechaDesdeDateTimePicker_ValueChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingBusquedaCombos) return;

            if (fechaDesdeDateTimePicker.Checked && fechaHastaDateTimePicker.Checked &&
                fechaDesdeDateTimePicker.Value.Date > fechaHastaDateTimePicker.Value.Date)
            {
                _isUpdatingBusquedaCombos = true;
                fechaHastaDateTimePicker.Value = fechaDesdeDateTimePicker.Value.Date;
                _isUpdatingBusquedaCombos = false;
            }

            AplicarFiltros();
        }

        private void FechaHastaDateTimePicker_ValueChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingBusquedaCombos) return;

            if (fechaDesdeDateTimePicker.Checked && fechaHastaDateTimePicker.Checked &&
                fechaHastaDateTimePicker.Value.Date < fechaDesdeDateTimePicker.Value.Date)
            {
                _isUpdatingBusquedaCombos = true;
                fechaDesdeDateTimePicker.Value = fechaHastaDateTimePicker.Value.Date;
                _isUpdatingBusquedaCombos = false;
            }

            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            if (_isUpdatingBusquedaCombos) return;

            bool filtrarFechaDesde = fechaDesdeDateTimePicker.Checked;
            DateTime fechaDesde = fechaDesdeDateTimePicker.Value.Date;
            bool filtrarFechaHasta = fechaHastaDateTimePicker.Checked;
            DateTime fechaHasta = fechaHastaDateTimePicker.Value.Date;
            var especialidadId = ObtenerComboSelectedId(busquedaEspecialidadComboBox);
            var profesionalId = ObtenerComboSelectedId(busquedaProfesionalComboBox);
            string textoPaciente = busquedaPacienteTextBox.Text.Trim().ToLowerInvariant();
            int filtroFacturacionIndex = estadoFacturacionComboBox.SelectedIndex;

            var filtrados = _allTurnosFacturables.AsEnumerable();

            if (filtrarFechaDesde)
            {
                filtrados = filtrados.Where(t => t.FechaHoraInicio.Date >= fechaDesde);
            }

            if (filtrarFechaHasta)
            {
                filtrados = filtrados.Where(t => t.FechaHoraInicio.Date <= fechaHasta);
            }

            if (especialidadId > 0)
            {
                filtrados = filtrados.Where(t => t.EspecialidadId == especialidadId);
            }

            if (profesionalId > 0)
            {
                filtrados = filtrados.Where(t => t.ProfesionalId == profesionalId);
            }

            if (!string.IsNullOrWhiteSpace(textoPaciente))
            {
                filtrados = filtrados.Where(t => t.PacienteNombre.ToLowerInvariant().Contains(textoPaciente));
            }

            if (filtroFacturacionIndex == 1) // Pendientes de Facturar
            {
                filtrados = filtrados.Where(t => !t.EstaFacturado);
            }
            else if (filtroFacturacionIndex == 2) // Facturados
            {
                filtrados = filtrados.Where(t => t.EstaFacturado);
            }

            turnosDataGridView.DataSource = filtrados.OrderBy(t => t.FechaHoraInicio).ToList();
        }

        private void FiltrarDataGridView(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            _isUpdatingBusquedaCombos = true;
            if (busquedaEspecialidadComboBox.Items.Count > 0) busquedaEspecialidadComboBox.SelectedIndex = 0;
            PoblarComboProfesionalesBusqueda(0);
            if (busquedaProfesionalComboBox.Items.Count > 0) busquedaProfesionalComboBox.SelectedIndex = 0;
            if (estadoFacturacionComboBox.Items.Count > 0) estadoFacturacionComboBox.SelectedIndex = 0;
            fechaDesdeDateTimePicker.Value = DateTime.Today;
            fechaDesdeDateTimePicker.Checked = false;
            fechaHastaDateTimePicker.Value = DateTime.Today;
            fechaHastaDateTimePicker.Checked = false;
            _isUpdatingBusquedaCombos = false;

            busquedaPacienteTextBox.Text = string.Empty;

            AplicarFiltros();
        }

        // ==========================================
        // MENÚ CONTEXTUAL Y ACCIONES DE FACTURACIÓN
        // ==========================================

        private void ConfigurarContextMenu()
        {
            _facturacionContextMenu = new ContextMenuStrip();

            _facturarMenuItem = new ToolStripMenuItem("Emitir factura (Maestro/Detalle)...", null, async (s, e) =>
            {
                var row = GetSelectedRow();
                if (row != null) await AbrirModalFacturacionAsync(row.Id);
            });

            _verFacturaMenuItem = new ToolStripMenuItem("Ver factura / Anular comprobante...", null, async (s, e) =>
            {
                var row = GetSelectedRow();
                if (row != null) await AbrirModalFacturacionAsync(row.Id);
            });

            var sep = new ToolStripSeparator();

            _detalleTurnoMenuItem = new ToolStripMenuItem("Ver detalle del turno", null, (s, e) =>
            {
                var row = GetSelectedRow();
                if (row != null) MostrarDetalleTurno(row);
            });

            _facturacionContextMenu.Items.AddRange(new ToolStripItem[]
            {
                _facturarMenuItem,
                _verFacturaMenuItem,
                sep,
                _detalleTurnoMenuItem
            });

            _facturacionContextMenu.Opening += FacturacionContextMenu_Opening;
            turnosDataGridView.ContextMenuStrip = _facturacionContextMenu;
        }

        private void FacturacionContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var row = GetSelectedRow();
            if (row == null)
            {
                e.Cancel = true;
                return;
            }

            _facturarMenuItem.Enabled = !row.EstaFacturado;
            _verFacturaMenuItem.Enabled = row.EstaFacturado;
            _detalleTurnoMenuItem.Enabled = true;
        }

        private FacturacionGridRow? GetSelectedRow()
        {
            return turnosDataGridView.CurrentRow?.DataBoundItem as FacturacionGridRow;
        }

        private void TurnosDataGridView_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                turnosDataGridView.ClearSelection();
                turnosDataGridView.Rows[e.RowIndex].Selected = true;
                turnosDataGridView.CurrentCell = turnosDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex >= 0 ? e.ColumnIndex : 0];
            }
        }

        private void TurnosDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            turnosDataGridView.ClearSelection();
            turnosDataGridView.CurrentCell = null;
        }

        private async void TurnosDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender!;
            if (grid.Columns[e.ColumnIndex].Name == "accionFacturarColumn")
            {
                if (grid.Rows[e.RowIndex].DataBoundItem is FacturacionGridRow rowItem)
                {
                    await AbrirModalFacturacionAsync(rowItem.Id);
                }
            }
        }

        private async Task AbrirModalFacturacionAsync(int turnoId)
        {
            using (var modal = new FacturaModalForm(turnoId))
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    await CargarTurnosFacturablesAsync();
                }
            }
        }

        private void MostrarDetalleTurno(FacturacionGridRow rowItem)
        {
            var infoFactura = rowItem.EstaFacturado
                ? $"Facturado (Factura N° {rowItem.FacturaId:D5} - Total: {rowItem.MontoTotalTexto})"
                : "Pendiente de Facturar";

            MessageBox.Show(
                $"Turno N° {rowItem.Id}\n\n" +
                $"Fecha: {rowItem.Fecha} ({rowItem.Horario})\n" +
                $"Especialidad: {rowItem.EspecialidadNombre}\n" +
                $"Profesional: {rowItem.ProfesionalNombre}\n" +
                $"Paciente: {rowItem.PacienteNombre}\n" +
                $"Obra Social: {rowItem.ObraSocial}\n" +
                $"Estado del Turno: {rowItem.EstadoTurno}\n" +
                $"Estado de Facturación: {infoFactura}\n" +
                $"Motivo: {(string.IsNullOrWhiteSpace(rowItem.Motivo) ? "— Sin motivo —" : rowItem.Motivo)}",
                "Detalle del Turno",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void TurnosDataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= turnosDataGridView.Rows.Count) return;
            var rowItem = turnosDataGridView.Rows[e.RowIndex].DataBoundItem as FacturacionGridRow;
            if (rowItem == null || e.CellStyle == null) return;

            if (rowItem.EstaFacturado)
            {
                e.CellStyle.BackColor = Color.FromArgb(238, 248, 240);
                e.CellStyle.ForeColor = Color.FromArgb(25, 95, 45);
            }
        }

        // ==========================================
        // CLASES AUXILIARES DE VISTA
        // ==========================================

        private class ProfesionalItem
        {
            public int Id { get; set; }
            public string DisplayName { get; set; } = string.Empty;
        }

        public class FacturacionGridRow
        {
            public int Id { get; set; }
            public string Fecha { get; set; } = string.Empty;
            public string Horario { get; set; } = string.Empty;
            public int ProfesionalId { get; set; }
            public string ProfesionalNombre { get; set; } = string.Empty;
            public int? EspecialidadId { get; set; }
            public string EspecialidadNombre { get; set; } = string.Empty;
            public int? PacienteId { get; set; }
            public string PacienteNombre { get; set; } = string.Empty;
            public string ObraSocial { get; set; } = string.Empty;
            public string EstadoTurno { get; set; } = string.Empty;
            public int? FacturaId { get; set; }
            public decimal? MontoTotal { get; set; }
            public string MetodoPago { get; set; } = string.Empty;
            public string Motivo { get; set; } = string.Empty;
            public string Observaciones { get; set; } = string.Empty;
            public DateTime FechaHoraInicio { get; set; }
            public DateTime FechaHoraFin { get; set; }

            public bool EstaFacturado => FacturaId.HasValue;
            public string EstadoFacturacionTexto => FacturaId.HasValue ? $"Facturado (#{FacturaId.Value:D5})" : "Pendiente";
            public string MontoTotalTexto => MontoTotal.HasValue ? $"$ {MontoTotal.Value:N2}" : "—";
            public string AccionBotonTexto => FacturaId.HasValue ? "Ver Factura" : "Facturar";
        }
    }
}
