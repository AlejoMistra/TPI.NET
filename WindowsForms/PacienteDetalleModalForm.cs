using API.Clients;
using DTOs;
using System.Data;
using WindowsForms.Helpers;

namespace WindowsForms
{
    public partial class PacienteDetalleModalForm : Form
    {
        private readonly int _pacienteId;
        private PacienteDTO? _paciente;
        private HistoriaClinicaDTO? _historiaClinica;
        private List<ProfesionalDTO> _profesionales = new();
        private List<EspecialidadDTO> _especialidades = new();
        private List<TurnoPacienteGridRow> _turnosPaciente = new();
        private List<RegistroClinicoGridRow> _allRegistros = new();
        private bool _isUpdatingFiltro = false;
        private int _selectedTurnoRowIndex = -1;
        private int _selectedRegistroRowIndex = -1;

        public PacienteDetalleModalForm(int pacienteId)
        {
            _pacienteId = pacienteId;
            InitializeComponent();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            ConfigurarFiltroRegistros();
            ConfigurarGrillaTurnos();
            ConfigurarGrillaRegistros();

            this.Load += PacienteDetalleModalForm_Load;
            this.Shown += (s, e) => LimpiarSeleccionGrillas();
            cerrarButton.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.KeyPreview = true;
            this.KeyDown += PacienteDetalleModalForm_KeyDown;
        }

        private void ConfigurarFiltroRegistros()
        {
            _isUpdatingFiltro = true;
            var opciones = new List<TipoRegistroOption>
            {
                new() { Value = "Todos", Display = "Todos los tipos" },
                new() { Value = "NotaClinica", Display = "Nota Clínica" },
                new() { Value = "Diagnostico", Display = "Diagnóstico" },
                new() { Value = "Tratamiento", Display = "Tratamiento" },
                new() { Value = "Evolucion", Display = "Evolución" },
                new() { Value = "Antecedente", Display = "Antecedente" },
                new() { Value = "Alergia", Display = "Alergia" },
                new() { Value = "Otro", Display = "Otro" }
            };

            tipoRegistroFiltroComboBox.DataSource = opciones;
            tipoRegistroFiltroComboBox.DisplayMember = nameof(TipoRegistroOption.Display);
            tipoRegistroFiltroComboBox.ValueMember = nameof(TipoRegistroOption.Value);
            tipoRegistroFiltroComboBox.SelectedIndex = 0;
            _isUpdatingFiltro = false;

            tipoRegistroFiltroComboBox.SelectedIndexChanged += (s, e) =>
            {
                if (!_isUpdatingFiltro) AplicarFiltroRegistros();
            };

            limpiarFiltroRegistrosLinkLabel.LinkClicked += (s, e) =>
            {
                _isUpdatingFiltro = true;
                if (tipoRegistroFiltroComboBox.Items.Count > 0)
                    tipoRegistroFiltroComboBox.SelectedIndex = 0;
                _isUpdatingFiltro = false;
                AplicarFiltroRegistros();
            };
        }

