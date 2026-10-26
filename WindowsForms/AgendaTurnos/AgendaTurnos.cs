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
        private List<EspecialidadDTO> _especialidades = new();
        private List<PacienteDTO> _pacientes = new();
        private bool _isUpdatingBusquedaCombos = false;

        private ContextMenuStrip _agendaContextMenu = null!;
        private ToolStripMenuItem _llegadaMenuItem = null!;
        private ToolStripMenuItem _revertirLlegadaMenuItem = null!;
        private ToolStripMenuItem _ausenteMenuItem = null!;
        private ToolStripMenuItem _atenderMenuItem = null!;
        private ToolStripMenuItem _liberarMenuItem = null!;
        private ToolStripMenuItem _detalleMenuItem = null!;
        private ToolStripMenuItem _editarMenuItem = null!;
        private ToolStripMenuItem _facturarMenuItem = null!;
        private ToolStripSeparator _sep1 = null!;
        private ToolStripSeparator _sep2 = null!;
        private ToolStripSeparator _sep3 = null!;

        public AgendaTurnos()
        {
            InitializeComponent();
            ConfigurarColumnas();
            ConfigurarContextMenu();

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
                Name = "estadoColumn",
                HeaderText = "Estado",
                DataPropertyName = nameof(AgendaGridRow.Estado),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "llegadaColumn",
                HeaderText = "Llegada",
                DataPropertyName = nameof(AgendaGridRow.HoraLlegada),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "motivoColumn",
                HeaderText = "Motivo",
                DataPropertyName = nameof(AgendaGridRow.Motivo),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "especialidadColumn",
                HeaderText = "Especialidad",
                DataPropertyName = nameof(AgendaGridRow.EspecialidadNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "profesionalColumn",
                HeaderText = "Profesional",
                DataPropertyName = nameof(AgendaGridRow.ProfesionalNombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "detalleColumn",
                HeaderText = "",
                Text = "Ver Detalle",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "atenderColumn",
                HeaderText = "",
                Text = "Atender",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "editarColumn",
                HeaderText = "",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "liberarColumn",
                HeaderText = "",
                Text = "Liberar",
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
                await ConfigurePermissionsAsync();
                await CargarEspecialidadesAsync();
                await CargarProfesionalesAsync();
                await CargarPacientesAsync();
                await CargarAgendaAsync();
            }, this, "Error al cargar la agenda de turnos");
        }

        private async Task ConfigurePermissionsAsync()
        {
            var authService = AuthServiceProvider.Instance;
            bool canAdd = await authService.HasPermissionAsync("turnos.agregar");
            bool canUpdate = await authService.HasPermissionAsync("turnos.actualizar");
            bool canDelete = await authService.HasPermissionAsync("turnos.eliminar");
            bool canAtender = await authService.HasPermissionAsync("turnos.atender");
            bool canFacturar = await authService.HasPermissionAsync("facturas.agregar");
            bool isProfesional = await authService.IsInRoleAsync("Profesional");

            // Botón Nuevo Turno
            nuevoTurnoButton.Visible = canAdd;
            nuevoTurnoButton.Tag = canAdd;

            // Columnas de grilla
            if (turnosDataGridView.Columns.Contains("atenderColumn"))
            {
                turnosDataGridView.Columns["atenderColumn"].Visible = canAtender && isProfesional;
            }

            if (turnosDataGridView.Columns.Contains("editarColumn"))
            {
                turnosDataGridView.Columns["editarColumn"].Visible = canUpdate;
            }

            if (turnosDataGridView.Columns.Contains("liberarColumn"))
            {
                turnosDataGridView.Columns["liberarColumn"].Visible = canDelete;
            }

            // Opciones del Menú Contextual
            _llegadaMenuItem.Visible = canUpdate;
            _revertirLlegadaMenuItem.Visible = canUpdate;
            _ausenteMenuItem.Visible = canUpdate;
            _atenderMenuItem.Visible = canAtender;
            _liberarMenuItem.Visible = canDelete;
            _detalleMenuItem.Visible = true;
            _editarMenuItem.Visible = canUpdate;
            _facturarMenuItem.Visible = canFacturar;

            _sep1.Visible = canDelete && (canUpdate || canAtender);
            _sep2.Visible = canDelete || canUpdate || canAtender;
            _sep3.Visible = canFacturar;
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

            var authService = AuthServiceProvider.Instance;
            bool isProfesional = await authService.IsInRoleAsync("Profesional");
            int? personaId = await authService.GetPersonaIdAsync();

            if (isProfesional && personaId.HasValue && personaId.Value > 0)
            {
                var miProfesional = _profesionales.FirstOrDefault(p => p.Id == personaId.Value);
                var lista = new List<ProfesionalItem>();
                if (miProfesional != null)
                {
                    lista.Add(new ProfesionalItem
                    {
                        Id = miProfesional.Id,
                        DisplayName = $"{miProfesional.Apellido}, {miProfesional.Nombre}"
                    });
                }
                else
                {
                    lista.Add(new ProfesionalItem
                    {
                        Id = personaId.Value,
                        DisplayName = "Mi Agenda"
                    });
                }

                _isUpdatingBusquedaCombos = true;
                busquedaProfesionalComboBox.DataSource = lista;
                busquedaProfesionalComboBox.DisplayMember = "DisplayName";
                busquedaProfesionalComboBox.ValueMember = "Id";
                busquedaProfesionalComboBox.SelectedValue = personaId.Value;
                busquedaProfesionalComboBox.Enabled = false;
                _isUpdatingBusquedaCombos = false;
            }
            else
            {
                PoblarComboProfesionalesBusqueda(0);
            }
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

        private void BusquedaEspecialidadComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingBusquedaCombos) return;
            if (!busquedaProfesionalComboBox.Enabled) return;

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
                var esp = prof != null ? _especialidades.FirstOrDefault(e => e.Id == prof.EspecialidadId) : null;

                return new AgendaGridRow
                {
                    Id = t.Id,
                    Fecha = t.FechaHoraInicio.ToString("dd/MM/yyyy"),
                    HoraInicio = t.FechaHoraInicio.ToString("HH:mm"),
                    HoraFin = t.FechaHoraFin.ToString("HH:mm"),
                    ProfesionalId = t.ProfesionalId,
                    ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : $"ID {t.ProfesionalId}",
                    EspecialidadId = prof?.EspecialidadId,
                    EspecialidadNombre = esp?.Nombre ?? "—",
                    PacienteId = t.PacienteId,
                    PacienteNombre = pac != null ? $"{pac.Apellido}, {pac.Nombre} (DNI: {pac.NroDocumento})" : "— Sin Asignar —",
                    Estado = t.EstadoTurno,
                    Motivo = t.Motivo,
                    Observaciones = t.Observaciones,
                    FacturaId = t.FacturaId,
                    FechaHoraInicio = t.FechaHoraInicio,
                    FechaHoraFin = t.FechaHoraFin,
                    FechaHoraLlegada = t.FechaHoraLlegada
                };
            }).OrderBy(t => t.FechaHoraInicio).ToList();

            AplicarFiltros();
        }

        // ==========================================
        //                 FILTROS 
        // ==========================================

        private void AplicarFiltros()
        {
            bool salaEspera = salaEsperaCheckBox.Checked;
            bool filtrarPorFecha = busquedaFechaDateTimePicker.Checked;
            DateTime fechaSeleccionada = busquedaFechaDateTimePicker.Value.Date;
            var especialidadId = ObtenerComboSelectedId(busquedaEspecialidadComboBox);
            var profesionalId = ObtenerComboSelectedId(busquedaProfesionalComboBox);
            string textoPaciente = busquedaPacienteTextBox.Text.Trim().ToLowerInvariant();

            var filtrados = _allTurnos.AsEnumerable();

            if (salaEspera)
            {
                // Vista de sala de espera: Solo pacientes en estado Presente
                filtrados = filtrados.Where(t => string.Equals(t.Estado, "Presente", StringComparison.OrdinalIgnoreCase));
            }

            if (filtrarPorFecha)
            {
                filtrados = filtrados.Where(t => t.FechaHoraInicio.Date == fechaSeleccionada);
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

            if (salaEspera)
            {
                // Orden de llegada para la sala de espera (FechaHoraLlegada ascendente)
                filtrados = filtrados.OrderBy(t => t.FechaHoraLlegada ?? DateTime.MaxValue);
            }
            else
            {
                filtrados = filtrados.OrderBy(t => t.FechaHoraInicio);
            }

            turnosDataGridView.DataSource = filtrados.ToList();
        }

        private void FiltrarDataGridView(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            _isUpdatingBusquedaCombos = true;
            if (busquedaEspecialidadComboBox.Items.Count > 0) busquedaEspecialidadComboBox.SelectedIndex = 0;
            if (busquedaProfesionalComboBox.Enabled)
            {
                PoblarComboProfesionalesBusqueda(0);
                if (busquedaProfesionalComboBox.Items.Count > 0) busquedaProfesionalComboBox.SelectedIndex = 0;
            }
            _isUpdatingBusquedaCombos = false;

            busquedaFechaDateTimePicker.Checked = false;
            busquedaFechaDateTimePicker.Value = DateTime.Today;
            busquedaPacienteTextBox.Text = string.Empty;
            salaEsperaCheckBox.Checked = false;

            AplicarFiltros();
        }

        // ==========================================
        // CONFIGURACIÓN DE MENÚ CONTEXTUAL Y SELECCIÓN (Menu Click Derecho)
        // ==========================================

        private void ConfigurarContextMenu()
        {
            _agendaContextMenu = new ContextMenuStrip();

            _llegadaMenuItem = new ToolStripMenuItem("Registrar llegada del paciente", null, async (s, e) =>
            {
                var row = GetSelectedAgendaRow();
                if (row != null) await EjecutarRegistrarLlegadaAsync(row);
            });

            _revertirLlegadaMenuItem = new ToolStripMenuItem("Revertir llegada (Asignado)", null, async (s, e) =>
            {
                var row = GetSelectedAgendaRow();
                if (row != null) await EjecutarRevertirLlegadaAsync(row);
            });

            _ausenteMenuItem = new ToolStripMenuItem("No se presentó / Retirado (Ausente)", null, async (s, e) =>
            {
                var row = GetSelectedAgendaRow();
                if (row != null) await EjecutarMarcarAusenteAsync(row);
            });

            _atenderMenuItem = new ToolStripMenuItem("Iniciar atención (Atendido)", null, async (s, e) =>
            {
                var row = GetSelectedAgendaRow();
                if (row != null) await EjecutarAtenderAsync(row);
            });

            _sep1 = new ToolStripSeparator();

            _liberarMenuItem = new ToolStripMenuItem("Liberar turno", null, async (s, e) =>
            {
                var row = GetSelectedAgendaRow();
                if (row != null) await EjecutarLiberarTurnoAsync(row);
            });

            _sep2 = new ToolStripSeparator();

            _detalleMenuItem = new ToolStripMenuItem("Ver detalle", null, (s, e) =>
            {
                var row = GetSelectedAgendaRow();
                if (row != null) MostrarDetalleTurno(row);
            });

            _editarMenuItem = new ToolStripMenuItem("Asignar", null, async (s, e) =>
            {
                var row = GetSelectedAgendaRow();
                if (row != null) await EjecutarEditarTurnoAsync(row);
            });

            _sep3 = new ToolStripSeparator();

            _facturarMenuItem = new ToolStripMenuItem("Facturar turno...", null, async (s, e) =>
            {
                var row = GetSelectedAgendaRow();
                if (row != null) await EjecutarFacturarTurnoAsync(row);
            });

            _agendaContextMenu.Items.AddRange(new ToolStripItem[]
            {
                _llegadaMenuItem,
                _revertirLlegadaMenuItem,
                _ausenteMenuItem,
                _atenderMenuItem,
                _sep1,
                _liberarMenuItem,
                _sep2,
                _detalleMenuItem,
                _editarMenuItem,
                _sep3,
                _facturarMenuItem
            });

            _agendaContextMenu.Opening += AgendaContextMenu_Opening;
            turnosDataGridView.ContextMenuStrip = _agendaContextMenu;
            turnosDataGridView.CellMouseDown += TurnosDataGridView_CellMouseDown;
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

        private void AgendaContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var row = GetSelectedAgendaRow();
            if (row == null)
            {
                e.Cancel = true;
                return;
            }

            var estado = row.Estado;
            bool esAsignado = string.Equals(estado, "Asignado", StringComparison.OrdinalIgnoreCase);
            bool esPresente = string.Equals(estado, "Presente", StringComparison.OrdinalIgnoreCase);
            bool esAtendido = string.Equals(estado, "Atendido", StringComparison.OrdinalIgnoreCase);
            bool esLibre = string.Equals(estado, "Libre", StringComparison.OrdinalIgnoreCase);

            _llegadaMenuItem.Enabled = _llegadaMenuItem.Visible && esAsignado;
            _revertirLlegadaMenuItem.Enabled = _revertirLlegadaMenuItem.Visible && esPresente;
            _ausenteMenuItem.Enabled = _ausenteMenuItem.Visible && (esAsignado || esPresente);
            _atenderMenuItem.Enabled = _atenderMenuItem.Visible && esPresente;
            _liberarMenuItem.Enabled = _liberarMenuItem.Visible && esAsignado && !row.FacturaId.HasValue;
            _editarMenuItem.Enabled = _editarMenuItem.Visible && (esLibre || esAsignado);
            _detalleMenuItem.Enabled = _detalleMenuItem.Visible;

            bool esFacturable = esAsignado || esPresente || esAtendido;
            _facturarMenuItem.Enabled = _facturarMenuItem.Visible && esFacturable;
            _facturarMenuItem.Text = row.FacturaId.HasValue
                ? $"Ver factura (#{row.FacturaId.Value:D5})..."
                : "Facturar turno...";
        }

        private AgendaGridRow? GetSelectedAgendaRow()
        {
            return turnosDataGridView.CurrentRow?.DataBoundItem as AgendaGridRow;
        }

        // ==========================================
        // ACCIONES DE LA AGENDA
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
            var rowItem = grid.Rows[e.RowIndex].DataBoundItem as AgendaGridRow;
            if (rowItem == null) return;

            string colName = grid.Columns[e.ColumnIndex].Name;

            if (colName == "detalleColumn")
            {
                MostrarDetalleTurno(rowItem);
            }
            else if (colName == "atenderColumn")
            {
                await EjecutarAtenderAsync(rowItem);
            }
            else if (colName == "editarColumn")
            {
                await EjecutarEditarTurnoAsync(rowItem);
            }
            else if (colName == "liberarColumn")
            {
                await EjecutarLiberarTurnoAsync(rowItem);
            }
        }

        private void MostrarDetalleTurno(AgendaGridRow rowItem)
        {
            var llegadaInfo = rowItem.FechaHoraLlegada.HasValue
                ? $"\nFecha/Hora Llegada: {rowItem.FechaHoraLlegada.Value:dd/MM/yyyy HH:mm}"
                : "";

            var facturaInfo = rowItem.FacturaId.HasValue
                ? $"\nFacturación: Facturado (Factura N° {rowItem.FacturaId.Value:D5})"
                : "\nFacturación: Pendiente";

            MessageBox.Show(
                $"Turno N° {rowItem.Id}\n\n" +
                $"Fecha: {rowItem.Fecha} ({rowItem.HoraInicio} - {rowItem.HoraFin})\n" +
                $"Especialidad: {rowItem.EspecialidadNombre}\n" +
                $"Profesional: {rowItem.ProfesionalNombre}\n" +
                $"Paciente: {rowItem.PacienteNombre}\n" +
                $"Estado: {rowItem.Estado}\n" +
                $"Motivo: {(string.IsNullOrWhiteSpace(rowItem.Motivo) ? "— Sin motivo —" : rowItem.Motivo)}\n" +
                $"Observaciones: {(string.IsNullOrWhiteSpace(rowItem.Observaciones) ? "— Sin observaciones —" : rowItem.Observaciones)}" +
                llegadaInfo +
                facturaInfo,
                "Detalle del Turno",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private async Task EjecutarFacturarTurnoAsync(AgendaGridRow rowItem)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("facturas.agregar"))
            {
                MessageBox.Show("No tiene permisos para facturar turnos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esFacturable =
                string.Equals(rowItem.Estado, "Asignado", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(rowItem.Estado, "Presente", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(rowItem.Estado, "Atendido", StringComparison.OrdinalIgnoreCase);

            if (!esFacturable)
            {
                MessageBox.Show($"Solo se puede facturar un turno en estado Asignado, Presente o Atendido. Estado actual: '{rowItem.Estado}'.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var modal = new FacturaModalForm(rowItem.Id))
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    await CargarAgendaAsync();
                }
            }
        }

        private async Task EjecutarEditarTurnoAsync(AgendaGridRow rowItem)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("turnos.actualizar"))
            {
                MessageBox.Show("No tiene permisos para editar o asignar turnos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(rowItem.Estado, "Libre", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(rowItem.Estado, "Asignado", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Solo se puede editar o asignar un turno en estado Libre o Asignado. Estado actual: '{rowItem.Estado}'.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new AsignacionTurno(rowItem.Id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await CargarPacientesAsync();
                    await CargarAgendaAsync();
                }
            }
        }

        private async Task EjecutarLiberarTurnoAsync(AgendaGridRow rowItem)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("turnos.eliminar"))
            {
                MessageBox.Show("No tiene permisos para liberar turnos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(rowItem.Estado, "Asignado", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Solo se puede liberar un turno en estado Asignado. Estado actual: '{rowItem.Estado}'.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Desea liberar el turno N° {rowItem.Id} de {rowItem.PacienteNombre}?\n\nEl paciente será removido y el turno volverá a estar disponible (Libre).",
                "Liberar Turno",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await TurnoApiClient.LiberarAsync(rowItem.Id);
                MessageBox.Show($"Turno N° {rowItem.Id} liberado exitosamente. Ahora se encuentra en estado Libre.", "Turno Liberado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarAgendaAsync();
            }, this, "Error al liberar turno");
        }

        private async Task EjecutarRegistrarLlegadaAsync(AgendaGridRow rowItem)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("turnos.actualizar"))
            {
                MessageBox.Show("No tiene permisos para modificar el estado del turno.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(rowItem.Estado, "Asignado", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Solo se puede registrar la llegada para turnos en estado Asignado. Estado actual: '{rowItem.Estado}'.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await TurnoApiClient.RegistrarLlegadaAsync(rowItem.Id);
                MessageBox.Show($"Llegada registrada para {rowItem.PacienteNombre}.\nEl turno N° {rowItem.Id} pasó a estado 'Presente'.", "Llegada Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarAgendaAsync();
            }, this, "Error al registrar llegada");
        }

        private async Task EjecutarRevertirLlegadaAsync(AgendaGridRow rowItem)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("turnos.actualizar"))
            {
                MessageBox.Show("No tiene permisos para modificar el estado del turno.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(rowItem.Estado, "Presente", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Solo se puede revertir la llegada de un turno en estado Presente. Estado actual: '{rowItem.Estado}'.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Confirmar que desea revertir la llegada de {rowItem.PacienteNombre} para el turno N° {rowItem.Id}?\n\nEl turno volverá a estado Asignado y se limpiará la hora de llegada registrada.",
                "Revertir Llegada",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await TurnoApiClient.RevertirLlegadaAsync(rowItem.Id);
                MessageBox.Show($"Llegada revertida para el turno N° {rowItem.Id}. El estado volvió a 'Asignado'.", "Llegada Revertida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarAgendaAsync();
            }, this, "Error al revertir llegada");
        }

        private async Task EjecutarMarcarAusenteAsync(AgendaGridRow rowItem)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("turnos.actualizar"))
            {
                MessageBox.Show("No tiene permisos para marcar turnos como ausentes.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(rowItem.Estado, "Asignado", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(rowItem.Estado, "Presente", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Solo se puede marcar como ausente un turno en estado Asignado o Presente. Estado actual: '{rowItem.Estado}'.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string detalle = string.Equals(rowItem.Estado, "Presente", StringComparison.OrdinalIgnoreCase)
                ? $"El paciente {rowItem.PacienteNombre} se anunció pero se retiró de la sala de espera sin ser atendido."
                : $"El paciente {rowItem.PacienteNombre} no se presentó al turno.";

            var confirm = MessageBox.Show(
                $"¿Confirmar que se marca como 'Ausente' al turno N° {rowItem.Id}?\n\n{detalle}\n\nEsta acción es irreversible (estado final).",
                "Confirmar Ausencia (Estado Final)",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await TurnoApiClient.MarcarAusenteAsync(rowItem.Id);
                MessageBox.Show($"Turno N° {rowItem.Id} marcado como 'Ausente'.", "Ausencia Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarAgendaAsync();
            }, this, "Error al registrar ausencia");
        }

        private async Task EjecutarAtenderAsync(AgendaGridRow rowItem)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("turnos.atender"))
            {
                MessageBox.Show("No tiene permisos para iniciar la atención clínica de turnos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (await authService.IsInRoleAsync("Profesional"))
            {
                int? personaId = await authService.GetPersonaIdAsync();
                if (personaId.HasValue && personaId.Value > 0 && rowItem.ProfesionalId != personaId.Value)
                {
                    MessageBox.Show("Solo puede atender sus propios turnos asignados.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.Equals(rowItem.Estado, "Presente", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Solo se puede atender un turno cuando el paciente está en estado Presente. Estado actual: '{rowItem.Estado}'.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Confirmar el inicio de la atención clínica para el turno N° {rowItem.Id} ({rowItem.PacienteNombre})?\n\nEsta acción pasará el turno al estado 'Atendido' y es irreversible (estado final).",
                "Confirmar Atención (Estado Final)",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await TurnoApiClient.AtenderAsync(rowItem.Id);
                MessageBox.Show($"Turno N° {rowItem.Id} marcado como 'Atendido'.", "Atención Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarAgendaAsync();
            }, this, "Error al registrar atención");
        }

        private void TurnosDataGridView_CellToolTipTextNeeded(object? sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= turnosDataGridView.Rows.Count) return;
            var rowItem = turnosDataGridView.Rows[e.RowIndex].DataBoundItem as AgendaGridRow;
            if (rowItem != null && string.Equals(rowItem.Estado, "Asignado", StringComparison.OrdinalIgnoreCase) && rowItem.FechaHoraInicio < DateTime.Now)
            {
                e.ToolTipText = "Turno asignado con horario expirado. Se sugiere marcar como Ausente.";
            }
        }

        private async void nuevoTurnoButton_Click(object? sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("turnos.agregar"))
            {
                MessageBox.Show("No tiene permisos para crear turnos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new AsignacionTurno())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await CargarPacientesAsync();
                    await CargarAgendaAsync();
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
            public int? EspecialidadId { get; set; }
            public string EspecialidadNombre { get; set; } = string.Empty;
            public int? PacienteId { get; set; }
            public string PacienteNombre { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
            public DateTime? FechaHoraLlegada { get; set; }
            public string HoraLlegada => FechaHoraLlegada.HasValue ? FechaHoraLlegada.Value.ToString("HH:mm") : "—";
            public string Motivo { get; set; } = string.Empty;
            public string Observaciones { get; set; } = string.Empty;
            public int? FacturaId { get; set; }
            public DateTime FechaHoraInicio { get; set; }
            public DateTime FechaHoraFin { get; set; }
        }
    }
}
