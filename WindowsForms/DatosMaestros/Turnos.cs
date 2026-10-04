using API.Clients;
using DTOs;
using System.Data;
using WindowsForms.Helpers;

namespace WindowsForms.DatosMaestros
{
    public partial class Turnos : UserControl
    {
        private List<TurnoGridRow> _allTurnos = new();
        private List<ProfesionalDTO> _profesionales = new();
        private List<EspecialidadDTO> _especialidades = new();
        private int? _selectedTurnoId = null;
        private int? _selectedTurnoPacienteId = null;
        private bool _isUpdatingCombos = false;
        private bool _isUpdatingBusquedaCombos = false;

        public Turnos()
        {
            InitializeComponent();
            ConfigurarColumnas();
            InicializarEstados();

            // Habilitar checkbox en el selector de fecha de búsqueda
            busquedaFechaDateTimePicker.ShowCheckBox = true;
            busquedaFechaDateTimePicker.Checked = false;

            // Suscribir eventos
            this.Load += Turnos_Load;
            turnosDataGridView.CellContentClick += TurnosDataGridView_CellContentClick;
            turnosDataGridView.DataBindingComplete += TurnosDataGridView_DataBindingComplete;

            especialidadComboBox.SelectedIndexChanged += EspecialidadComboBox_SelectedIndexChanged;
            profesionalComboBox.SelectedIndexChanged += ProfesionalComboBox_SelectedIndexChanged;

            busquedaEspecialidadComboBox.SelectedIndexChanged += BusquedaEspecialidadComboBox_SelectedIndexChanged;
            busquedaProfesionalComboBox.SelectedIndexChanged += BusquedaProfesionalComboBox_SelectedIndexChanged;
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
                DataPropertyName = nameof(TurnoGridRow.Id),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fechaColumn",
                HeaderText = "Fecha",
                DataPropertyName = nameof(TurnoGridRow.Fecha),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horaInicioColumn",
                HeaderText = "Hora Inicio",
                DataPropertyName = nameof(TurnoGridRow.HoraInicio),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horaFinColumn",
                HeaderText = "Hora Fin",
                DataPropertyName = nameof(TurnoGridRow.HoraFin),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "profesionalColumn",
                HeaderText = "Profesional",
                DataPropertyName = nameof(TurnoGridRow.ProfesionalNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "especialidadColumn",
                HeaderText = "Especialidad",
                DataPropertyName = nameof(TurnoGridRow.EspecialidadNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "estadoColumn",
                HeaderText = "Estado",
                DataPropertyName = nameof(TurnoGridRow.Estado),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "motivoColumn",
                HeaderText = "Motivo",
                DataPropertyName = nameof(TurnoGridRow.Motivo),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "accionesColumn",
                HeaderText = "",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "eliminarColumn",
                HeaderText = "",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });
        }

        private void InicializarEstados()
        {
            var estados = new[] { "Libre", "Asignado", "Presente", "Atendido", "Ausente" };

            // Combo de búsqueda
            busquedaEstadoComboBox.Items.Clear();
            busquedaEstadoComboBox.Items.Add("Todos");
            busquedaEstadoComboBox.Items.AddRange(estados);
            busquedaEstadoComboBox.SelectedIndex = 0;

            // Combo de formulario
            estadoComboBox.Items.Clear();
            estadoComboBox.Items.AddRange(estados);
            if (estadoComboBox.Items.Count > 0)
            {
                estadoComboBox.SelectedIndex = 0; // Por defecto "Libre"
            }
        }

        // ==========================================
        // CARGA DE DATOS ASINCRÓNICA
        // ==========================================

