using API.Clients;
using DTOs;
using System.Data;
using WindowsForms.Helpers;

namespace WindowsForms
{
    public partial class AgendaTurnos : UserControl
    {
        private List<AgendaGridRow> _allTurnos = new();
        private List<ProfesionalDTO> _profesionales = new();
        private List<PacienteDTO> _pacientes = new();

        public AgendaTurnos()
        {
            InitializeComponent();
            ConfigurarColumnas();

            // Configurar selector de fecha de agenda (por defecto hoy activado)
            busquedaFechaDateTimePicker.ShowCheckBox = true;
            busquedaFechaDateTimePicker.Checked = true;
            busquedaFechaDateTimePicker.Value = DateTime.Today;
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
                DataPropertyName = nameof(AgendaGridRow.Id),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fechaColumn",
                HeaderText = "Fecha",
                DataPropertyName = nameof(AgendaGridRow.Fecha),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horaInicioColumn",
                HeaderText = "Hora Inicio",
                DataPropertyName = nameof(AgendaGridRow.HoraInicio),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horaFinColumn",
                HeaderText = "Hora Fin",
                DataPropertyName = nameof(AgendaGridRow.HoraFin),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "pacienteColumn",
                HeaderText = "Paciente",
                DataPropertyName = nameof(AgendaGridRow.PacienteNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "profesionalColumn",
                HeaderText = "Profesional",
                DataPropertyName = nameof(AgendaGridRow.ProfesionalNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "estadoColumn",
                HeaderText = "Estado",
                DataPropertyName = nameof(AgendaGridRow.Estado),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "accionesColumn",
                HeaderText = "",
                Text = "Ver Detalle",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });
        }

        // ==========================================
        // CARGA ASINCRÓNICA DE DATOS
        // ==========================================

        private async void Turnos_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await CargarProfesionalesAsync();
                await CargarPacientesAsync();
                await CargarAgendaAsync();
            }, this, "Error al cargar la agenda de turnos");
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

            busquedaProfesionalComboBox.DataSource = lista;
            busquedaProfesionalComboBox.DisplayMember = "DisplayName";
            busquedaProfesionalComboBox.ValueMember = "Id";
        }

        private async Task CargarPacientesAsync()
        {
            var pacientes = await PacienteApiClient.GetAllAsync();
            _pacientes = pacientes.ToList();
        }

        private async Task CargarAgendaAsync()
        {
            var turnos = await TurnoApiClient.GetAllAsync();

            _allTurnos = turnos.Select(t =>
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == t.ProfesionalId);
                var pac = t.PacienteId.HasValue ? _pacientes.FirstOrDefault(p => p.Id == t.PacienteId.Value) : null;

                return new AgendaGridRow
                {
                    Id = t.Id,
                    Fecha = t.FechaHoraInicio.ToString("dd/MM/yyyy"),
                    HoraInicio = t.FechaHoraInicio.ToString("HH:mm"),
                    HoraFin = t.FechaHoraFin.ToString("HH:mm"),
                    ProfesionalId = t.ProfesionalId,
                    ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : $"ID {t.ProfesionalId}",
                    PacienteId = t.PacienteId,
                    PacienteNombre = pac != null ? $"{pac.Apellido}, {pac.Nombre} (DNI: {pac.NroDocumento})" : "— Sin Asignar —",
                    Estado = t.EstadoTurno,
                    Motivo = t.Motivo,
                    Observaciones = t.Observaciones,
                    FechaHoraInicio = t.FechaHoraInicio,
                    FechaHoraFin = t.FechaHoraFin
                };
            }).OrderBy(t => t.FechaHoraInicio).ToList();

            AplicarFiltros();
        }

        // ==========================================
        // FILTROS
        // ==========================================

        private void AplicarFiltros()
        {
            bool filtrarPorFecha = busquedaFechaDateTimePicker.Checked;
            DateTime fechaSeleccionada = busquedaFechaDateTimePicker.Value.Date;
            var profesionalId = (busquedaProfesionalComboBox.SelectedValue as int?) ?? 0;
            string textoPaciente = busquedaPacienteTextBox.Text.Trim().ToLowerInvariant();

            var filtrados = _allTurnos.AsEnumerable();

            if (filtrarPorFecha)
            {
                filtrados = filtrados.Where(t => t.FechaHoraInicio.Date == fechaSeleccionada);
            }

            if (profesionalId > 0)
            {
                filtrados = filtrados.Where(t => t.ProfesionalId == profesionalId);
            }

            if (!string.IsNullOrWhiteSpace(textoPaciente))
            {
                filtrados = filtrados.Where(t => t.PacienteNombre.ToLowerInvariant().Contains(textoPaciente));
            }

            turnosDataGridView.DataSource = filtrados.ToList();
        }

        private void FiltrarButton_Click(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            busquedaFechaDateTimePicker.Checked = false;
            busquedaFechaDateTimePicker.Value = DateTime.Today;

            if (busquedaProfesionalComboBox.Items.Count > 0)
            {
                busquedaProfesionalComboBox.SelectedIndex = 0;
            }

            busquedaPacienteTextBox.Text = string.Empty;

            AplicarFiltros();
        }

        // ==========================================
        // ACCIONES DE LA AGENDA
        // ==========================================

        private void TurnosDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            turnosDataGridView.ClearSelection();
            turnosDataGridView.CurrentCell = null;
        }

        private void TurnosDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender!;
            if (grid.Columns[e.ColumnIndex].Name == "accionesColumn")
            {
                var rowItem = grid.Rows[e.RowIndex].DataBoundItem as AgendaGridRow;
                if (rowItem == null) return;

                MessageBox.Show(
                    $"Turno N° {rowItem.Id}\n" +
                    $"Fecha: {rowItem.Fecha} ({rowItem.HoraInicio} - {rowItem.HoraFin})\n" +
                    $"Profesional: {rowItem.ProfesionalNombre}\n" +
                    $"Paciente: {rowItem.PacienteNombre}\n" +
                    $"Estado: {rowItem.Estado}\n" +
                    $"Motivo: {rowItem.Motivo}\n\n" +
                    "Nota: La reasignación y cambio de estados se habilitará en la próxima iteración.",
                    "Detalle del Turno",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void nuevoTurnoButton_Click(object? sender, EventArgs e)
        {
            // abre form AsignacionTurno estilo modal para crear un nuevo turno
            using (var form = new AsignacionTurno())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // recargar agenda
                    _ = CargarAgendaAsync();
                }
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

        public class AgendaGridRow
        {
            public int Id { get; set; }
            public string Fecha { get; set; } = string.Empty;
            public string HoraInicio { get; set; } = string.Empty;
            public string HoraFin { get; set; } = string.Empty;
            public int ProfesionalId { get; set; }
            public string ProfesionalNombre { get; set; } = string.Empty;
            public int? PacienteId { get; set; }
            public string PacienteNombre { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
            public string Motivo { get; set; } = string.Empty;
            public string Observaciones { get; set; } = string.Empty;
            public DateTime FechaHoraInicio { get; set; }
            public DateTime FechaHoraFin { get; set; }
        }
    }
}