        private void ConfigurarGrillaTurnos()
        {
            turnosPacienteDataGridView.AutoGenerateColumns = false;
            turnosPacienteDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            turnosPacienteDataGridView.MultiSelect = false;
            turnosPacienteDataGridView.ReadOnly = true;
            turnosPacienteDataGridView.RowHeadersVisible = false;
            turnosPacienteDataGridView.AllowUserToAddRows = false;
            turnosPacienteDataGridView.AllowUserToDeleteRows = false;
            turnosPacienteDataGridView.TabStop = false;
            turnosPacienteDataGridView.EnableHeadersVisualStyles = false;
            turnosPacienteDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = turnosPacienteDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            turnosPacienteDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = turnosPacienteDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            turnosPacienteDataGridView.Columns.Clear();

            turnosPacienteDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idTurnoColumn",
                HeaderText = "N° Turno",
                DataPropertyName = nameof(TurnoPacienteGridRow.Id),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosPacienteDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fechaTurnoColumn",
                HeaderText = "Fecha",
                DataPropertyName = nameof(TurnoPacienteGridRow.Fecha),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosPacienteDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horarioTurnoColumn",
                HeaderText = "Horario",
                DataPropertyName = nameof(TurnoPacienteGridRow.Horario),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosPacienteDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "especialidadTurnoColumn",
                HeaderText = "Especialidad",
                DataPropertyName = nameof(TurnoPacienteGridRow.EspecialidadNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosPacienteDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "profesionalTurnoColumn",
                HeaderText = "Profesional",
                DataPropertyName = nameof(TurnoPacienteGridRow.ProfesionalNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosPacienteDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "estadoTurnoColumn",
                HeaderText = "Estado",
                DataPropertyName = nameof(TurnoPacienteGridRow.Estado),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosPacienteDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "motivoTurnoColumn",
                HeaderText = "Motivo / Observaciones",
                DataPropertyName = nameof(TurnoPacienteGridRow.MotivoResumen),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosPacienteDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "verTurnoColumn",
                HeaderText = "",
                Text = "Ver Detalle",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            turnosPacienteDataGridView.CellClick += TurnosPacienteDataGridView_CellClick;
            turnosPacienteDataGridView.CellContentClick += TurnosPacienteDataGridView_CellContentClick;
            turnosPacienteDataGridView.CellDoubleClick += TurnosPacienteDataGridView_CellDoubleClick;
            turnosPacienteDataGridView.CellFormatting += TurnosPacienteDataGridView_CellFormatting;
            turnosPacienteDataGridView.MouseDown += TurnosPacienteDataGridView_MouseDown;
            turnosPacienteDataGridView.SelectionChanged += (s, e) =>
            {
                if (Control.MouseButtons == MouseButtons.None)
                {
                    _selectedTurnoRowIndex = turnosPacienteDataGridView.CurrentRow?.Selected == true
                        ? turnosPacienteDataGridView.CurrentRow.Index
                        : -1;
                }
            };
            turnosPacienteDataGridView.DataBindingComplete += (s, e) => LimpiarSeleccionTurnos();
        }

        private void ConfigurarGrillaRegistros()
        {
            registrosDataGridView.AutoGenerateColumns = false;
            registrosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            registrosDataGridView.MultiSelect = false;
            registrosDataGridView.ReadOnly = true;
            registrosDataGridView.RowHeadersVisible = false;
            registrosDataGridView.AllowUserToAddRows = false;
            registrosDataGridView.AllowUserToDeleteRows = false;
            registrosDataGridView.TabStop = false;
            registrosDataGridView.EnableHeadersVisualStyles = false;
            registrosDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = registrosDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            registrosDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = registrosDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            registrosDataGridView.Columns.Clear();

            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fechaRegistroColumn",
                HeaderText = "Fecha / Hora",
                DataPropertyName = nameof(RegistroClinicoGridRow.FechaTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "tipoRegistroColumn",
                HeaderText = "Tipo",
                DataPropertyName = nameof(RegistroClinicoGridRow.TipoFormateado),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "profesionalRegistroColumn",
                HeaderText = "Profesional",
                DataPropertyName = nameof(RegistroClinicoGridRow.ProfesionalNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "especialidadRegistroColumn",
                HeaderText = "Especialidad",
                DataPropertyName = nameof(RegistroClinicoGridRow.EspecialidadNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "turnoOrigenColumn",
                HeaderText = "Turno Origen",
                DataPropertyName = nameof(RegistroClinicoGridRow.TurnoOrigenTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            registrosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "descripcionRegistroColumn",
                HeaderText = "Descripción Clínica",
                DataPropertyName = nameof(RegistroClinicoGridRow.Descripcion),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    WrapMode = DataGridViewTriState.True
                }
            });

            registrosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "verRegistroColumn",
                HeaderText = "",
                Text = "Ver",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            registrosDataGridView.CellClick += RegistrosDataGridView_CellClick;
            registrosDataGridView.CellContentClick += RegistrosDataGridView_CellContentClick;
            registrosDataGridView.CellDoubleClick += RegistrosDataGridView_CellDoubleClick;
            registrosDataGridView.CellFormatting += RegistrosDataGridView_CellFormatting;
            registrosDataGridView.MouseDown += RegistrosDataGridView_MouseDown;
            registrosDataGridView.SelectionChanged += (s, e) =>
            {
                if (Control.MouseButtons == MouseButtons.None)
                {
                    _selectedRegistroRowIndex = registrosDataGridView.CurrentRow?.Selected == true
                        ? registrosDataGridView.CurrentRow.Index
                        : -1;
                }
            };
            registrosDataGridView.DataBindingComplete += (s, e) => LimpiarSeleccionRegistros();
        }

        // ==========================================
        // CARGA ASINCRÓNICA DE DATOS
        // ==========================================

        private async void PacienteDetalleModalForm_Load(object? sender, EventArgs e)
        {
            bool ok = await UiErrorHandler.ExecuteAsync(async () =>
            {
                _paciente = await PacienteApiClient.GetAsync(_pacienteId);
                if (_paciente == null)
                {
                    MessageBox.Show("No se encontró el paciente seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                var profesionalesTask = ProfesionalApiClient.GetAllAsync();
                var especialidadesTask = EspecialidadApiClient.GetAllAsync();
                var turnosTask = TurnoApiClient.GetAllAsync();
                var hcTask = HistoriaClinicaApiClient.GetByPacienteIdAsync(_pacienteId);

                await Task.WhenAll(profesionalesTask, especialidadesTask, turnosTask, hcTask);

                _profesionales = (await profesionalesTask).ToList();
                _especialidades = (await especialidadesTask).ToList();
                _historiaClinica = await hcTask;
                var todosLosTurnos = (await turnosTask).Where(t => t.PacienteId == _pacienteId).ToList();

                PoblarDatosPacienteYCabecera();
                PoblarTurnosPaciente(todosLosTurnos);
                PoblarRegistrosClinicos(todosLosTurnos);

                LimpiarSeleccionGrillas();
                if (this.IsHandleCreated)
                {
                    BeginInvoke(new Action(LimpiarSeleccionGrillas));
                }
            }, this, "Error al cargar el detalle del paciente");

            if (!ok && _paciente == null)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void PoblarDatosPacienteYCabecera()
        {
            var p = _paciente!;
            this.Text = $"Detalle de Paciente — {p.Apellido}, {p.Nombre} (DNI: {p.NroDocumento})";

            pacienteValueLabel.Text = $"{p.Apellido}, {p.Nombre} (#{p.Id})";
            documentoValueLabel.Text = $"{(string.IsNullOrWhiteSpace(p.TipoDocumento) ? "DNI" : p.TipoDocumento)} {p.NroDocumento}";
            fechaNacimientoValueLabel.Text = FormatearFechaNacimientoYEdad(p.FechaNacimiento);
            obraSocialValueLabel.Text = string.IsNullOrWhiteSpace(p.ObraSocial) ? "Particular" : p.ObraSocial;
            telefonoValueLabel.Text = string.IsNullOrWhiteSpace(p.Telefono) ? "—" : p.Telefono;
            emailValueLabel.Text = string.IsNullOrWhiteSpace(p.Email) ? "—" : p.Email;

            if (_historiaClinica != null)
            {
                var fechaLocal = _historiaClinica.FechaCreacion.Kind == DateTimeKind.Utc
                    ? _historiaClinica.FechaCreacion.ToLocalTime()
                    : _historiaClinica.FechaCreacion;

                nroHcValueLabel.Text = $"#{_historiaClinica.Id:D5} (Apertura: {fechaLocal:dd/MM/yyyy})";
                grupoSanguineoValueLabel.Text = FormatearGrupoSanguineo(_historiaClinica.GrupoSanguineo);
                totalRegistrosValueLabel.Text = $"{_historiaClinica.Registros.Count} registro(s)";
            }
            else
            {
                nroHcValueLabel.Text = "Sin historia clínica";
                grupoSanguineoValueLabel.Text = "No especificado";
                totalRegistrosValueLabel.Text = "0 registros";
            }
        }

        private void PoblarTurnosPaciente(List<TurnoDTO> turnos)
        {
            _turnosPaciente = turnos
                .OrderByDescending(t => t.FechaHoraInicio)
                .Select(t =>
                {
                    var prof = _profesionales.FirstOrDefault(pr => pr.Id == t.ProfesionalId);
                    var esp = prof != null ? _especialidades.FirstOrDefault(e => e.Id == prof.EspecialidadId) : null;

                    return new TurnoPacienteGridRow
                    {
                        Id = t.Id,
                        Fecha = t.FechaHoraInicio.ToString("dd/MM/yyyy"),
                        Horario = $"{t.FechaHoraInicio:HH:mm} - {t.FechaHoraFin:HH:mm}",
                        ProfesionalId = t.ProfesionalId,
                        ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : $"ID {t.ProfesionalId}",
                        EspecialidadNombre = esp?.Nombre ?? "—",
                        Estado = t.EstadoTurno,
                        Motivo = t.Motivo ?? string.Empty,
                        Observaciones = t.Observaciones ?? string.Empty,
                        FechaHoraInicio = t.FechaHoraInicio,
                        FechaHoraFin = t.FechaHoraFin,
                        FechaHoraLlegada = t.FechaHoraLlegada,
                        FacturaId = t.FacturaId
                    };
                })
                .ToList();

            // Resumen: Último Turno Atendido (más reciente en estado Atendido)
            var ultimoAtendido = _turnosPaciente
                .Where(t => string.Equals(t.Estado, "Atendido", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(t => t.FechaHoraInicio)
                .FirstOrDefault();

            if (ultimoAtendido != null)
            {
                ultimoTurnoValueLabel.Text = $"{ultimoAtendido.Fecha} ({ultimoAtendido.Horario}) — {ultimoAtendido.ProfesionalNombre} ({ultimoAtendido.EspecialidadNombre})";
                ultimoTurnoValueLabel.ForeColor = Color.FromArgb(40, 120, 60);
            }
            else
            {
                ultimoTurnoValueLabel.Text = "Sin atenciones previas registradas";
                ultimoTurnoValueLabel.ForeColor = SystemColors.ControlDarkDark;
            }

            // Resumen: Próximo Turno Asignado / Presente
            var proximoAsignado = _turnosPaciente
                .Where(t =>
                    string.Equals(t.Estado, "Asignado", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(t.Estado, "Presente", StringComparison.OrdinalIgnoreCase))
                .OrderBy(t => t.FechaHoraInicio >= DateTime.Today ? 0 : 1)
                .ThenBy(t => t.FechaHoraInicio)
                .FirstOrDefault();

            if (proximoAsignado != null)
            {
                proximoTurnoValueLabel.Text = $"{proximoAsignado.Fecha} ({proximoAsignado.Horario}) — {proximoAsignado.ProfesionalNombre} [{proximoAsignado.Estado}]";
                proximoTurnoValueLabel.ForeColor = Color.FromArgb(0, 102, 204);
            }
            else
            {
                proximoTurnoValueLabel.Text = "Sin turnos asignados pendientes";
                proximoTurnoValueLabel.ForeColor = SystemColors.ControlDarkDark;
            }

            turnosGroupBox.Text = $"Turnos del Paciente ({_turnosPaciente.Count}) — Últimas Atenciones y Turnos Asignados";
            turnosPacienteDataGridView.DataSource = _turnosPaciente;
            LimpiarSeleccionTurnos();
        }

        private void PoblarRegistrosClinicos(List<TurnoDTO> turnosPaciente)
        {
            if (_historiaClinica == null || _historiaClinica.Registros.Count == 0)
            {
                _allRegistros = new List<RegistroClinicoGridRow>();
                AplicarFiltroRegistros();
                return;
            }

            var turnosPorId = turnosPaciente.ToDictionary(t => t.Id, t => t);

            _allRegistros = _historiaClinica.Registros
                .OrderByDescending(r => r.Fecha)
                .Select(r =>
                {
                    var prof = _profesionales.FirstOrDefault(p => p.Id == r.ProfesionalId);
                    var esp = prof != null ? _especialidades.FirstOrDefault(e => e.Id == prof.EspecialidadId) : null;
                    var fechaLocal = r.Fecha.Kind == DateTimeKind.Utc ? r.Fecha.ToLocalTime() : r.Fecha;

                    string turnoTexto = "General";
                    if (r.TurnoId.HasValue)
                    {
                        if (turnosPorId.TryGetValue(r.TurnoId.Value, out var turnoOrig))
                        {
                            turnoTexto = $"Turno #{turnoOrig.Id} ({turnoOrig.FechaHoraInicio:dd/MM/yyyy})";
                        }
                        else
                        {
                            turnoTexto = $"Turno #{r.TurnoId.Value}";
                        }
                    }

                    return new RegistroClinicoGridRow
                    {
                        Id = r.Id,
                        Fecha = fechaLocal,
                        FechaTexto = fechaLocal.ToString("dd/MM/yyyy HH:mm"),
                        TipoRaw = r.Tipo,
                        TipoFormateado = FormatearTipoRegistro(r.Tipo),
                        ProfesionalId = r.ProfesionalId,
                        ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : $"ID {r.ProfesionalId}",
                        EspecialidadNombre = esp?.Nombre ?? "—",
                        TurnoId = r.TurnoId,
                        TurnoOrigenTexto = turnoTexto,
                        Descripcion = r.Descripcion
                    };
                })
                .ToList();

            AplicarFiltroRegistros();
        }

        private void AplicarFiltroRegistros()
        {
            var tipoSeleccionado = tipoRegistroFiltroComboBox.SelectedValue?.ToString() ?? "Todos";

            var filtrados = _allRegistros.AsEnumerable();
            if (!string.Equals(tipoSeleccionado, "Todos", StringComparison.OrdinalIgnoreCase))
            {
                filtrados = filtrados.Where(r => string.Equals(r.TipoRaw, tipoSeleccionado, StringComparison.OrdinalIgnoreCase));
            }

            var lista = filtrados.ToList();
            historiaClinicaGroupBox.Text = $"Historia Clínica — Registros Clínicos ({lista.Count} de {_allRegistros.Count})";
            registrosDataGridView.DataSource = lista;
            LimpiarSeleccionRegistros();
        }

        private void LimpiarSeleccionGrillas()
        {
            LimpiarSeleccionTurnos();
            LimpiarSeleccionRegistros();
        }

        private void LimpiarSeleccionTurnos()
        {
            turnosPacienteDataGridView.ClearSelection();
            turnosPacienteDataGridView.CurrentCell = null;
            _selectedTurnoRowIndex = -1;
        }

        private void LimpiarSeleccionRegistros()
        {
            registrosDataGridView.ClearSelection();
            registrosDataGridView.CurrentCell = null;
            _selectedRegistroRowIndex = -1;
        }

        // ==========================================
        // EVENTOS DE GRILLAS Y DETALLES
        // ==========================================

        private void TurnosPacienteDataGridView_MouseDown(object? sender, MouseEventArgs e)
        {
            var hit = turnosPacienteDataGridView.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.None || hit.Type == DataGridViewHitTestType.ColumnHeader)
            {
                LimpiarSeleccionTurnos();
            }
        }

        private void TurnosPacienteDataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex >= 0 && turnosPacienteDataGridView.Columns[e.ColumnIndex].Name == "verTurnoColumn")
            {
                _selectedTurnoRowIndex = e.RowIndex;
                return;
            }

            if (_selectedTurnoRowIndex == e.RowIndex)
            {
                LimpiarSeleccionTurnos();
            }
            else
            {
                _selectedTurnoRowIndex = e.RowIndex;
            }
        }

        private void TurnosPacienteDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (turnosPacienteDataGridView.Columns[e.ColumnIndex].Name == "verTurnoColumn")
            {
                if (turnosPacienteDataGridView.Rows[e.RowIndex].DataBoundItem is TurnoPacienteGridRow row)
                {
                    MostrarDetalleTurno(row);
                }
            }
        }

        private void TurnosPacienteDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (turnosPacienteDataGridView.Rows[e.RowIndex].DataBoundItem is TurnoPacienteGridRow row)
            {
                MostrarDetalleTurno(row);
            }
        }

        private void MostrarDetalleTurno(TurnoPacienteGridRow row)
        {
            var llegadaInfo = row.FechaHoraLlegada.HasValue
                ? $"\nHora de Llegada: {row.FechaHoraLlegada.Value:dd/MM/yyyy HH:mm}"
                : string.Empty;

            var facturaInfo = row.FacturaId.HasValue
                ? $"\nFacturación: Facturado (Factura N° {row.FacturaId.Value:D5})"
                : "\nFacturación: Pendiente";

            MessageBox.Show(
                $"Turno N° {row.Id}\n\n" +
                $"Fecha: {row.Fecha} ({row.Horario})\n" +
                $"Especialidad: {row.EspecialidadNombre}\n" +
                $"Profesional: {row.ProfesionalNombre}\n" +
                $"Estado: {row.Estado}\n" +
                $"Motivo: {(string.IsNullOrWhiteSpace(row.Motivo) ? "— Sin motivo —" : row.Motivo)}\n" +
                $"Observaciones: {(string.IsNullOrWhiteSpace(row.Observaciones) ? "— Sin observaciones —" : row.Observaciones)}" +
                llegadaInfo +
                facturaInfo,
                $"Detalle del Turno N° {row.Id}",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void RegistrosDataGridView_MouseDown(object? sender, MouseEventArgs e)
        {
            var hit = registrosDataGridView.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.None || hit.Type == DataGridViewHitTestType.ColumnHeader)
            {
                LimpiarSeleccionRegistros();
            }
        }

        private void RegistrosDataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex >= 0 && registrosDataGridView.Columns[e.ColumnIndex].Name == "verRegistroColumn")
            {
                _selectedRegistroRowIndex = e.RowIndex;
                return;
            }

            if (_selectedRegistroRowIndex == e.RowIndex)
            {
                LimpiarSeleccionRegistros();
            }
            else
            {
                _selectedRegistroRowIndex = e.RowIndex;
            }
        }

        private void RegistrosDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (registrosDataGridView.Columns[e.ColumnIndex].Name == "verRegistroColumn")
            {
                if (registrosDataGridView.Rows[e.RowIndex].DataBoundItem is RegistroClinicoGridRow row)
                {
                    MostrarDetalleRegistro(row);
                }
            }
        }

        private void RegistrosDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (registrosDataGridView.Rows[e.RowIndex].DataBoundItem is RegistroClinicoGridRow row)
            {
                MostrarDetalleRegistro(row);
            }
        }

        private void MostrarDetalleRegistro(RegistroClinicoGridRow row)
        {
            MessageBox.Show(
                $"Registro Clínico #{row.Id}\n\n" +
                $"Fecha / Hora: {row.FechaTexto}\n" +
                $"Tipo: {row.TipoFormateado}\n" +
                $"Profesional: {row.ProfesionalNombre} ({row.EspecialidadNombre})\n" +
                $"Origen: {row.TurnoOrigenTexto}\n\n" +
                $"Descripción:\n{row.Descripcion}",
                $"Detalle de Registro Clínico — {row.TipoFormateado}",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void TurnosPacienteDataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= turnosPacienteDataGridView.Rows.Count) return;
            var row = turnosPacienteDataGridView.Rows[e.RowIndex].DataBoundItem as TurnoPacienteGridRow;
            if (row == null || e.CellStyle == null) return;

            if (string.Equals(row.Estado, "Asignado", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(row.Estado, "Presente", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(235, 243, 255);
                e.CellStyle.ForeColor = Color.FromArgb(0, 82, 164);
            }
            else if (string.Equals(row.Estado, "Atendido", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(238, 248, 240);
                e.CellStyle.ForeColor = Color.FromArgb(25, 95, 45);
            }
            else if (string.Equals(row.Estado, "Ausente", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 238, 238);
                e.CellStyle.ForeColor = Color.DarkRed;
            }
        }

        private void RegistrosDataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= registrosDataGridView.Rows.Count) return;
            var row = registrosDataGridView.Rows[e.RowIndex].DataBoundItem as RegistroClinicoGridRow;
            if (row == null || e.CellStyle == null) return;

            if (string.Equals(row.TipoRaw, "Alergia", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 242, 238);
                e.CellStyle.ForeColor = Color.FromArgb(165, 40, 20);
            }
        }

        private void PacienteDetalleModalForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // ==========================================
        // FORMATEADORES Y CLASES AUXILIARES
        // ==========================================

        public static string FormatearFechaNacimientoYEdad(DateTime fechaNacimiento)
        {
            if (fechaNacimiento <= new DateTime(1900, 1, 1) || fechaNacimiento > DateTime.Today)
                return "—";

            var hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad))
                edad--;

            return $"{fechaNacimiento:dd/MM/yyyy} ({edad} años)";
        }

