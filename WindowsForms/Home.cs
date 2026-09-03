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
            ShowControl(new DatosMaestros());
        }

        private void ContentPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
