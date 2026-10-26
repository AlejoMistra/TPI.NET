using API.Clients;
using DTOs;
using WindowsForms.Helpers;
using TurnoPacienteGridRow = WindowsForms.HistoriaClinicaModalForm.TurnoPacienteGridRow;

namespace WindowsForms
{
    public partial class NuevoRegistroClinicoModalForm : Form
    {
        private readonly PacienteDTO _paciente;
        private HistoriaClinicaDTO? _historiaClinica;
        private readonly List<ProfesionalDTO> _profesionales;
        private readonly List<TurnoPacienteGridRow> _turnosPaciente;
        private readonly TurnoPacienteGridRow? _turnoPreseleccionado;

        public HistoriaClinicaDTO? HistoriaClinicaActualizada => _historiaClinica;

        public NuevoRegistroClinicoModalForm(
            PacienteDTO paciente,
            HistoriaClinicaDTO? historiaClinica,
            List<ProfesionalDTO> profesionales,
            List<TurnoPacienteGridRow> turnosPaciente,
            TurnoPacienteGridRow? turnoPreseleccionado = null)
        {
            _paciente = paciente ?? throw new ArgumentNullException(nameof(paciente));
            _historiaClinica = historiaClinica;
            _profesionales = profesionales ?? new List<ProfesionalDTO>();
            _turnosPaciente = turnosPaciente ?? new List<TurnoPacienteGridRow>();
            _turnoPreseleccionado = turnoPreseleccionado;

            InitializeComponent();
            ConfigurarControles();
            this.Load += NuevoRegistroClinicoModalForm_Load;
        }

