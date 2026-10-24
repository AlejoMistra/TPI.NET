using API.Clients;
using DTOs;
using System.Data;
using WindowsForms.Helpers;

namespace WindowsForms.DatosMaestros
{
    public partial class TurnoModalForm : Form
    {
        private readonly int? _turnoId;
        private readonly List<ProfesionalDTO> _profesionales = new();
        private readonly List<EspecialidadDTO> _especialidades = new();

        private TurnoDTO? _turnoOriginal;
        private int? _pacienteIdConservado;
        private int? _facturaIdConservado;
        private string? _motivoConservado;
        private string? _observacionesConservadas;

        private bool _isUpdatingCombos = false;

        public TurnoDTO? TurnoGuardado { get; private set; }

        public TurnoModalForm(
            int? turnoId = null,
            IEnumerable<ProfesionalDTO>? profesionales = null,
            IEnumerable<EspecialidadDTO>? especialidades = null)
        {
            _turnoId = turnoId;

            if (profesionales != null)
            {
                _profesionales.AddRange(profesionales);
            }

            if (especialidades != null)
            {
                _especialidades.AddRange(especialidades);
            }

            InitializeComponent();
        }

        private async void TurnoModalForm_Load(object? sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            string requiredPerm = _turnoId.HasValue ? "turnos.actualizar" : "turnos.agregar";
            if (!await authService.HasPermissionAsync(requiredPerm))
            {
                MessageBox.Show("No tiene autorización para realizar esta acción.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await InicializarDatosAsync();

                if (_turnoId.HasValue)
                {
                    this.Text = "Editar Turno";
                    tituloLabel.Text = "Editar Datos del Turno";
                    subtituloLabel.Text = "Actualice la fecha, horario, profesional y estado del turno de atención.";
                    guardarButton.Text = "Guardar Cambios";

                    await CargarDatosTurnoAsync(_turnoId.Value);
                }
                else
                {
                    this.Text = "Nuevo Turno";
                    tituloLabel.Text = "Nuevo Turno";
                    subtituloLabel.Text = "Configure la fecha, horario, profesional y estado del turno de atención.";
                    guardarButton.Text = "Guardar Turno";

                    numeroTurnoTextBox.Text = "(Nuevo)";
                    fechaTurnoDateTimePicker.Value = DateTime.Today;
                    horaInicioDateTimePicker.Value = DateTime.Today.AddHours(8);
                    horaFinDateTimePicker.Value = DateTime.Today.AddHours(8).AddMinutes(30);

                    if (estadoComboBox.Items.Count > 0)
                    {
                        estadoComboBox.SelectedIndex = 0; // "Libre"
                    }
                }
            }, this, "Error al inicializar formulario de turno");
        }

        private async Task InicializarDatosAsync()
        {
            if (_especialidades.Count == 0)
            {
                var especialidadesFromApi = await EspecialidadApiClient.GetAllAsync();
                _especialidades.AddRange(especialidadesFromApi.OrderBy(esp => esp.Nombre));
            }

            if (_profesionales.Count == 0)
            {
                var profesionalesFromApi = await ProfesionalApiClient.GetAllAsync();
                _profesionales.AddRange(profesionalesFromApi.OrderBy(p => p.Apellido).ThenBy(p => p.Nombre));
            }

            // Inicializar estados
            estadoComboBox.Items.Clear();
            estadoComboBox.Items.AddRange(new object[] { "Libre", "Asignado", "Presente", "Atendido", "Ausente" });
            estadoComboBox.SelectedIndex = 0;

            // Inicializar combos reactivos
            PoblarComboEspecialidades();
            PoblarComboProfesionales(0);

            especialidadComboBox.SelectedIndexChanged += EspecialidadComboBox_SelectedIndexChanged;
            profesionalComboBox.SelectedIndexChanged += ProfesionalComboBox_SelectedIndexChanged;
        }

        private void PoblarComboEspecialidades()
        {
            var lista = new List<EspecialidadDTO>
            {
                new EspecialidadDTO { Id = 0, Nombre = "Todas las especialidades" }
            };
            lista.AddRange(_especialidades);

            _isUpdatingCombos = true;
            especialidadComboBox.DataSource = lista;
            especialidadComboBox.DisplayMember = "Nombre";
            especialidadComboBox.ValueMember = "Id";
            especialidadComboBox.SelectedIndex = 0;
            _isUpdatingCombos = false;
        }

