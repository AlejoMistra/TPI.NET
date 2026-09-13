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
        }

        private void nuevoTurnoButton_Click(object sender, EventArgs e)
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
