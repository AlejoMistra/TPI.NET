using API.Clients;
using DTOs;
using System.Data;
using WindowsForms.Helpers;

namespace WindowsForms.DatosMaestros
{
    public partial class Profesionales : UserControl
    {
        private List<EspecialidadDTO> _especialidades = new();
        private List<ProfesionalGridRow> _allProfesionales = new();

        public Profesionales()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            profesionalesDataGridView.AutoGenerateColumns = false;
            profesionalesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            profesionalesDataGridView.MultiSelect = false;
            profesionalesDataGridView.ReadOnly = true;
            profesionalesDataGridView.AllowUserToAddRows = false;
            profesionalesDataGridView.AllowUserToDeleteRows = false;
            profesionalesDataGridView.RowHeadersVisible = false;

            // Deshabilitar cambio de color en celdas de encabezado al seleccionar columnas/filas
            profesionalesDataGridView.EnableHeadersVisualStyles = false;
            profesionalesDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = profesionalesDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            profesionalesDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = profesionalesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            profesionalesDataGridView.Columns.Clear();

            profesionalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "matriculaColumn",
                HeaderText = "Matrícula",
                DataPropertyName = nameof(ProfesionalGridRow.Matricula)
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "apellidoColumn",
                HeaderText = "Apellido",
                DataPropertyName = nameof(ProfesionalGridRow.Apellido)
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nombreColumn",
                HeaderText = "Nombre",
                DataPropertyName = nameof(ProfesionalGridRow.Nombre)
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "documentColumn",
                HeaderText = "N° Doc",
                DataPropertyName = nameof(ProfesionalGridRow.NroDocumento)
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "especialidadColumn",
                HeaderText = "Especialidad",
                DataPropertyName = nameof(ProfesionalGridRow.EspecialidadNombre)
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "telefonoColumn",
                HeaderText = "Teléfono",
                DataPropertyName = nameof(ProfesionalGridRow.Telefono)
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "emailColumn",
                HeaderText = "Correo Electrónico",
                DataPropertyName = nameof(ProfesionalGridRow.Email)
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "statusColumn",
                HeaderText = "Estado",
                DataPropertyName = nameof(ProfesionalGridRow.Estado)
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "accionesColumn",
                HeaderText = "",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            profesionalesDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "eliminarColumn",
                HeaderText = "",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });
        }

        private async void Profesionales_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await ConfigureButtonPermissions();
                await CargarEspecialidadesAsync();
                InicializarFiltroEstado();
                await CargarProfesionalesAsync();
            }, this, "Error al cargar la vista de profesionales");
        }

        private async Task ConfigureButtonPermissions()
        {
            var authService = AuthServiceProvider.Instance;
            bool canAdd = await authService.HasPermissionAsync("profesionales.agregar");
            bool canUpdate = await authService.HasPermissionAsync("profesionales.actualizar");
            bool canDelete = await authService.HasPermissionAsync("profesionales.eliminar");

            AgregarProfesionalButton.Visible = canAdd;
            AgregarProfesionalButton.Tag = canAdd;

            if (profesionalesDataGridView.Columns.Contains("accionesColumn"))
            {
                profesionalesDataGridView.Columns["accionesColumn"].Visible = canUpdate;
            }

            if (profesionalesDataGridView.Columns.Contains("eliminarColumn"))
            {
                profesionalesDataGridView.Columns["eliminarColumn"].Visible = canDelete;
            }
        }

        private async Task CargarProfesionalesAsync()
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                var profesionales = await ProfesionalApiClient.GetAllAsync();

                _allProfesionales = profesionales.Select(p =>
                {
                    var esp = _especialidades.FirstOrDefault(e => e.Id == p.EspecialidadId);
                    return new ProfesionalGridRow
                    {
                        Id = p.Id,
                        Matricula = p.Matricula,
                        Apellido = p.Apellido,
                        Nombre = p.Nombre,
                        TipoDocumento = p.TipoDocumento,
                        NroDocumento = p.NroDocumento,
                        EspecialidadId = p.EspecialidadId,
                        EspecialidadNombre = esp?.Nombre ?? $"ID {p.EspecialidadId}",
                        Telefono = p.Telefono ?? string.Empty,
                        Email = p.Email ?? string.Empty,
                        Estado = p.Estado
                    };
                }).ToList();

                AplicarFiltros();
            }, this, "Error al cargar profesionales");
        }

        private async Task CargarEspecialidadesAsync()
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                var especialidades = await EspecialidadApiClient.GetAllAsync();
                _especialidades = especialidades.OrderBy(e => e.Nombre).ToList();

                // Combo de búsqueda con opción 'Todas'
                var listaBusqueda = new List<EspecialidadDTO>
                {
                    new EspecialidadDTO { Id = 0, Nombre = "Todas las especialidades" }
                };
                listaBusqueda.AddRange(_especialidades);
                busquedaEspecialidadComboBox.DataSource = listaBusqueda;
                busquedaEspecialidadComboBox.DisplayMember = "Nombre";
                busquedaEspecialidadComboBox.ValueMember = "Id";
            }, this, "Error al cargar especialidades");
        }

        // Filtering
        private void InicializarFiltroEstado()
        {
            busquedaEstadoComboBox.Items.Clear();
            busquedaEstadoComboBox.Items.Add("Todos");
            busquedaEstadoComboBox.Items.Add("Activo");
            busquedaEstadoComboBox.Items.Add("Inactivo");
            busquedaEstadoComboBox.SelectedIndex = 0;
        }

        private void AplicarFiltros()
        {
            var textoBusqueda = busquedaProfesionalTextBox.Text.Trim().ToLowerInvariant();
            var especialidadId = (busquedaEspecialidadComboBox.SelectedValue as int?) ?? 0;
            var estadoFiltro = busquedaEstadoComboBox.SelectedItem?.ToString() ?? "Todos";

            var filtrados = _allProfesionales.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                filtrados = filtrados.Where(p =>
                    p.Nombre.ToLowerInvariant().Contains(textoBusqueda) ||
                    p.Apellido.ToLowerInvariant().Contains(textoBusqueda) ||
                    p.Matricula.ToLowerInvariant().Contains(textoBusqueda) ||
                    p.NroDocumento.ToLowerInvariant().Contains(textoBusqueda));
            }

            if (especialidadId > 0)
            {
                filtrados = filtrados.Where(p => p.EspecialidadId == especialidadId);
            }

            if (estadoFiltro != "Todos")
            {
                filtrados = filtrados.Where(p => p.Estado == estadoFiltro);
            }

            profesionalesDataGridView.DataSource = filtrados.ToList();
        }

        private void FiltrarDataGridView(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            busquedaProfesionalTextBox.Text = string.Empty;
            if (busquedaEspecialidadComboBox.Items.Count > 0)
            {
                busquedaEspecialidadComboBox.SelectedIndex = 0;
            }
            if (busquedaEstadoComboBox.Items.Count > 0)
            {
                busquedaEstadoComboBox.SelectedIndex = 0;
            }

            AplicarFiltros();
        }

        // Grid events
        private void ProfesionalesDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            profesionalesDataGridView.ClearSelection();
            profesionalesDataGridView.CurrentCell = null;
        }

        private async void ProfesionalesDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender!;
            var rowItem = grid.Rows[e.RowIndex].DataBoundItem as ProfesionalGridRow;
            if (rowItem == null) return;

            // Clic en Editar
            if (grid.Columns[e.ColumnIndex].Name == "accionesColumn")
            {
                var authService = AuthServiceProvider.Instance;
                if (!await authService.HasPermissionAsync("profesionales.actualizar"))
                {
                    MessageBox.Show("No tiene permisos para editar profesionales.", "Acceso Denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var modal = new ProfesionalModalForm(rowItem.Id, _especialidades);
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    await CargarProfesionalesAsync();
                }
            }
            // Clic en Eliminar
            else if (grid.Columns[e.ColumnIndex].Name == "eliminarColumn")
            {
                var authService = AuthServiceProvider.Instance;
                if (!await authService.HasPermissionAsync("profesionales.eliminar"))
                {
                    MessageBox.Show("No tiene permisos para eliminar profesionales.", "Acceso Denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar al profesional {rowItem.Nombre} {rowItem.Apellido} (Matrícula: {rowItem.Matricula})?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    await UiErrorHandler.ExecuteAsync(async () =>
                    {
                        await ProfesionalApiClient.DeleteAsync(rowItem.Id);
                        MessageBox.Show("Profesional eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarProfesionalesAsync();
                    }, this, "Error al eliminar profesional");
                }
            }
        }

        // Form actions
        private async void AgregarProfesionalButton_Click(object? sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            if (!await authService.HasPermissionAsync("profesionales.agregar"))
            {
                MessageBox.Show("No tiene permisos para agregar profesionales.", "Acceso Denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var modal = new ProfesionalModalForm(null, _especialidades);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                await CargarProfesionalesAsync();
            }
        }

        public class ProfesionalGridRow
        {
            public int Id { get; set; }
            public string Matricula { get; set; } = string.Empty;
            public string Apellido { get; set; } = string.Empty;
            public string Nombre { get; set; } = string.Empty;
            public string TipoDocumento { get; set; } = string.Empty;
            public string NroDocumento { get; set; } = string.Empty;
            public int EspecialidadId { get; set; }
            public string EspecialidadNombre { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
        }
    }
}
