using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.DatosMaestros
{
    public partial class Turnos : UserControl
    {
        private int? _selectedTurnoId = null;

        public Turnos()
        {
            InitializeComponent();
            ConfigurarColumnas();
            InicializarEstados();
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

            // Deshabilitar cambio de color en celdas de encabezado al seleccionar columnas/filas
            turnosDataGridView.EnableHeadersVisualStyles = false;
            turnosDataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = turnosDataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            turnosDataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = turnosDataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            turnosDataGridView.Columns.Clear();

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idColumn",
                HeaderText = "N° Turno",
                DataPropertyName = "Id",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fechaColumn",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horaInicioColumn",
                HeaderText = "Hora Inicio",
                DataPropertyName = "HoraInicio",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "horaFinColumn",
                HeaderText = "Hora Fin",
                DataPropertyName = "HoraFin",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "profesionalColumn",
                HeaderText = "Profesional",
                DataPropertyName = "ProfesionalNombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "especialidadColumn",
                HeaderText = "Especialidad",
                DataPropertyName = "EspecialidadNombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "estadoColumn",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "motivoColumn",
                HeaderText = "Motivo",
                DataPropertyName = "Motivo",
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
            var estados = new[] { "Pendiente", "Confirmado", "Atendido", "Ausente", "Cancelado", "Reprogramado" };

            // Combo búsqueda
            busquedaEstadoComboBox.Items.Clear();
            busquedaEstadoComboBox.Items.Add("Todos");
            busquedaEstadoComboBox.Items.AddRange(estados);
            busquedaEstadoComboBox.SelectedIndex = 0;

            // Combo formulario
            estadoComboBox.Items.Clear();
            estadoComboBox.Items.AddRange(estados);
            if (estadoComboBox.Items.Count > 0)
            {
                estadoComboBox.SelectedIndex = 0;
            }
        }

        private void FiltrarButton_Click(object? sender, EventArgs e)
        {
            // Stub para filtrar turnos cuando la API esté conectada
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (busquedaProfesionalComboBox.Items.Count > 0) busquedaProfesionalComboBox.SelectedIndex = -1;
            busquedaProfesionalComboBox.Text = string.Empty;

            if (busquedaEspecialidadComboBox.Items.Count > 0) busquedaEspecialidadComboBox.SelectedIndex = -1;
            busquedaEspecialidadComboBox.Text = string.Empty;

            busquedaFechaDateTimePicker.Value = DateTime.Today;

            if (busquedaEstadoComboBox.Items.Count > 0) busquedaEstadoComboBox.SelectedIndex = 0;
        }

        private void AgregarTurnoButton_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            numeroTurnoTextBox.Text = "(Nuevo)";
            profesionalComboBox.Focus();
        }

        private void GuardarTurnoButton_Click(object? sender, EventArgs e)
        {
            if (profesionalComboBox.SelectedIndex < 0 && string.IsNullOrWhiteSpace(profesionalComboBox.Text))
            {
                MessageBox.Show("Debe seleccionar un profesional.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                profesionalComboBox.Focus();
                return;
            }

            if (especialidadComboBox.SelectedIndex < 0 && string.IsNullOrWhiteSpace(especialidadComboBox.Text))
            {
                MessageBox.Show("Debe seleccionar una especialidad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                especialidadComboBox.Focus();
                return;
            }

            if (horaFinDateTimePicker.Value.TimeOfDay <= horaInicioDateTimePicker.Value.TimeOfDay)
            {
                MessageBox.Show("La hora de fin debe ser posterior a la hora de inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                horaFinDateTimePicker.Focus();
                return;
            }

            if (_selectedTurnoId == null)
            {
                // Alta
                MessageBox.Show("Turno registrado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Modificación
                MessageBox.Show("Turno actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LimpiarFormulario();
        }

        private void CancelarButton_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _selectedTurnoId = null;
            numeroTurnoTextBox.Text = string.Empty;

            if (profesionalComboBox.Items.Count > 0) profesionalComboBox.SelectedIndex = -1;
            profesionalComboBox.Text = string.Empty;

            if (especialidadComboBox.Items.Count > 0) especialidadComboBox.SelectedIndex = -1;
            especialidadComboBox.Text = string.Empty;

            fechaTurnoDateTimePicker.Value = DateTime.Today;
            horaInicioDateTimePicker.Value = DateTime.Today.AddHours(8);
            horaFinDateTimePicker.Value = DateTime.Today.AddHours(8).AddMinutes(30);

            if (estadoComboBox.Items.Count > 0) estadoComboBox.SelectedIndex = 0;
            motivoTextBox.Text = string.Empty;

            guardarTurnoButton.Text = "Guardar Turno";
        }
    }
}
