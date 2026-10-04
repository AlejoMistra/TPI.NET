using API.Clients;
using DTOs;
using System.Data;
using WindowsForms.Helpers;

namespace WindowsForms
{
    public partial class AsignacionTurno : Form
    {
        private readonly int? _turnoIdEditar;

        private List<TurnoDTO> _allTurnos = new();
        private List<TurnoDTO> _turnosDisponibles = new();
        private List<ProfesionalDTO> _profesionales = new();
        private List<EspecialidadDTO> _especialidades = new();
        private List<PacienteDTO> _pacientes = new();

        private TurnoDTO? _selectedTurno = null;
        private PacienteDTO? _selectedPaciente = null;
        private bool _isUpdatingCombos = false;
        private bool _esNuevoPaciente = false;

        public AsignacionTurno(int? turnoIdEditar = null)
        {
            _turnoIdEditar = turnoIdEditar;
            InitializeComponent();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Campos de paciente en solo lectura por defecto
            nombreTextBox.ReadOnly = true;
            nombreTextBox.BackColor = SystemColors.Control;
            apellidoTextBox.ReadOnly = true;
            apellidoTextBox.BackColor = SystemColors.Control;
            documentoTextBox.ReadOnly = true;
            documentoTextBox.BackColor = SystemColors.Control;
            telefonoTextBox.ReadOnly = true;
            telefonoTextBox.BackColor = SystemColors.Control;
            emailTextBox.ReadOnly = true;
            emailTextBox.BackColor = SystemColors.Control;
            obraSocialTextBox.ReadOnly = true;
            obraSocialTextBox.BackColor = SystemColors.Control;

            fechaNacimientoDateTimePicker.Value = DateTime.Today.AddYears(-30);
            fechaNacimientoDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaNacimientoDateTimePicker.Enabled = false;

            fechaDesdeDateTimePicker.Value = DateTime.Today;
            fechaDesdeDateTimePicker.Format = DateTimePickerFormat.Short;

            // Configurar grilla de turnos disponibles
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridView1.ColumnHeadersDefaultCellStyle.BackColor;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor;

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idColumn",
                HeaderText = "N° Turno",
                DataPropertyName = nameof(TurnoDisponibleGridRow.Id),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fechaColumn",
                HeaderText = "Fecha",
                DataPropertyName = nameof(TurnoDisponibleGridRow.Fecha),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horaInicioColumn",
                HeaderText = "Hora Inicio",
                DataPropertyName = nameof(TurnoDisponibleGridRow.HoraInicio),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horaFinColumn",
                HeaderText = "Hora Fin",
                DataPropertyName = nameof(TurnoDisponibleGridRow.HoraFin),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "especialidadColumn",
                HeaderText = "Especialidad",
                DataPropertyName = nameof(TurnoDisponibleGridRow.EspecialidadNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "profesionalColumn",
                HeaderText = "Profesional",
                DataPropertyName = nameof(TurnoDisponibleGridRow.ProfesionalNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Suscribir eventos
            this.Load += AsignacionTurno_Load;
            button4.Click += BuscarPacienteButton_Click;
            nuevoPacienteButton.Click += (s, e) => HabilitarModoNuevoPaciente(busquedaTextBox.Text.Trim());
            button1.Click += ConfirmarButton_Click;
            button2.Click += CancelarButton_Click;

            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            especialidadComboBox.SelectedIndexChanged += EspecialidadComboBox_SelectedIndexChanged;
            especialidadComboBox.SelectedValueChanged += FiltrarDataGridView;
            ProfesionalComboBox.SelectedIndexChanged += ProfesionalComboBox_SelectedIndexChanged;
            ProfesionalComboBox.SelectedValueChanged += FiltrarDataGridView;
            fechaDesdeDateTimePicker.ValueChanged += FiltrarDataGridView;
            fechaDesdeDateTimePicker.BindingContextChanged += FiltrarDataGridView;
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;

            busquedaTextBox.KeyDown += BusquedaTextBox_KeyDown;
            this.KeyPreview = true;
            this.KeyDown += Form_KeyDown;

            ActualizarLabelTurnoSeleccionado();
            LimpiarDatosPaciente();
        }

        // ==========================================
        // CARGA ASINCRÓNICA DE DATOS
        // ==========================================

        private async void AsignacionTurno_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await CargarEspecialidadesAsync();
                await CargarProfesionalesAsync();
                await CargarPacientesAsync();
                await CargarTurnosAsync();

                if (_turnoIdEditar.HasValue)
                {
                    this.Text = $"Editar Asignación - Turno N° {_turnoIdEditar.Value}";
                    await CargarTurnoParaEdicionAsync(_turnoIdEditar.Value);
                }
                else
                {
                    this.Text = "Asignar Turno a Paciente";
                    AplicarFiltros();
                }
            }, this, "Error al inicializar formulario de asignación");
        }

