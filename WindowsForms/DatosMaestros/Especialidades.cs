using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms.DatosMaestros
{
    public partial class Especialidades : UserControl
    {
        private List<EspecialidadDTO> _allEspecialidades = new();
        private int? _selectedEspecialidadId = null;

        public Especialidades()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            especialidadesDataGridView.AutoGenerateColumns = false;
            especialidadesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            especialidadesDataGridView.MultiSelect = false;
            especialidadesDataGridView.ReadOnly = true;
            especialidadesDataGridView.AllowUserToAddRows = false;
            especialidadesDataGridView.AllowUserToDeleteRows = false;
            especialidadesDataGridView.RowHeadersVisible = false;

            especialidadesDataGridView.EnableHeadersVisualStyles = false;
            especialidadesDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                especialidadesDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            especialidadesDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                especialidadesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            especialidadesDataGridView.Columns.Clear();

            especialidadesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idColumn",
                HeaderText = "ID",
                DataPropertyName = nameof(EspecialidadDTO.Id),
                Width = 60
            });

            especialidadesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "nombreColumn",
                HeaderText = "Nombre",
                DataPropertyName = nameof(EspecialidadDTO.Nombre),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            especialidadesDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "editarColumn",
                HeaderText = "",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            especialidadesDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "eliminarColumn",
                HeaderText = "",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });
        }

        // ─── Load ────────────────────────────────────────────────────────────

        private async void Especialidades_Load(object? sender, EventArgs e)
        {
            await CargarEspecialidadesAsync();
        }

        private async System.Threading.Tasks.Task CargarEspecialidadesAsync()
        {
            try
            {
                var especialidades = await EspecialidadApiClient.GetAllAsync();
                _allEspecialidades = especialidades.ToList();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar especialidades: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Filtering ───────────────────────────────────────────────────────

        private void AplicarFiltros()
        {
            var texto = busquedaEspecialidadTextBox.Text.Trim().ToLowerInvariant();
            var filtradas = _allEspecialidades.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(texto))
                filtradas = filtradas.Where(e => e.Nombre.ToLowerInvariant().Contains(texto));

            especialidadesDataGridView.DataSource = filtradas.ToList();
        }

        private void FiltrarButton_Click(object? sender, EventArgs e) => AplicarFiltros();

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            busquedaEspecialidadTextBox.Text = string.Empty;
            AplicarFiltros();
        }

        // ─── Grid events ─────────────────────────────────────────────────────

        private void EspecialidadesDataGridView_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            especialidadesDataGridView.ClearSelection();
            especialidadesDataGridView.CurrentCell = null;
        }

        private async void EspecialidadesDataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender!;
            var rowItem = grid.Rows[e.RowIndex].DataBoundItem as EspecialidadDTO;
            if (rowItem == null) return;

            if (grid.Columns[e.ColumnIndex].Name == "editarColumn")
            {
                CargarEspecialidadEnFormulario(rowItem);
            }
            else if (grid.Columns[e.ColumnIndex].Name == "eliminarColumn")
            {
                var confirm = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar la especialidad \"{rowItem.Nombre}\"?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    await EliminarEspecialidadAsync(rowItem.Id);
                }
            }
        }

        private async System.Threading.Tasks.Task EliminarEspecialidadAsync(int id)
        {
            try
            {
                await EspecialidadApiClient.DeleteAsync(id);
                MessageBox.Show("Especialidad eliminada exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarEspecialidadesAsync();
            }
            catch (InvalidOperationException ex)
            {
                // HTTP 409 Conflict — FK constraint: especialidad has associated profesionales
                MessageBox.Show(ex.Message, "No se puede eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar especialidad: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Form actions ────────────────────────────────────────────────────

        private void AgregarEspecialidadButton_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            nombreTextBox.Focus();
        }

        private async void GuardarEspecialidadButton_Click(object? sender, EventArgs e)
        {
            string nombre = nombreTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre es requerido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nombreTextBox.Focus();
                return;
            }

            try
            {
                guardarEspecialidadButton.Enabled = false;

                if (_selectedEspecialidadId == null)
                {
                    // Alta
                    var nueva = new EspecialidadDTO { Nombre = nombre };
                    await EspecialidadApiClient.AddAsync(nueva);
                    MessageBox.Show("Especialidad registrada exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modificación
                    var editada = new EspecialidadDTO
                    {
                        Id = _selectedEspecialidadId.Value,
                        Nombre = nombre
                    };
                    await EspecialidadApiClient.UpdateAsync(editada);
                    MessageBox.Show("Especialidad actualizada exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpiarFormulario();
                await CargarEspecialidadesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar especialidad: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                guardarEspecialidadButton.Enabled = true;
            }
        }

        private void CancelarButton_Click(object? sender, EventArgs e) => LimpiarFormulario();

        private void CargarEspecialidadEnFormulario(EspecialidadDTO especialidad)
        {
            _selectedEspecialidadId = especialidad.Id;
            nombreTextBox.Text = especialidad.Nombre;
            formTitleLabel.Text = "Editar Especialidad";
            guardarEspecialidadButton.Text = "Actualizar Especialidad";
        }

        private void LimpiarFormulario()
        {
            _selectedEspecialidadId = null;
            nombreTextBox.Text = string.Empty;
            formTitleLabel.Text = "Nueva Especialidad";
            guardarEspecialidadButton.Text = "Guardar Especialidad";
        }
    }
}