        public static string FormatearGrupoSanguineo(string? grupo)
        {
            if (string.IsNullOrWhiteSpace(grupo))
                return "No especificado";

            return grupo.ToUpperInvariant() switch
            {
                "A_POSITIVO" => "A+",
                "A_NEGATIVO" => "A-",
                "B_POSITIVO" => "B+",
                "B_NEGATIVO" => "B-",
                "AB_POSITIVO" => "AB+",
                "AB_NEGATIVO" => "AB-",
                "O_POSITIVO" => "O+",
                "O_NEGATIVO" => "O-",
                "NO_ESPECIFICADO" => "No especificado",
                _ => grupo
            };
        }

        public static string FormatearTipoRegistro(string? tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                return "—";

            return tipo switch
            {
                "NotaClinica" => "Nota Clínica",
                "Diagnostico" => "Diagnóstico",
                "Evolucion" => "Evolución",
                "Alergia" => "Alergia",
                "Antecedente" => "Antecedente",
                "Tratamiento" => "Tratamiento",
                "Otro" => "Otro",
                _ => tipo
            };
        }

        private class TipoRegistroOption
        {
            public string Value { get; set; } = string.Empty;
            public string Display { get; set; } = string.Empty;
        }

        public class TurnoPacienteGridRow
        {
            public int Id { get; set; }
            public string Fecha { get; set; } = string.Empty;
            public string Horario { get; set; } = string.Empty;
            public int ProfesionalId { get; set; }
            public string ProfesionalNombre { get; set; } = string.Empty;
            public string EspecialidadNombre { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
            public string Motivo { get; set; } = string.Empty;
            public string Observaciones { get; set; } = string.Empty;
            public DateTime FechaHoraInicio { get; set; }
            public DateTime FechaHoraFin { get; set; }
            public DateTime? FechaHoraLlegada { get; set; }
            public int? FacturaId { get; set; }

            public string MotivoResumen
            {
                get
                {
                    if (!string.IsNullOrWhiteSpace(Motivo) && !string.IsNullOrWhiteSpace(Observaciones))
                        return $"{Motivo} — {Observaciones}";
                    if (!string.IsNullOrWhiteSpace(Motivo))
                        return Motivo;
                    if (!string.IsNullOrWhiteSpace(Observaciones))
                        return Observaciones;
                    return "—";
                }
            }
        }

        public class RegistroClinicoGridRow
        {
            public int Id { get; set; }
            public DateTime Fecha { get; set; }
            public string FechaTexto { get; set; } = string.Empty;
            public string TipoRaw { get; set; } = string.Empty;
            public string TipoFormateado { get; set; } = string.Empty;
            public int ProfesionalId { get; set; }
            public string ProfesionalNombre { get; set; } = string.Empty;
            public string EspecialidadNombre { get; set; } = string.Empty;
            public int? TurnoId { get; set; }
            public string TurnoOrigenTexto { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
        }
    }
}