        private void PoblarComboProfesionales(int especialidadIdFiltrar)
        {
            var filtrados = especialidadIdFiltrar > 0
                ? _profesionales.Where(p => p.EspecialidadId == especialidadIdFiltrar).ToList()
                : _profesionales;

            var lista = new List<ProfesionalItem>
            {
                new ProfesionalItem { Id = 0, DisplayName = "Seleccionar profesional" }
            };
            lista.AddRange(filtrados.Select(p => new ProfesionalItem
            {
                Id = p.Id,
                DisplayName = $"{p.Apellido}, {p.Nombre} (MP: {p.Matricula})"
            }));

            _isUpdatingCombos = true;
            profesionalComboBox.DataSource = lista;
            profesionalComboBox.DisplayMember = "DisplayName";
            profesionalComboBox.ValueMember = "Id";
            profesionalComboBox.SelectedIndex = 0;
            _isUpdatingCombos = false;
        }

        private async Task CargarDatosTurnoAsync(int id)
        {
            _turnoOriginal = await TurnoApiClient.GetAsync(id);
            if (_turnoOriginal == null)
            {
                MessageBox.Show("No se encontró la información del turno solicitado.", "Turno no encontrado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            _pacienteIdConservado = _turnoOriginal.PacienteId;
            _facturaIdConservado = _turnoOriginal.FacturaId;
            _motivoConservado = _turnoOriginal.Motivo;
            _observacionesConservadas = _turnoOriginal.Observaciones;

            this.Text = $"Editar Turno N° {_turnoOriginal.Id}";
            numeroTurnoTextBox.Text = _turnoOriginal.Id.ToString();

            // Restaurar lista completa de profesionales antes de seleccionar
            PoblarComboProfesionales(0);

            var prof = _profesionales.FirstOrDefault(p => p.Id == _turnoOriginal.ProfesionalId);
            if (prof != null)
            {
                _isUpdatingCombos = true;
                if (prof.EspecialidadId > 0)
                {
                    especialidadComboBox.SelectedValue = prof.EspecialidadId;
                }
                profesionalComboBox.SelectedValue = prof.Id;
                _isUpdatingCombos = false;
            }

            fechaTurnoDateTimePicker.Value = _turnoOriginal.FechaHoraInicio.Date;
            horaInicioDateTimePicker.Value = _turnoOriginal.FechaHoraInicio;
            horaFinDateTimePicker.Value = _turnoOriginal.FechaHoraFin;

            int estadoIdx = estadoComboBox.FindStringExact(_turnoOriginal.EstadoTurno);
            estadoComboBox.SelectedIndex = estadoIdx >= 0 ? estadoIdx : 0;
        }

        private void EspecialidadComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingCombos) return;

            var especialidadId = ObtenerComboSelectedId(especialidadComboBox);
            var currentProfId = ObtenerComboSelectedId(profesionalComboBox);

            PoblarComboProfesionales(especialidadId);

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

        private async void GuardarButton_Click(object? sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            string requiredPerm = _turnoId.HasValue ? "turnos.actualizar" : "turnos.agregar";
            if (!await authService.HasPermissionAsync(requiredPerm))
            {
                MessageBox.Show("No tiene autorización para realizar esta acción.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                var profesionalId = ObtenerComboSelectedId(profesionalComboBox);
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
                    Id = _turnoId ?? 0,
                    FechaHoraInicio = fechaHoraInicio,
                    FechaHoraFin = fechaHoraFin,
                    EstadoTurno = estadoComboBox.SelectedItem?.ToString() ?? "Libre",
                    Observaciones = _observacionesConservadas ?? string.Empty,
                    Motivo = _motivoConservado ?? string.Empty,
                    FacturaId = _facturaIdConservado,
                    ProfesionalId = profesionalId,
                    PacienteId = _pacienteIdConservado
                };

                guardarButton.Enabled = false;
                cancelarButton.Enabled = false;

                try
                {
                    if (_turnoId == null)
                    {
                        TurnoGuardado = await TurnoApiClient.AddAsync(turnoDto);
                        MessageBox.Show("Turno registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        await TurnoApiClient.UpdateAsync(turnoDto);
                        TurnoGuardado = turnoDto;
                        MessageBox.Show("Turno actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                finally
                {
                    if (!this.IsDisposed)
                    {
                        guardarButton.Enabled = true;
                        cancelarButton.Enabled = true;
                    }
                }
            }, this, "Error al guardar el turno");
        }

        private void CancelarButton_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TurnoModalForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private class ProfesionalItem
        {
            public int Id { get; set; }
            public string DisplayName { get; set; } = string.Empty;
        }
    }
}

