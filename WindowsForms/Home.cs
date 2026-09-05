using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatosMaestrosUC = WindowsForms.DatosMaestros.DatosMaestros;

namespace WindowsForms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void ShowControl(UserControl control)
        {
            ContentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(control);
        }

        private void datosMaestrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowControl(new DatosMaestrosUC());
        }
    }
}