        private async Task CargarEspecialidadesAsync()
        {
            var especialidades = await EspecialidadApiClient.GetAllAsync();
            _especialidades = especialidades.OrderBy(e => e.Nombre).ToList();

            var lista = new List<EspecialidadDTO>
            {
                new EspecialidadDTO { Id = 0, Nombre = "Todas las especialidades" }
            };
            lista.AddRange(_especialidades);

            _isUpdatingCombos = true;
            especialidadComboBox.DataSource = lista;
            especialidadComboBox.DisplayMember = "Nombre";
            especialidadComboBox.ValueMember = "Id";
            _isUpdatingCombos = false;
        }

        private async Task CargarProfesionalesAsync()
        {
            var profesionales = await ProfesionalApiClient.GetAllAsync();
            _profesionales = profesionales.OrderBy(p => p.Apellido).ThenBy(p => p.Nombre).ToList();

            PoblarComboProfesionales(0);
        }

        private void PoblarComboProfesionales(int especialidadIdFiltrar)
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

            _isUpdatingCombos = true;
            ProfesionalComboBox.DataSource = lista;
            ProfesionalComboBox.DisplayMember = "DisplayName";
            ProfesionalComboBox.ValueMember = "Id";
            _isUpdatingCombos = false;
        }

        private async Task CargarPacientesAsync()
        {
            var pacientes = await PacienteApiClient.GetAllAsync();
            _pacientes = pacientes.OrderBy(p => p.Apellido).ThenBy(p => p.Nombre).ToList();
        }

        private async Task CargarTurnosAsync()
        {
            var todos = await TurnoApiClient.GetAllAsync();
            _allTurnos = todos.ToList();
        }

        private async Task CargarTurnoParaEdicionAsync(int turnoId)
        {
            var turno = await TurnoApiClient.GetAsync(turnoId);
            if (turno == null)
            {
                MessageBox.Show("No se encontró el turno seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            _selectedTurno = turno;

            _isUpdatingCombos = true;
            if (turno.FechaHoraInicio.Date < fechaDesdeDateTimePicker.Value.Date)
            {
                fechaDesdeDateTimePicker.Value = turno.FechaHoraInicio.Date;
            }

            var prof = _profesionales.FirstOrDefault(p => p.Id == turno.ProfesionalId);
            if (prof != null)
            {
                if (prof.EspecialidadId > 0)
                {
                    especialidadComboBox.SelectedValue = prof.EspecialidadId;
                    PoblarComboProfesionales(prof.EspecialidadId);
                }
                ProfesionalComboBox.SelectedValue = prof.Id;
            }
            _isUpdatingCombos = false;

            AplicarFiltros(incluirTurnoId: turnoId);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DataBoundItem is TurnoDisponibleGridRow item && item.Id == turnoId)
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    break;
                }
            }

            if (turno.PacienteId.HasValue && turno.PacienteId.Value > 0)
            {
                var pac = _pacientes.FirstOrDefault(p => p.Id == turno.PacienteId.Value);
                if (pac != null)
                {
                    SeleccionarPaciente(pac);
                }
            }

            motivoTextBox.Text = turno.Motivo ?? string.Empty;
            observacionesTextBox.Text = turno.Observaciones ?? string.Empty;
            ActualizarLabelTurnoSeleccionado();
        }

        // ==========================================
        // BÚSQUEDA Y FILTRADO DE TURNOS
        // ==========================================

        private void AplicarFiltros(int? incluirTurnoId = null)
        {
            var turnoIdIncluir = incluirTurnoId ?? _turnoIdEditar ?? _selectedTurno?.Id;
            var fechaFiltro = fechaDesdeDateTimePicker.Value.Date;
            var especialidadId = ObtenerComboSelectedId(especialidadComboBox);
            var profesionalId = ObtenerComboSelectedId(ProfesionalComboBox);

            var query = _allTurnos.Where(t =>
                (string.Equals(t.EstadoTurno, "Libre", StringComparison.OrdinalIgnoreCase) || (turnoIdIncluir.HasValue && t.Id == turnoIdIncluir.Value)) &&
                t.FechaHoraInicio.Date >= fechaFiltro &&
                t.FechaHoraInicio >= DateTime.Now
            );

            if (profesionalId > 0)
            {
                query = query.Where(t => t.ProfesionalId == profesionalId);
            }
            else if (especialidadId > 0)
            {
                var profIdsEspecialidad = _profesionales.Where(p => p.EspecialidadId == especialidadId).Select(p => p.Id).ToHashSet();
                query = query.Where(t => profIdsEspecialidad.Contains(t.ProfesionalId));
            }

            _turnosDisponibles = query.OrderBy(t => t.FechaHoraInicio).ToList();

            var rows = _turnosDisponibles.Select(t =>
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == t.ProfesionalId);
                var esp = prof != null ? _especialidades.FirstOrDefault(e => e.Id == prof.EspecialidadId) : null;

                return new TurnoDisponibleGridRow
                {
                    Id = t.Id,
                    Fecha = t.FechaHoraInicio.ToString("dd/MM/yyyy"),
                    HoraInicio = t.FechaHoraInicio.ToString("HH:mm"),
                    HoraFin = t.FechaHoraFin.ToString("HH:mm"),
                    EspecialidadNombre = esp?.Nombre ?? "—",
                    ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : $"ID {t.ProfesionalId}",
                    TurnoOriginal = t
                };
            }).ToList();

            dataGridView1.DataSource = rows;

            if (_selectedTurno != null)
            {
                var found = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.DataBoundItem is TurnoDisponibleGridRow item && item.Id == _selectedTurno.Id)
                    {
                        row.Selected = true;
                        dataGridView1.CurrentCell = row.Cells[0];
                        found = true;
                        break;
                    }
                }
                if (!found && !turnoIdIncluir.HasValue)
                {
                    _selectedTurno = null;
                }
            }
            ActualizarLabelTurnoSeleccionado();
        }

        private void FiltrarDataGridView(object? sender, EventArgs e)
        {
            if (_isUpdatingCombos) return;
            AplicarFiltros();
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            _isUpdatingCombos = true;
            if (especialidadComboBox.Items.Count > 0) especialidadComboBox.SelectedIndex = 0;
            PoblarComboProfesionales(0);
            if (ProfesionalComboBox.Items.Count > 0) ProfesionalComboBox.SelectedIndex = 0;
            fechaDesdeDateTimePicker.Value = (_selectedTurno != null && _selectedTurno.FechaHoraInicio.Date < DateTime.Today)
                ? _selectedTurno.FechaHoraInicio.Date
                : DateTime.Today;
            _isUpdatingCombos = false;

            AplicarFiltros();
        }

        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is TurnoDisponibleGridRow item)
            {
                _selectedTurno = item.TurnoOriginal;
            }
            else
            {
                _selectedTurno = null;
            }
            ActualizarLabelTurnoSeleccionado();
        }

        private void ActualizarLabelTurnoSeleccionado()
        {
            if (_selectedTurno != null)
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == _selectedTurno.ProfesionalId);
                var esp = prof != null ? _especialidades.FirstOrDefault(e => e.Id == prof.EspecialidadId) : null;
                label6.Text = $"Turno Seleccionado: N° {_selectedTurno.Id} | {_selectedTurno.FechaHoraInicio:dd/MM/yyyy} ({_selectedTurno.FechaHoraInicio:HH:mm} - {_selectedTurno.FechaHoraFin:HH:mm}) | {(prof != null ? $"{prof.Apellido}, {prof.Nombre}" : "Prof. " + _selectedTurno.ProfesionalId)} ({(esp?.Nombre ?? "Sin esp.")})";
            }
            else
            {
                label6.Text = "Ningún turno seleccionado. Haga clic en una fila de la grilla para elegir un turno.";
            }
        }

        // ==========================================
        // INTERACCIÓN REACTIVA ESPECIALIDAD <-> PROFESIONAL
        // ==========================================

        private void EspecialidadComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingCombos) return;

            var especialidadId = ObtenerComboSelectedId(especialidadComboBox);
            var currentProfId = ObtenerComboSelectedId(ProfesionalComboBox);

            PoblarComboProfesionales(especialidadId);

            if (currentProfId > 0)
            {
                var prof = _profesionales.FirstOrDefault(p => p.Id == currentProfId);
                if (prof != null && (especialidadId == 0 || prof.EspecialidadId == especialidadId))
                {
                    _isUpdatingCombos = true;
                    ProfesionalComboBox.SelectedValue = currentProfId;
                    _isUpdatingCombos = false;
                }
            }

            AplicarFiltros();
        }

        private void ProfesionalComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingCombos) return;

            var profesionalId = ObtenerComboSelectedId(ProfesionalComboBox);
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

            AplicarFiltros();
        }

        private static int ObtenerComboSelectedId(ComboBox comboBox)
        {
            if (comboBox.SelectedValue is int id) return id;
            if (comboBox.SelectedValue is EspecialidadDTO esp) return esp.Id;
            if (comboBox.SelectedValue is ProfesionalItem prof) return prof.Id;
            return 0;
        }

        // ==========================================
        // BÚSQUEDA Y GESTIÓN DE PACIENTE (EXISTENTE / NUEVO)
        // ==========================================

        private void BuscarPacienteButton_Click(object? sender, EventArgs e)
        {
            var texto = busquedaTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show("Ingrese un DNI o apellido para buscar el paciente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                busquedaTextBox.Focus();
                return;
            }

            var matching = _pacientes.Where(p =>
                string.Equals(p.NroDocumento, texto, StringComparison.OrdinalIgnoreCase) ||
                p.Apellido.Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                p.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (matching.Count == 0)
            {
                var dr = MessageBox.Show(
                    $"No se encontró ningún paciente con el criterio '{texto}'.",
                    "Paciente no encontrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Question);

                busquedaTextBox.Focus();
                busquedaTextBox.SelectAll();

            }
            else if (matching.Count == 1)
            {
                HabilitarModoSoloLectura(matching[0]);
            }
            else
            {
                var exactDni = matching.FirstOrDefault(p => string.Equals(p.NroDocumento, texto, StringComparison.OrdinalIgnoreCase));
                if (exactDni != null)
                {
                    HabilitarModoSoloLectura(exactDni);
                }
                else
                {
                    HabilitarModoSoloLectura(matching[0]);
                    MessageBox.Show($"Se encontraron {matching.Count} pacientes que coinciden con '{texto}'. Se seleccionó a {matching[0].Apellido}, {matching[0].Nombre}. Ingrese el DNI exacto si desea otro.", "Múltiples Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SeleccionarPaciente(PacienteDTO paciente)
        {
            HabilitarModoSoloLectura(paciente);
        }

        private void HabilitarModoNuevoPaciente(string criterioBusqueda = "")
        {
            _esNuevoPaciente = true;
            _selectedPaciente = null;

            // Desbloquear campos para edición
            nombreTextBox.ReadOnly = false;
            nombreTextBox.BackColor = SystemColors.Window;
            apellidoTextBox.ReadOnly = false;
            apellidoTextBox.BackColor = SystemColors.Window;
            documentoTextBox.ReadOnly = false;
            documentoTextBox.BackColor = SystemColors.Window;
            telefonoTextBox.ReadOnly = false;
            telefonoTextBox.BackColor = SystemColors.Window;
            emailTextBox.ReadOnly = false;
            emailTextBox.BackColor = SystemColors.Window;
            obraSocialTextBox.ReadOnly = false;
            obraSocialTextBox.BackColor = SystemColors.Window;
            fechaNacimientoDateTimePicker.Enabled = true;

            // Limpiar valores
            nombreTextBox.Clear();
            apellidoTextBox.Clear();
            documentoTextBox.Clear();
            telefonoTextBox.Clear();
            emailTextBox.Clear();
            obraSocialTextBox.Clear();
            fechaNacimientoDateTimePicker.Value = DateTime.Today.AddYears(-30);

            // Banner visual "Nuevo Paciente"
            label8.BackColor = Color.FromArgb(235, 243, 255);
            label8.ForeColor = Color.FromArgb(0, 102, 204);
            label8.Text = "Nuevo Paciente: complete los datos para registrarlo y asignar el turno.";

            // Autocomplete inteligente según el criterio de búsqueda ingresado (apellido o documento)
            var criterio = criterioBusqueda.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                if (criterio.All(char.IsDigit))
                {
                    documentoTextBox.Text = criterio;
                    nombreTextBox.Focus();
                }
                else
                {
                    apellidoTextBox.Text = criterio;
                    nombreTextBox.Focus();
                }
            }
            else
            {
                nombreTextBox.Focus();
            }
        }

        private void HabilitarModoSoloLectura(PacienteDTO paciente)
        {
            _esNuevoPaciente = false;
            _selectedPaciente = paciente;

            nombreTextBox.ReadOnly = true;
            nombreTextBox.BackColor = SystemColors.Control;
            apellidoTextBox.ReadOnly = true;
            apellidoTextBox.BackColor = SystemColors.Control;
            documentoTextBox.ReadOnly = true;
            documentoTextBox.BackColor = SystemColors.Control;
            telefonoTextBox.ReadOnly = true;
            telefonoTextBox.BackColor = SystemColors.Control;
            emailTextBox.ReadOnly = true;
            emailTextBox.BackColor = SystemColors.Control;
            obraSocialTextBox.ReadOnly = true;
            obraSocialTextBox.BackColor = SystemColors.Control;
            fechaNacimientoDateTimePicker.Enabled = false;

            nombreTextBox.Text = paciente.Nombre;
            apellidoTextBox.Text = paciente.Apellido;
            documentoTextBox.Text = paciente.NroDocumento;
            telefonoTextBox.Text = paciente.Telefono ?? string.Empty;
            emailTextBox.Text = paciente.Email ?? string.Empty;
            obraSocialTextBox.Text = paciente.ObraSocial ?? string.Empty;
            if (paciente.FechaNacimiento > DateTime.MinValue && paciente.FechaNacimiento <= DateTime.Today)
            {
                fechaNacimientoDateTimePicker.Value = paciente.FechaNacimiento;
            }
            else
            {
                fechaNacimientoDateTimePicker.Value = DateTime.Today.AddYears(-30);
            }

            label8.BackColor = Color.FromArgb(235, 247, 238);
            label8.ForeColor = Color.FromArgb(40, 120, 60);
            label8.Text = $"Paciente seleccionado: {paciente.Apellido}, {paciente.Nombre} (DNI: {paciente.NroDocumento})";
            motivoTextBox.Focus();
        }

        private void LimpiarDatosPaciente()
        {
            _esNuevoPaciente = false;
            _selectedPaciente = null;

            nombreTextBox.ReadOnly = true;
            nombreTextBox.BackColor = SystemColors.Control;
            apellidoTextBox.ReadOnly = true;
            apellidoTextBox.BackColor = SystemColors.Control;
            documentoTextBox.ReadOnly = true;
            documentoTextBox.BackColor = SystemColors.Control;
            telefonoTextBox.ReadOnly = true;
            telefonoTextBox.BackColor = SystemColors.Control;
            emailTextBox.ReadOnly = true;
            emailTextBox.BackColor = SystemColors.Control;
            obraSocialTextBox.ReadOnly = true;
            obraSocialTextBox.BackColor = SystemColors.Control;
            fechaNacimientoDateTimePicker.Enabled = false;

            nombreTextBox.Text = string.Empty;
            apellidoTextBox.Text = string.Empty;
            documentoTextBox.Text = string.Empty;
            telefonoTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            obraSocialTextBox.Text = string.Empty;
            fechaNacimientoDateTimePicker.Value = DateTime.Today.AddYears(-30);

            label8.BackColor = SystemColors.ControlLight;
            label8.ForeColor = SystemColors.ControlDarkDark;
            label8.Text = "Ningún paciente seleccionado.";
        }

        // ==========================================
        // CONFIRMACIÓN Y CANCELACIÓN
        // ==========================================

        private async void ConfirmarButton_Click(object? sender, EventArgs e)
        {
            if (_selectedTurno == null)
            {
                MessageBox.Show("Debe seleccionar un turno disponible de la grilla.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_esNuevoPaciente && _selectedPaciente == null)
            {
                MessageBox.Show("Debe buscar y seleccionar un paciente o registrar uno nuevo para asignar el turno.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                busquedaTextBox.Focus();
                return;
            }

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                PacienteDTO pacienteParaTurno;

                if (_esNuevoPaciente)
                {
                    var nombre = nombreTextBox.Text.Trim();
                    var apellido = apellidoTextBox.Text.Trim();
                    var documento = documentoTextBox.Text.Trim();

                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        MessageBox.Show("El nombre del paciente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nombreTextBox.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(apellido))
                    {
                        MessageBox.Show("El apellido del paciente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        apellidoTextBox.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(documento))
                    {
                        MessageBox.Show("El documento (DNI) del paciente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        documentoTextBox.Focus();
                        return;
                    }

                    if (_pacientes.Any(p => string.Equals(p.NroDocumento, documento, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"Ya existe un paciente registrado con el DNI {documento}.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        documentoTextBox.Focus();
                        return;
                    }

                    var nuevoPaciente = new PacienteDTO
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        TipoDocumento = "DNI",
                        NroDocumento = documento,
                        Telefono = string.IsNullOrWhiteSpace(telefonoTextBox.Text) ? null : telefonoTextBox.Text.Trim(),
                        Email = string.IsNullOrWhiteSpace(emailTextBox.Text) ? null : emailTextBox.Text.Trim(),
                        ObraSocial = obraSocialTextBox.Text.Trim(),
                        FechaNacimiento = fechaNacimientoDateTimePicker.Value.Date
                    };

                    pacienteParaTurno = await PacienteApiClient.AddAsync(nuevoPaciente);
                    _pacientes.Add(pacienteParaTurno);
                    _selectedPaciente = pacienteParaTurno;
                }
                else
                {
                    pacienteParaTurno = _selectedPaciente!;
                }

                if (string.Equals(_selectedTurno.EstadoTurno, "Libre", StringComparison.OrdinalIgnoreCase))
                {
                    await TurnoApiClient.AsignarAsync(_selectedTurno.Id, pacienteParaTurno.Id, motivoTextBox.Text.Trim(), observacionesTextBox.Text.Trim());
                }
                else
                {
                    var turnoActualizado = new TurnoDTO
                    {
                        Id = _selectedTurno.Id,
                        FechaHoraInicio = _selectedTurno.FechaHoraInicio,
                        FechaHoraFin = _selectedTurno.FechaHoraFin,
                        ProfesionalId = _selectedTurno.ProfesionalId,
                        PacienteId = pacienteParaTurno.Id,
                        EstadoTurno = "Asignado",
                        Motivo = motivoTextBox.Text.Trim(),
                        Observaciones = observacionesTextBox.Text.Trim(),
                        FacturaId = _selectedTurno.FacturaId,
                        FechaHoraLlegada = _selectedTurno.FechaHoraLlegada
                    };
                    await TurnoApiClient.UpdateAsync(turnoActualizado);
                }

                var mensaje = _esNuevoPaciente
                    ? $"Paciente '{pacienteParaTurno.Apellido}, {pacienteParaTurno.Nombre}' registrado exitosamente y Turno N° {_selectedTurno.Id} asignado correctamente."
                    : $"Turno N° {_selectedTurno.Id} asignado correctamente a {pacienteParaTurno.Apellido}, {pacienteParaTurno.Nombre}.";

                MessageBox.Show(mensaje, "Asignación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }, this, "Error al procesar la asignación del turno");
        }

        private void CancelarButton_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ==========================================
        // NAVEGACIÓN Y TECLADO
        // ==========================================

        private void BusquedaTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                BuscarPacienteButton_Click(sender, e);
            }
        }

        private void Form_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CancelarButton_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Enter && !observacionesTextBox.Focused && !busquedaTextBox.Focused)
            {
                ConfirmarButton_Click(sender, e);
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

        private class TurnoDisponibleGridRow
        {
            public int Id { get; set; }
            public string Fecha { get; set; } = string.Empty;
            public string HoraInicio { get; set; } = string.Empty;
            public string HoraFin { get; set; } = string.Empty;
            public string EspecialidadNombre { get; set; } = string.Empty;
            public string ProfesionalNombre { get; set; } = string.Empty;
            public TurnoDTO TurnoOriginal { get; set; } = null!;
        }
    }
}