        private async void NuevoRegistroClinicoModalForm_Load(object? sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("historiasclinicas.agregar"))
            {
                MessageBox.Show("No tiene autorización para registrar notas clínicas.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
        }

        private void ConfigurarControles()
        {
            PoblarCabeceraPaciente();
            ConfigurarComboTiposRegistro();
            ConfigurarComboTurnos();
        }

        private void PoblarCabeceraPaciente()
        {
            // Iniciales del avatar (ej: CG para Carlos García)
            string inicialNombre = !string.IsNullOrWhiteSpace(_paciente.Nombre) ? _paciente.Nombre.Trim().Substring(0, 1).ToUpperInvariant() : "";
            string inicialApellido = !string.IsNullOrWhiteSpace(_paciente.Apellido) ? _paciente.Apellido.Trim().Substring(0, 1).ToUpperInvariant() : "";
            avatarLabel.Text = $"{inicialNombre}{inicialApellido}".Trim();
            if (string.IsNullOrEmpty(avatarLabel.Text)) avatarLabel.Text = "P";

            pacienteNombreLabel.Text = $"{_paciente.Apellido}, {_paciente.Nombre}";
            string docTipo = string.IsNullOrWhiteSpace(_paciente.TipoDocumento) ? "DNI" : _paciente.TipoDocumento;
            string os = string.IsNullOrWhiteSpace(_paciente.ObraSocial) ? "Particular" : _paciente.ObraSocial;
            pacienteMetaLabel.Text = $"{docTipo}: {_paciente.NroDocumento} • Obra Social: {os}";

            string hcTexto = _historiaClinica != null ? $"#HC-{_historiaClinica.Id:D5}" : "Sin historia clínica";
            this.Text = $"Nuevo Registro Clínico - Historia Clínica {hcTexto}";

            fechaDateTimePicker.Value = DateTime.Now;
        }

        private void ConfigurarComboTiposRegistro()
        {
            var opciones = new List<TipoRegistroItem>
            {
                new()
                {
                    Value = "Evolucion",
                    Display = "Evolución médica",
                    Descripcion = "Evolución: Registra la actualización clínica del cuadro, hallazgos semiológicos y respuesta al tratamiento."
                },
                new()
                {
                    Value = "NotaClinica",
                    Display = "Nota clínica",
                    Descripcion = "Nota clínica: Observaciones generales, comentarios o anotaciones administrativas-asistenciales."
                },
                new()
                {
                    Value = "Diagnostico",
                    Display = "Diagnóstico",
                    Descripcion = "Diagnóstico: Registra diagnósticos presuntivos, sindrómicos o de certeza del paciente."
                },
                new()
                {
                    Value = "Tratamiento",
                    Display = "Tratamiento",
                    Descripcion = "Tratamiento: Terapéutica indicada, plan farmacológico, posología y recomendaciones."
                },
                new()
                {
                    Value = "Antecedente",
                    Display = "Antecedente",
                    Descripcion = "Antecedente: Historia patológica previa, quirúrgica, familiar o factores de riesgo."
                },
                new()
                {
                    Value = "Alergia",
                    Display = "Alergia (Alergia)",
                    Descripcion = "Alergia: Registra reacciones adversas o hipersensibilidad a medicamentos o sustancias."
                },
                new()
                {
                    Value = "Otro",
                    Display = "Otro",
                    Descripcion = "Otro: Registro de índole clínica general no clasificado en las categorías anteriores."
                }
            };

            tipoRegistroComboBox.DataSource = opciones;
            tipoRegistroComboBox.DisplayMember = nameof(TipoRegistroItem.Display);
            tipoRegistroComboBox.ValueMember = nameof(TipoRegistroItem.Value);

            tipoRegistroComboBox.SelectedIndex = 0;
            tipoInfoTextLabel.Text = opciones[0].Descripcion;
        }

        private void ConfigurarComboTurnos()
        {
            var turnosItems = new List<TurnoComboItem>
            {
                new() { TurnoId = null, ProfesionalId = null, Display = "[Sin turno asociado] (Registro general independiente)" }
            };

            // Regla de dominio: Solo turnos en estado 'Atendido' son elegibles para registrar datos clínicos
            var turnosAtendidos = _turnosPaciente
                .Where(t => string.Equals(t.Estado, "Atendido", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(t => t.FechaHoraInicio);

            foreach (var t in turnosAtendidos)
            {
                string detalle = !string.IsNullOrWhiteSpace(t.Motivo) ? t.Motivo : t.EspecialidadNombre;
                turnosItems.Add(new TurnoComboItem
                {
                    TurnoId = t.Id,
                    ProfesionalId = t.ProfesionalId,
                    Display = $"[#TRN-{t.Id}] {t.Fecha} ({t.Horario}) — {detalle} — {t.ProfesionalNombre} (Atendido)"
                });
            }

            turnoOrigenComboBox.DataSource = turnosItems;
            turnoOrigenComboBox.DisplayMember = nameof(TurnoComboItem.Display);
            turnoOrigenComboBox.ValueMember = nameof(TurnoComboItem.TurnoId);

            // Si viene un turno preseleccionado y está Atendido, seleccionarlo
            if (_turnoPreseleccionado != null &&
                string.Equals(_turnoPreseleccionado.Estado, "Atendido", StringComparison.OrdinalIgnoreCase))
            {
                var match = turnosItems.FirstOrDefault(item => item.TurnoId == _turnoPreseleccionado.Id);
                if (match != null)
                {
                    turnoOrigenComboBox.SelectedItem = match;
                }
                else
                {
                    turnoOrigenComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                turnoOrigenComboBox.SelectedIndex = 0;
            }
        }

        private void tipoRegistroComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tipoRegistroComboBox.SelectedItem is TipoRegistroItem item)
            {
                tipoInfoTextLabel.Text = item.Descripcion;
            }
        }

        private void desvincularTurnoButton_Click(object? sender, EventArgs e)
        {
            if (turnoOrigenComboBox.Items.Count > 0)
            {
                turnoOrigenComboBox.SelectedIndex = 0;
            }
        }

        private void descripcionTextBox_TextChanged(object? sender, EventArgs e)
        {
            int len = descripcionTextBox.Text.Length;
            caracteresCounterLabel.Text = $"{len} / 500 car.";
            caracteresCounterLabel.ForeColor = len >= 480 ? Color.DarkRed : Color.Gray;
        }

        private void limpiarButton_Click(object? sender, EventArgs e)
        {
            descripcionTextBox.Clear();
            if (turnoOrigenComboBox.Items.Count > 0)
                turnoOrigenComboBox.SelectedIndex = 0;
            if (tipoRegistroComboBox.Items.Count > 0)
                tipoRegistroComboBox.SelectedIndex = 0;
            descripcionTextBox.Focus();
        }

        private void cancelarButton_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void guardarButton_Click(object? sender, EventArgs e)
        {
            string descripcion = descripcionTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show(
                    "La descripción del registro clínico es obligatoria.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                descripcionTextBox.Focus();
                return;
            }

            if (descripcion.Length > 500)
            {
                MessageBox.Show(
                    $"La descripción excede el límite permitido de 500 caracteres (longitud actual: {descripcion.Length}).",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                descripcionTextBox.Focus();
                return;
            }

            if (tipoRegistroComboBox.SelectedItem is not TipoRegistroItem tipoSeleccionado)
            {
                MessageBox.Show(
                    "Debe seleccionar un tipo de registro clínico válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var turnoSeleccionado = turnoOrigenComboBox.SelectedItem as TurnoComboItem;
            int? turnoId = turnoSeleccionado?.TurnoId;

            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("historiasclinicas.agregar"))
            {
                MessageBox.Show("No tiene autorización para agregar registros clínicos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? profesionalLogueadoId = null;
            if (await authService.IsInRoleAsync("Profesional"))
            {
                profesionalLogueadoId = await authService.GetPersonaIdAsync();
            }

            // Determinar ProfesionalId:
            // Si hay turno de origen, el backend exige turno.ProfesionalId == profesionalId.
            // Si es registro general (sin turno), se utiliza el profesional logueado o el activo por defecto.
            int profesionalId;
            if (turnoId.HasValue && turnoSeleccionado?.ProfesionalId.HasValue == true)
            {
                profesionalId = turnoSeleccionado.ProfesionalId.Value;
            }
            else if (profesionalLogueadoId.HasValue && profesionalLogueadoId.Value > 0)
            {
                profesionalId = profesionalLogueadoId.Value;
            }
            else
            {
                var profActivo = _profesionales.FirstOrDefault(p => string.Equals(p.Estado, "Activo", StringComparison.OrdinalIgnoreCase))
                                 ?? _profesionales.FirstOrDefault();
                profesionalId = profActivo?.Id ?? 1;
            }

            SetGuardando(true);

            bool exito = await UiErrorHandler.ExecuteAsync(async () =>
            {
                // Si el paciente aún no tiene Historia Clínica, se crea automáticamente
                if (_historiaClinica == null)
                {
                    _historiaClinica = await HistoriaClinicaApiClient.CreateAsync(new HistoriaClinicaCreateDTO
                    {
                        PacienteId = _paciente.Id,
                        GrupoSanguineo = "NO_ESPECIFICADO"
                    });
                }

                var dto = new RegistroClinicoCreateDTO
                {
                    Tipo = tipoSeleccionado.Value,
                    Descripcion = descripcion,
                    ProfesionalId = profesionalId,
                    TurnoId = turnoId
                };

                await HistoriaClinicaApiClient.AddRegistroAsync(_historiaClinica.Id, dto);

                MessageBox.Show(
                    "Registro clínico guardado exitosamente.",
                    "Registro Clínico Guardado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }, this, "Error al guardar el registro clínico");

            if (!exito)
            {
                SetGuardando(false);
            }
        }

        private void NuevoRegistroClinicoModalForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (e.KeyCode == Keys.F5)
            {
                guardarButton.PerformClick();
            }
        }

        private void SetGuardando(bool guardando)
        {
            guardarButton.Enabled = !guardando;
            cancelarButton.Enabled = !guardando;
            limpiarButton.Enabled = !guardando;
            desvincularTurnoButton.Enabled = !guardando;
            tipoRegistroComboBox.Enabled = !guardando;
            turnoOrigenComboBox.Enabled = !guardando;
            descripcionTextBox.ReadOnly = guardando;
            this.Cursor = guardando ? Cursors.WaitCursor : Cursors.Default;
        }

        private class TipoRegistroItem
        {
            public string Value { get; set; } = string.Empty;
            public string Display { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
        }

        private class TurnoComboItem
        {
            public int? TurnoId { get; set; }
            public int? ProfesionalId { get; set; }
            public string Display { get; set; } = string.Empty;
        }
    }
}