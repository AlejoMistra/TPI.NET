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
        public DatosMaestros()
        {
            InitializeComponent();
            // TODO: ConfigurarColumnas();
        }

        private async void Profesionales_Load(object sender, EventArgs e)
        {
            // await ConfigureButtonPermissions();
            await this.GetByEspecialidadAndLoad();
        }

        private async Task GetByEspecialidadAndLoad(string especialidad = "")
        {
            try
            {
                // DeshabilitarControles();
                this.profesionalesDataGridView.DataSource = null;

                IEnumerable<ProfesionalDTO> profesionales;
                if (string.IsNullOrWhiteSpace(especialidad))
                {
                    profesionales = await ProfesionalApiClient.GetAllAsync();
                }
                else
                {
                    profesionales = await ProfesionalApiClient.GetByEspecialidadAsync(especialidad);
                }

                this.profesionalesDataGridView.DataSource = profesionales;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar profesionales {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // HabilitarControles();
            }
        }
    }
}
