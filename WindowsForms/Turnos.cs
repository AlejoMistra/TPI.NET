using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Turnos : UserControl
    {
        public Turnos()
        {
            InitializeComponent();
            ConfigurarColumnas();
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
                Name = "pacienteColumn",
                HeaderText = "Paciente",
                DataPropertyName = "PacienteNombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            turnosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "estadoColumn",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            turnosDataGridView.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "accionesColumn",
                HeaderText = "",
                Text = "Ver / Editar",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });
        }

        private void FiltrarButton_Click(object? sender, EventArgs e)
        {
            // Stub para filtrar turnos cuando la API esté conectada
        }

        private void LimpiarFiltrosLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            busquedaFechaDateTimePicker.Value = DateTime.Today;

            if (busquedaProfesionalComboBox.Items.Count > 0)
            {
                busquedaProfesionalComboBox.SelectedIndex = -1;
            }
            busquedaProfesionalComboBox.Text = string.Empty;

            busquedaPacienteTextBox.Text = string.Empty;
        }

        private void nuevoTurnoButton_Click(object? sender, EventArgs e)
        {
            // Abrir form NuevoTurno como un diálogo modal
            using (var nuevoTurnoForm = new AsignacionTurno())
            {
                if (nuevoTurnoForm.ShowDialog() == DialogResult.OK)
                {
                    // Aquí puedes manejar la lógica después de que se cierre el formulario
                    // Por ejemplo, actualizar la lista de turnos en el DataGridView
                }
            }
        }
    }
}
