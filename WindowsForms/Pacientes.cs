using API.Clients;
using DTOs;
using System.Data;
using WindowsForms.Helpers;

namespace WindowsForms
{
    public partial class Pacientes : UserControl
    {
        private List<PacienteGridRow> _allPacientes = new();
        private List<ProfesionalDTO> _profesionales = new();
        private List<EspecialidadDTO> _especialidades = new();
        private List<TurnoDTO> _turnos = new();
        private bool _isUpdatingBusquedaCombos = false;
        private int _selectedPacienteRowIndex = -1;

        private ContextMenuStrip _pacientesContextMenu = null!;
        private ToolStripMenuItem _verDetalleMenuItem = null!;

        public Pacientes()
        {
            InitializeComponent();
            ConfigurarFiltrosIniciales();
            ConfigurarColumnas();
            ConfigurarContextMenu();
        }

        private void ConfigurarFiltrosIniciales()
        {
            _isUpdatingBusquedaCombos = true;
            busquedaObraSocialComboBox.Items.Clear();
            busquedaObraSocialComboBox.Items.AddRange(new object[]
            {
                "Todas",
                "Particular"
            });
            busquedaObraSocialComboBox.SelectedIndex = 0;
            _isUpdatingBusquedaCombos = false;
        }

        private void ConfigurarColumnas()
        {
            pacientesDataGridView.AutoGenerateColumns = false;
            pacientesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pacientesDataGridView.MultiSelect = false;
            pacientesDataGridView.ReadOnly = true;
            pacientesDataGridView.AllowUserToAddRows = false;
            pacientesDataGridView.AllowUserToDeleteRows = false;
            pacientesDataGridView.RowHeadersVisible = false;
            pacientesDataGridView.TabStop = false;

            pacientesDataGridView.EnableHeadersVisualStyles = false;
            pacientesDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = pacientesDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            pacientesDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = pacientesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            pacientesDataGridView.Columns.Clear();

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idColumn",
                HeaderText = "N° Pac.",
                DataPropertyName = nameof(PacienteGridRow.Id),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "apellidoColumn",
                HeaderText = "Apellido",
                DataPropertyName = nameof(PacienteGridRow.Apellido),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nombreColumn",
                HeaderText = "Nombre",
                DataPropertyName = nameof(PacienteGridRow.Nombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "documentoColumn",
                HeaderText = "Documento",
                DataPropertyName = nameof(PacienteGridRow.DocumentoTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fechaNacimientoColumn",
                HeaderText = "Fecha Nac. / Edad",
                DataPropertyName = nameof(PacienteGridRow.FechaNacimientoTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "obraSocialColumn",
                HeaderText = "Obra Social",
                DataPropertyName = nameof(PacienteGridRow.ObraSocialTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "telefonoColumn",
                HeaderText = "Teléfono",
                DataPropertyName = nameof(PacienteGridRow.TelefonoTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "emailColumn",
                HeaderText = "Correo Electrónico",
                DataPropertyName = nameof(PacienteGridRow.EmailTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ultimoTurnoColumn",
                HeaderText = "Última Atención",
                DataPropertyName = nameof(PacienteGridRow.UltimoTurnoTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            pacientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "proximoTurnoColumn",
                HeaderText = "Próximo Turno",
                DataPropertyName = nameof(PacienteGridRow.ProximoTurnoTexto),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            pacientesDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "detalleColumn",
                HeaderText = "",
                Text = "Ver Detalle",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            pacientesDataGridView.CellClick += PacientesDataGridView_CellClick;
            pacientesDataGridView.MouseDown += PacientesDataGridView_MouseDown;
            pacientesDataGridView.SelectionChanged += (s, e) =>
            {
                if (Control.MouseButtons == MouseButtons.None)
                {
                    _selectedPacienteRowIndex = pacientesDataGridView.CurrentRow?.Selected == true
                        ? pacientesDataGridView.CurrentRow.Index
                        : -1;
                }
            };
        }

        // ==========================================
        // CARGA ASINCRÓNICA DE DATOS
        // ==========================================

        private async void Pacientes_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await CargarEspecialidadesAsync();
                await CargarProfesionalesAsync();
                await CargarDatosPacientesYTurnosAsync();

                LimpiarSeleccionPacientes();
                if (this.IsHandleCreated)
                {
                    BeginInvoke(new Action(LimpiarSeleccionPacientes));
                }
            }, this, "Error al cargar el listado de pacientes");
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

        private async Task CargarDatosPacientesYTurnosAsync()
        {
            var pacientesTask = PacienteApiClient.GetAllAsync();
            var turnosTask = TurnoApiClient.GetAllAsync();

            await Task.WhenAll(pacientesTask, turnosTask);

            var pacientes = (await pacientesTask).OrderBy(p => p.Apellido).ThenBy(p => p.Nombre).ToList();
            _turnos = (await turnosTask).Where(t => t.PacienteId.HasValue).ToList();

            PoblarComboObrasSociales(pacientes);

            var profPorId = _profesionales.ToDictionary(pr => pr.Id, pr => pr);

            _allPacientes = pacientes.Select(p =>
            {
                var turnosDelPaciente = _turnos.Where(t => t.PacienteId == p.Id).ToList();

                var profesionalIds = turnosDelPaciente.Select(t => t.ProfesionalId).Distinct().ToHashSet();
                var especialidadIds = profesionalIds
                    .Where(id => profPorId.ContainsKey(id))
                    .Select(id => profPorId[id].EspecialidadId)
                    .Distinct()
                    .ToHashSet();

                var ultimoAtendido = turnosDelPaciente
                    .Where(t => string.Equals(t.EstadoTurno, "Atendido", StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(t => t.FechaHoraInicio)
                    .FirstOrDefault();

                var proximoAsignado = turnosDelPaciente
                    .Where(t =>
                        string.Equals(t.EstadoTurno, "Asignado", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(t.EstadoTurno, "Presente", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(t => t.FechaHoraInicio >= DateTime.Today ? 0 : 1)
                    .ThenBy(t => t.FechaHoraInicio)
                    .FirstOrDefault();

                return new PacienteGridRow
                {
                    Id = p.Id,
                    Apellido = p.Apellido,
                    Nombre = p.Nombre,
                    TipoDocumento = string.IsNullOrWhiteSpace(p.TipoDocumento) ? "DNI" : p.TipoDocumento,
                    NroDocumento = p.NroDocumento,
                    FechaNacimiento = p.FechaNacimiento,
                    FechaNacimientoTexto = PacienteDetalleModalForm.FormatearFechaNacimientoYEdad(p.FechaNacimiento),
                    ObraSocialTexto = string.IsNullOrWhiteSpace(p.ObraSocial) ? "Particular" : p.ObraSocial.Trim(),
                    TelefonoTexto = string.IsNullOrWhiteSpace(p.Telefono) ? "—" : p.Telefono.Trim(),
                    EmailTexto = string.IsNullOrWhiteSpace(p.Email) ? "—" : p.Email.Trim(),
                    UltimoTurnoTexto = ultimoAtendido != null ? $"{ultimoAtendido.FechaHoraInicio:dd/MM/yyyy}" : "—",
                    ProximoTurnoTexto = proximoAsignado != null ? $"{proximoAsignado.FechaHoraInicio:dd/MM/yyyy HH:mm} ({proximoAsignado.EstadoTurno})" : "—",
                    TieneProximoTurno = proximoAsignado != null,
                    ProfesionalIdsAtencion = profesionalIds,
                    EspecialidadIdsAtencion = especialidadIds
                };
            }).ToList();

            AplicarFiltros();
        }

        private void PoblarComboObrasSociales(List<PacienteDTO> pacientes)
        {
            var currentSelection = busquedaObraSocialComboBox.SelectedItem?.ToString() ?? "Todas";

            var obrasSociales = pacientes
                .Select(p => p.ObraSocial?.Trim() ?? string.Empty)
                .Where(os => !string.IsNullOrWhiteSpace(os) && !string.Equals(os, "Particular", StringComparison.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(os => os)
                .ToList();

            _isUpdatingBusquedaCombos = true;
            busquedaObraSocialComboBox.Items.Clear();
            busquedaObraSocialComboBox.Items.Add("Todas");
            busquedaObraSocialComboBox.Items.Add("Particular");
            foreach (var os in obrasSociales)
            {
                busquedaObraSocialComboBox.Items.Add(os);
            }

            int idx = busquedaObraSocialComboBox.Items.IndexOf(currentSelection);
            busquedaObraSocialComboBox.SelectedIndex = idx >= 0 ? idx : 0;
            _isUpdatingBusquedaCombos = false;
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

        private void AplicarFiltros()
        {
            if (_isUpdatingBusquedaCombos) return;

            string textoBusqueda = busquedaPacienteTextBox.Text.Trim().ToLowerInvariant();
            string obraSocialFiltro = busquedaObraSocialComboBox.SelectedItem?.ToString() ?? "Todas";
            int especialidadId = ObtenerComboSelectedId(busquedaEspecialidadComboBox);
            int profesionalId = ObtenerComboSelectedId(busquedaProfesionalComboBox);

            var filtrados = _allPacientes.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                filtrados = filtrados.Where(p =>
                    p.Nombre.ToLowerInvariant().Contains(textoBusqueda) ||
                    p.Apellido.ToLowerInvariant().Contains(textoBusqueda) ||
                    p.NroDocumento.ToLowerInvariant().Contains(textoBusqueda) ||
                    $"{p.Apellido}, {p.Nombre}".ToLowerInvariant().Contains(textoBusqueda) ||
                    $"{p.Nombre} {p.Apellido}".ToLowerInvariant().Contains(textoBusqueda));
            }

            if (!string.Equals(obraSocialFiltro, "Todas", StringComparison.OrdinalIgnoreCase))
            {
                filtrados = filtrados.Where(p => string.Equals(p.ObraSocialTexto, obraSocialFiltro, StringComparison.OrdinalIgnoreCase));
            }

            if (profesionalId > 0)
            {
                filtrados = filtrados.Where(p => p.ProfesionalIdsAtencion.Contains(profesionalId));
            }
            else if (especialidadId > 0)
            {
                filtrados = filtrados.Where(p => p.EspecialidadIdsAtencion.Contains(especialidadId));
            }

            pacientesDataGridView.DataSource = filtrados.ToList();
            LimpiarSeleccionPacientes();
        }

        private void LimpiarSeleccionPacientes()
        {
            pacientesDataGridView.ClearSelection();
            pacientesDataGridView.CurrentCell = null;
            _selectedPacienteRowIndex = -1;
        }

        private void FiltrarDataGridView(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            _isUpdatingBusquedaCombos = true;
            if (busquedaObraSocialComboBox.Items.Count > 0) busquedaObraSocialComboBox.SelectedIndex = 0;
            if (busquedaEspecialidadComboBox.Items.Count > 0) busquedaEspecialidadComboBox.SelectedIndex = 0;
            PoblarComboProfesionalesBusqueda(0);
            if (busquedaProfesionalComboBox.Items.Count > 0) busquedaProfesionalComboBox.SelectedIndex = 0;
            _isUpdatingBusquedaCombos = false;

            busquedaPacienteTextBox.Text = string.Empty;
            AplicarFiltros();
        }

        // ==========================================
        // MENÚ CONTEXTUAL Y ACCIONES DE DETALLE
        // ==========================================

        private void ConfigurarContextMenu()
        {
            _pacientesContextMenu = new ContextMenuStrip();

            _verDetalleMenuItem = new ToolStripMenuItem("Ver detalle e Historia Clínica...", null, (s, e) =>
            {
                var row = GetSelectedRow();
                if (row != null) AbrirModalDetallePaciente(row.Id);
            });

            _pacientesContextMenu.Items.Add(_verDetalleMenuItem);
            _pacientesContextMenu.Opening += PacientesContextMenu_Opening;
            pacientesDataGridView.ContextMenuStrip = _pacientesContextMenu;
        }

        private void PacientesContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var row = GetSelectedRow();
            if (row == null)
            {
                e.Cancel = true;
                return;
            }
            _verDetalleMenuItem.Enabled = true;
        }

        private PacienteGridRow? GetSelectedRow()
        {
            return pacientesDataGridView.CurrentRow?.DataBoundItem as PacienteGridRow;
        }

        private void PacientesDataGridView_MouseDown(object? sender, MouseEventArgs e)
        {
            var hit = pacientesDataGridView.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.None || hit.Type == DataGridViewHitTestType.ColumnHeader)
            {
                LimpiarSeleccionPacientes();
            }
        }

        private void PacientesDataGridView_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                pacientesDataGridView.ClearSelection();
                pacientesDataGridView.Rows[e.RowIndex].Selected = true;
                pacientesDataGridView.CurrentCell = pacientesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex >= 0 ? e.ColumnIndex : 0];
                _selectedPacienteRowIndex = e.RowIndex;
            }
        }

        private void PacientesDataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex >= 0 && pacientesDataGridView.Columns[e.ColumnIndex].Name == "detalleColumn")
            {
                _selectedPacienteRowIndex = e.RowIndex;
                return;
            }

            if (_selectedPacienteRowIndex == e.RowIndex)
            {
                LimpiarSeleccionPacientes();
            }
            else
            {
                _selectedPacienteRowIndex = e.RowIndex;
            }
        }

        private void PacientesDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            LimpiarSeleccionPacientes();
        }

        private void PacientesDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender!;
            if (grid.Columns[e.ColumnIndex].Name == "detalleColumn")
            {
                if (grid.Rows[e.RowIndex].DataBoundItem is PacienteGridRow rowItem)
                {
                    AbrirModalDetallePaciente(rowItem.Id);
                }
            }
        }

        private void PacientesDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (pacientesDataGridView.Rows[e.RowIndex].DataBoundItem is PacienteGridRow rowItem)
            {
                AbrirModalDetallePaciente(rowItem.Id);
            }
        }

        private static void AbrirModalDetallePaciente(int pacienteId)
        {
            using (var modal = new PacienteDetalleModalForm(pacienteId))
            {
                modal.ShowDialog();
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

        public class PacienteGridRow
        {
            public int Id { get; set; }
            public string Apellido { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string TipoDocumento { get; set; } = "DNI";
            public string NroDocumento { get; set; } = string.Empty;
            public string DocumentoTexto => $"{TipoDocumento} {NroDocumento}";
            public DateTime FechaNacimiento { get; set; }
            public string FechaNacimientoTexto { get; set; } = string.Empty;
            public string ObraSocialTexto { get; set; } = string.Empty;
            public string TelefonoTexto { get; set; } = string.Empty;
            public string EmailTexto { get; set; } = string.Empty;
            public string UltimoTurnoTexto { get; set; } = string.Empty;
            public string ProximoTurnoTexto { get; set; } = string.Empty;
            public bool TieneProximoTurno { get; set; }
            public HashSet<int> ProfesionalIdsAtencion { get; set; } = new();
            public HashSet<int> EspecialidadIdsAtencion { get; set; } = new();
        }
    }
}
