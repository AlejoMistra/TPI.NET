using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class DatosMaestros : UserControl
    {
        private List<EspecialidadDTO> _especialidades = new();
        private List<ProfesionalGridRow> _allProfesionales = new();
        private int? _selectedProfesionalId = null;

        public DatosMaestros()
        {
            InitializeComponent();
            ConfigurarColumnas();
            RegistrarEventos();
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

        private void RegistrarEventos()
        {
            filtrarButton.Click += FiltrarButton_Click;
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;
            guardarProfesionalButton.Click += GuardarProfesionalButton_Click;
            cancelarButton.Click += CancelarButton_Click;
            profesionalesDataGridView.CellContentClick += ProfesionalesDataGridView_CellContentClick;
            profesionalesDataGridView.DataBindingComplete += ProfesionalesDataGridView_DataBindingComplete;
            AgregarProfesionalButton.Click += AgregarProfesionalButton_Click;
        }

        private void CancelarButton_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void ProfesionalesDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            profesionalesDataGridView.ClearSelection();
            profesionalesDataGridView.CurrentCell = null;
        }

        private void AgregarProfesionalButton_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            nombreTextBox.Focus();
        }

        private async void Profesionales_Load(object sender, EventArgs e)
        {
            await CargarEspecialidadesAsync();
            await CargarProfesionalesAsync();
        }

        private async Task CargarEspecialidadesAsync()
        {
            try
            {
                var especialidades = await EspecialidadApiClient.GetAllAsync();
                _especialidades = especialidades.ToList();

                // Combo de búsqueda con opción 'Todas'
                var listaBusqueda = new List<EspecialidadDTO>
                {
                    new EspecialidadDTO { Id = 0, Nombre = "Todas las especialidades" }
                };
                listaBusqueda.AddRange(_especialidades);
                busquedaEspecialidadComboBox.DataSource = listaBusqueda;
                busquedaEspecialidadComboBox.DisplayMember = "Nombre";
                busquedaEspecialidadComboBox.ValueMember = "Id";

                // Combo del formulario de alta/edición
                var listaFormulario = new List<EspecialidadDTO>
                {
                    new EspecialidadDTO { Id = 0, Nombre = "Seleccionar especialidad" }
                };
                listaFormulario.AddRange(_especialidades);
                especialidadComboBox.DataSource = listaFormulario;
                especialidadComboBox.DisplayMember = "Nombre";
                especialidadComboBox.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar especialidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarProfesionalesAsync()
        {
            try
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
                        Telefono = string.Empty,
                        Email = string.Empty,
                        Estado = "Activo"
                    };
                }).ToList();

                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar profesionales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            var textoBusqueda = busquedaProfesionalTextBox.Text.Trim().ToLowerInvariant();
            var especialidadId = (busquedaEspecialidadComboBox.SelectedValue as int?) ?? 0;

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

            profesionalesDataGridView.DataSource = filtrados.ToList();
        }

        private void FiltrarButton_Click(object? sender, EventArgs e)
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

        private async void GuardarProfesionalButton_Click(object? sender, EventArgs e)
        {
            string nombre = nombreTextBox.Text.Trim();
            string apellido = apellidoTextBox.Text.Trim();
            string matricula = matriculaTextBox.Text.Trim();
            string documento = documentoTextBox.Text.Trim();
            int especialidadId = (especialidadComboBox.SelectedValue as int?) ?? 0;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nombreTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("El apellido es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                apellidoTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(matricula))
            {
                MessageBox.Show("La matrícula es requerida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                matriculaTextBox.Focus();
                return;
            }

            if (especialidadId <= 0)
            {
                MessageBox.Show("Debe seleccionar una especialidad válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                especialidadComboBox.Focus();
                return;
            }

            try
            {
                guardarProfesionalButton.Enabled = false;

                if (_selectedProfesionalId == null)
                {
                    // Alta
                    var nuevoProfesional = new ProfesionalDTO
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        Matricula = matricula,
                        TipoDocumento = "DNI",
                        NroDocumento = documento,
                        EspecialidadId = especialidadId
                    };

                    await ProfesionalApiClient.AddAsync(nuevoProfesional);
                    MessageBox.Show("Profesional registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modificación
                    var profesionalEditado = new ProfesionalDTO
                    {
                        Id = _selectedProfesionalId.Value,
                        Nombre = nombre,
                        Apellido = apellido,
                        Matricula = matricula,
                        TipoDocumento = "DNI",
                        NroDocumento = documento,
                        EspecialidadId = especialidadId
                    };

                    await ProfesionalApiClient.UpdateAsync(profesionalEditado);
                    MessageBox.Show("Profesional actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpiarFormulario();
                await CargarProfesionalesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar profesional: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                guardarProfesionalButton.Enabled = true;
            }
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
                CargarProfesionalEnFormulario(rowItem);
            }
            // Clic en Eliminar
            else if (grid.Columns[e.ColumnIndex].Name == "eliminarColumn")
            {
                var confirmResult = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar al profesional {rowItem.Nombre} {rowItem.Apellido} (Matrícula: {rowItem.Matricula})?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        await ProfesionalApiClient.DeleteAsync(rowItem.Id);
                        MessageBox.Show("Profesional eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (_selectedProfesionalId == rowItem.Id)
                        {
                            LimpiarFormulario();
                        }

                        await CargarProfesionalesAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar profesional: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CargarProfesionalEnFormulario(ProfesionalGridRow profesional)
        {
            _selectedProfesionalId = profesional.Id;
            nombreTextBox.Text = profesional.Nombre;
            apellidoTextBox.Text = profesional.Apellido;
            matriculaTextBox.Text = profesional.Matricula;
            documentoTextBox.Text = profesional.NroDocumento;
            especialidadComboBox.SelectedValue = profesional.EspecialidadId;

            guardarProfesionalButton.Text = "Actualizar Profesional";
        }

        private void LimpiarFormulario()
        {
            _selectedProfesionalId = null;
            nombreTextBox.Text = string.Empty;
            apellidoTextBox.Text = string.Empty;
            matriculaTextBox.Text = string.Empty;
            documentoTextBox.Text = string.Empty;
            telefonoTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            habilitadoCheckBox.Checked = false;

            if (especialidadComboBox.Items.Count > 0)
            {
                especialidadComboBox.SelectedIndex = 0;
            }

            guardarProfesionalButton.Text = "Guardar Profesional";
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
        public string Estado { get; set; } = "Activo";
    }
}