        private async void Turnos_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await CargarEspecialidadesAsync();
                await CargarProfesionalesAsync();
                await CargarTurnosAsync();
            }, this, "Error al inicializar datos de turnos");
        }

        private async Task CargarEspecialidadesAsync()
        {
            var especialidades = await EspecialidadApiClient.GetAllAsync();
            _especialidades = especialidades.OrderBy(e => e.Nombre).ToList();

            // Combo de búsqueda
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

            // Combo de formulario
            PoblarComboEspecialidadFormulario();
        }

        private void PoblarComboEspecialidadFormulario()
        {
            var listaFormulario = new List<EspecialidadDTO>
            {
                new EspecialidadDTO { Id = 0, Nombre = "Seleccionar especialidad" }
            };
            listaFormulario.AddRange(_especialidades);

            _isUpdatingCombos = true;
            especialidadComboBox.DataSource = listaFormulario;
            especialidadComboBox.DisplayMember = "Nombre";
            especialidadComboBox.ValueMember = "Id";
            _isUpdatingCombos = false;
        }

        private async Task CargarProfesionalesAsync()
        {
            var profesionales = await ProfesionalApiClient.GetAllAsync();
            _profesionales = profesionales.OrderBy(p => p.Apellido).ThenBy(p => p.Nombre).ToList();

            // Combo de búsqueda
            PoblarComboProfesionalesBusqueda(0);

            // Combo de formulario
            PoblarComboProfesionalesFormulario(0);
        }

        private void PoblarComboProfesionalesBusqueda(int especialidadIdFiltrar)
        {
            var filtrados = especialidadIdFiltrar > 0
                ? _profesionales.Where(p => p.EspecialidadId == especialidadIdFiltrar).ToList()
                : _profesionales;

            var listaBusqueda = new List<ProfesionalItem>
            {
                new ProfesionalItem { Id = 0, DisplayName = "Todos los profesionales" }
            };
            listaBusqueda.AddRange(filtrados.Select(p => new ProfesionalItem
            {
                Id = p.Id,
                DisplayName = $"{p.Apellido}, {p.Nombre} (MP: {p.Matricula})"
            }));

            _isUpdatingBusquedaCombos = true;
            busquedaProfesionalComboBox.DataSource = listaBusqueda;
            busquedaProfesionalComboBox.DisplayMember = "DisplayName";
            busquedaProfesionalComboBox.ValueMember = "Id";
            _isUpdatingBusquedaCombos = false;
        }

        private void PoblarComboProfesionalesFormulario(int especialidadIdFiltrar)
        {
            var filtrados = especialidadIdFiltrar > 0
                ? _profesionales.Where(p => p.EspecialidadId == especialidadIdFiltrar).ToList()
                : _profesionales;

            var listaFormulario = new List<ProfesionalItem>
            {
                new ProfesionalItem { Id = 0, DisplayName = "Seleccionar profesional" }
            };
            listaFormulario.AddRange(filtrados.Select(p => new ProfesionalItem
            {
                Id = p.Id,
                DisplayName = $"{p.Apellido}, {p.Nombre} (MP: {p.Matricula})"
            }));

            _isUpdatingCombos = true;
            profesionalComboBox.DataSource = listaFormulario;
            profesionalComboBox.DisplayMember = "DisplayName";
            profesionalComboBox.ValueMember = "Id";
            _isUpdatingCombos = false;
        }

        private async Task CargarTurnosAsync()
        {
            var turnos = await TurnoApiClient.GetAllAsync();

            _allTurnos = turnos.Select(t =>
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == t.ProfesionalId);
                var esp = prof != null ? _especialidades.FirstOrDefault(e => e.Id == prof.EspecialidadId) : null;

                return new TurnoGridRow
                {
                    Id = t.Id,
                    Fecha = t.FechaHoraInicio.ToString("dd/MM/yyyy"),
                    HoraInicio = t.FechaHoraInicio.ToString("HH:mm"),
                    HoraFin = t.FechaHoraFin.ToString("HH:mm"),
                    ProfesionalId = t.ProfesionalId,
                    ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : $"ID {t.ProfesionalId}",
                    EspecialidadId = prof?.EspecialidadId,
                    EspecialidadNombre = esp?.Nombre ?? "—",
                    Estado = t.EstadoTurno,
                    Motivo = t.Motivo,
                    Observaciones = t.Observaciones,
                    FechaHoraInicio = t.FechaHoraInicio,
                    FechaHoraFin = t.FechaHoraFin,
                    PacienteId = t.PacienteId
                };
            }).OrderByDescending(t => t.FechaHoraInicio).ToList();

            AplicarFiltros();
        }

        // ==========================================
        // INTERACCIÓN REACTIVA ESPECIALIDAD <-> PROFESIONAL
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

            AplicarFiltros();
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

            AplicarFiltros();
        }

        private void EspecialidadComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingCombos) return;

            var especialidadId = ObtenerComboSelectedId(especialidadComboBox);
            var currentProfId = ObtenerComboSelectedId(profesionalComboBox);

            PoblarComboProfesionalesFormulario(especialidadId);

            if (currentProfId > 0)
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == currentProfId);
                if (prof != null && (especialidadId == 0 || prof.EspecialidadId == especialidadId))
                {
                    _isUpdatingCombos = true;
                    profesionalComboBox.SelectedValue = currentProfId;
                    _isUpdatingCombos = false;
                }
            }
        }

        private void ProfesionalComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingCombos) return;

            var profesionalId = ObtenerComboSelectedId(profesionalComboBox);
            if (profesionalId > 0)
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == profesionalId);
                if (prof != null && prof.EspecialidadId > 0)
                {
                    _isUpdatingCombos = true;
                    especialidadComboBox.SelectedValue = prof.EspecialidadId;
                    _isUpdatingCombos = false;
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

        // ==========================================
        // FILTRADO
        // ==========================================

        private void AplicarFiltros()
        {
            var profesionalId = ObtenerComboSelectedId(busquedaProfesionalComboBox);
            var especialidadId = ObtenerComboSelectedId(busquedaEspecialidadComboBox);
            var estadoFiltro = busquedaEstadoComboBox.SelectedItem?.ToString() ?? "Todos";
            bool filtrarPorFecha = busquedaFechaDateTimePicker.Checked;
            DateTime fechaSeleccionada = busquedaFechaDateTimePicker.Value.Date;

            var filtrados = _allTurnos.AsEnumerable();

            if (profesionalId > 0)
            {
                filtrados = filtrados.Where(t => t.ProfesionalId == profesionalId);
            }

            if (especialidadId > 0)
            {
                filtrados = filtrados.Where(t => t.EspecialidadId == especialidadId);
            }

            if (estadoFiltro != "Todos")
            {
                filtrados = filtrados.Where(t => string.Equals(t.Estado, estadoFiltro, StringComparison.OrdinalIgnoreCase));
            }

            if (filtrarPorFecha)
            {
                filtrados = filtrados.Where(t => t.FechaHoraInicio.Date == fechaSeleccionada);
            }

            turnosDataGridView.DataSource = filtrados.ToList();
        }

        private void FiltrarDataGridView(object? sender, EventArgs e)
        {
            if (_isUpdatingBusquedaCombos) return;
            AplicarFiltros();
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            _isUpdatingBusquedaCombos = true;
            if (busquedaEspecialidadComboBox.Items.Count > 0) busquedaEspecialidadComboBox.SelectedIndex = 0;
            PoblarComboProfesionalesBusqueda(0);
            if (busquedaProfesionalComboBox.Items.Count > 0) busquedaProfesionalComboBox.SelectedIndex = 0;
            if (busquedaEstadoComboBox.Items.Count > 0) busquedaEstadoComboBox.SelectedIndex = 0;
            _isUpdatingBusquedaCombos = false;

            busquedaFechaDateTimePicker.Checked = false;
            busquedaFechaDateTimePicker.Value = DateTime.Today;

            AplicarFiltros();
        }

        // ==========================================
        // EVENTOS DE LA GRILLA (EDITAR / ELIMINAR)
        // ==========================================

        private void TurnosDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            turnosDataGridView.ClearSelection();
            turnosDataGridView.CurrentCell = null;
        }

        private async void TurnosDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender!;
            var rowItem = grid.Rows[e.RowIndex].DataBoundItem as TurnoGridRow;
            if (rowItem == null) return;

            // Clic en Editar
            if (grid.Columns[e.ColumnIndex].Name == "accionesColumn")
            {
                CargarTurnoEnFormulario(rowItem);
            }
            // Clic en Eliminar
            else if (grid.Columns[e.ColumnIndex].Name == "eliminarColumn")
            {
                var confirmResult = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar el turno N° {rowItem.Id} del {rowItem.Fecha} ({rowItem.HoraInicio} - {rowItem.HoraFin})?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    await UiErrorHandler.ExecuteAsync(async () =>
                    {
                        await TurnoApiClient.DeleteAsync(rowItem.Id);
                        MessageBox.Show("Turno eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (_selectedTurnoId == rowItem.Id)
                        {
                            LimpiarFormulario();
                        }

                        await CargarTurnosAsync();
                    }, this, "Error al eliminar turno");
                }
            }
        }

        private void CargarTurnoEnFormulario(TurnoGridRow row)
        {
            _selectedTurnoId = row.Id;
            _selectedTurnoPacienteId = row.PacienteId;
            numeroTurnoTextBox.Text = row.Id.ToString();

            // Restaurar lista completa de profesionales antes de seleccionar
            PoblarComboProfesionalesFormulario(0);

            profesionalComboBox.SelectedValue = row.ProfesionalId;
            if (row.EspecialidadId.HasValue && row.EspecialidadId.Value > 0)
            {
                especialidadComboBox.SelectedValue = row.EspecialidadId.Value;
            }

            fechaTurnoDateTimePicker.Value = row.FechaHoraInicio.Date;
            horaInicioDateTimePicker.Value = row.FechaHoraInicio;
            horaFinDateTimePicker.Value = row.FechaHoraFin;
            estadoComboBox.SelectedItem = row.Estado;
            guardarTurnoButton.Text = "Actualizar Turno";
        }

        // ==========================================
        // GUARDAR / CANCELAR FORMULARIO
        // ==========================================

        private void AgregarTurnoButton_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            numeroTurnoTextBox.Text = "(Nuevo)";
            profesionalComboBox.Focus();
        }

        private async void GuardarTurnoButton_Click(object? sender, EventArgs e)
        {
            var profesionalId = (profesionalComboBox.SelectedValue as int?) ?? 0;
            if (profesionalId <= 0)
            {
                MessageBox.Show("Debe seleccionar un profesional.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                profesionalComboBox.Focus();
                return;
            }

            var fecha = fechaTurnoDateTimePicker.Value.Date;
            var horaInicio = horaInicioDateTimePicker.Value.TimeOfDay;
            var horaFin = horaFinDateTimePicker.Value.TimeOfDay;

            if (horaFin <= horaInicio)
            {
                MessageBox.Show("La hora de fin debe ser posterior a la hora de inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                horaFinDateTimePicker.Focus();
                return;
            }

            var fechaHoraInicio = fecha.Add(horaInicio);
            var fechaHoraFin = fecha.Add(horaFin);

            var turnoDto = new TurnoDTO
            {
                Id = _selectedTurnoId ?? 0,
                FechaHoraInicio = fechaHoraInicio,
                FechaHoraFin = fechaHoraFin,
                EstadoTurno = estadoComboBox.SelectedItem?.ToString() ?? "Libre",
                Observaciones = string.Empty,
                FacturaId = null,
                ProfesionalId = profesionalId,
                PacienteId = _selectedTurnoPacienteId
            };

            await UiErrorHandler.ExecuteAsync(async () =>
                {
                    if (_selectedTurnoId == null)
                    {
                        await TurnoApiClient.AddAsync(turnoDto);
                        MessageBox.Show("Turno registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        await TurnoApiClient.UpdateAsync(turnoDto);
                        MessageBox.Show("Turno actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LimpiarFormulario();
                    await CargarTurnosAsync();
                }, this, "Error al guardar turno");
        }

        private void CancelarButton_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _selectedTurnoId = null;
            _selectedTurnoPacienteId = null;
            numeroTurnoTextBox.Text = string.Empty;

            PoblarComboEspecialidadFormulario();
            PoblarComboProfesionalesFormulario(0);

            fechaTurnoDateTimePicker.Value = DateTime.Today;
            horaInicioDateTimePicker.Value = DateTime.Today.AddHours(8);
            horaFinDateTimePicker.Value = DateTime.Today.AddHours(8).AddMinutes(30);

            if (estadoComboBox.Items.Count > 0)
            {
                estadoComboBox.SelectedIndex = 0; // "Libre"
            }

            guardarTurnoButton.Text = "Guardar Turno";
        }

        // ==========================================
        // CLASES AUXILIARES DE VISTA
        // ==========================================

        private class ProfesionalItem
        {
            public int Id { get; set; }
            public string DisplayName { get; set; } = string.Empty;
        }

        public class TurnoGridRow
        {
            public int Id { get; set; }
            public string Fecha { get; set; } = string.Empty;
            public string HoraInicio { get; set; } = string.Empty;
            public string HoraFin { get; set; } = string.Empty;
            public int ProfesionalId { get; set; }
            public string ProfesionalNombre { get; set; } = string.Empty;
            public int? EspecialidadId { get; set; }
            public string EspecialidadNombre { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
            public string Motivo { get; set; } = string.Empty;
            public string Observaciones { get; set; } = string.Empty;
            public DateTime FechaHoraInicio { get; set; }
            public DateTime FechaHoraFin { get; set; }
            public int? PacienteId { get; set; }
        }
    }
}
