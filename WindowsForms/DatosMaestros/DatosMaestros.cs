using System;
using System.Windows.Forms;

namespace WindowsForms.DatosMaestros
{
    public partial class DatosMaestros : UserControl
    {
        // Lazy-loaded backing fields — instantiated only on first tab activation
        private Profesionales? _profesionales;
        private Especialidades? _especialidades;

        public DatosMaestros()
        {
            InitializeComponent();
            LoadProfesionales();
        }

        private void profesionalesTabPage_Enter(object sender, EventArgs e)
        {
            LoadProfesionales();
        }

        private void especialidadesTabPage_Enter(object sender, EventArgs e)
        {
            LoadEspecialidades();
        }

        private void LoadProfesionales()
        {
            if (_profesionales != null) return; // Already loaded

            _profesionales = new Profesionales();
            _profesionales.Dock = DockStyle.Fill;
            profesionalesTabPage.Controls.Add(_profesionales);
        }

        private void LoadEspecialidades()
        {
            if (_especialidades != null) return; // Already loaded
            _especialidades = new Especialidades();
            _especialidades.Dock = DockStyle.Fill;
            especialidadesTabPage.Controls.Add(_especialidades);
        }
    }
}
