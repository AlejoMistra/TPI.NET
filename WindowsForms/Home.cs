using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DatosMaestrosUC = WindowsForms.DatosMaestros.DatosMaestros;

namespace WindowsForms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private async void Home_Load(object sender, EventArgs e)
        {
            await ConfigureMenuPermissions();
        }

        private async Task ConfigureMenuPermissions()
        {
            var authService = AuthServiceProvider.Instance;

            // Mostrar usuario y rol en el título
            var username = await authService.GetUsernameAsync();
            var role = await authService.GetRoleAsync();
            if (!string.IsNullOrEmpty(username))
            {
                this.Text = $"Sistema de Gestión Médico - {username} ({role})";
            }

            // Permisos de menú según roles y políticas
            bool canViewAgenda = await authService.HasPermissionAsync("turnos.leer");
            bool canViewPacientes = await authService.HasPermissionAsync("pacientes.leer");
            bool canViewFacturacion = await authService.HasPermissionAsync("facturas.leer");
            bool canViewDatosMaestros = await authService.HasPermissionAsync("datosmaestros.leer");

            agendaDeTurnosToolStripMenuItem.Visible = canViewAgenda;
            pacientesToolStripMenuItem.Visible = canViewPacientes;
            facturacionToolStripMenuItem.Visible = canViewFacturacion;
            datosMaestrosToolStripMenuItem.Visible = canViewDatosMaestros;

            // Cargar por defecto la primera opción disponible
            if (canViewAgenda)
            {
                agendaDeTurnosToolStripMenuItem_Click(this, EventArgs.Empty);
            }
            else if (canViewPacientes)
            {
                pacientesToolStripMenuItem_Click(this, EventArgs.Empty);
            }
        }

        private async void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Está seguro que desea cerrar la sesión actual?", "Cerrar Sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var authService = AuthServiceProvider.Instance;
                await authService.LogoutAsync();
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        private void ShowControl(UserControl control)
        {
            ContentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(control);
        }

        private void agendaDeTurnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pacientesToolStripMenuItem.BackColor = SystemColors.Control;
            facturacionToolStripMenuItem.BackColor = SystemColors.Control;
            datosMaestrosToolStripMenuItem.BackColor = SystemColors.Control;
            agendaDeTurnosToolStripMenuItem.BackColor = SystemColors.ControlLight;
            ShowControl(new AgendaTurnos());
        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            agendaDeTurnosToolStripMenuItem.BackColor = SystemColors.Control;
            facturacionToolStripMenuItem.BackColor = SystemColors.Control;
            datosMaestrosToolStripMenuItem.BackColor = SystemColors.Control;
            pacientesToolStripMenuItem.BackColor = SystemColors.ControlLight;
            ShowControl(new Pacientes());
        }

        private void facturacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            agendaDeTurnosToolStripMenuItem.BackColor = SystemColors.Control;
            pacientesToolStripMenuItem.BackColor = SystemColors.Control;
            datosMaestrosToolStripMenuItem.BackColor = SystemColors.Control;
            facturacionToolStripMenuItem.BackColor = SystemColors.ControlLight;
            ShowControl(new Facturacion());

        }

        private void datosMaestrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            agendaDeTurnosToolStripMenuItem.BackColor = SystemColors.Control;
            pacientesToolStripMenuItem.BackColor = SystemColors.Control;
            facturacionToolStripMenuItem.BackColor = SystemColors.Control;
            datosMaestrosToolStripMenuItem.BackColor = SystemColors.ControlLight;
            ShowControl(new DatosMaestrosUC());
        }
    }
}
