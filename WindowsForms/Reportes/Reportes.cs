using API.Clients;
using WindowsForms.Helpers;

namespace WindowsForms.Reportes
{
    public partial class Reportes : UserControl
    {
        private ReporteTurnos? _reporteTurnos;
        private ReporteFacturacion? _reporteFacturacion;

        public Reportes()
        {
            InitializeComponent();
        }

        private async void Reportes_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await ConfigurarPermisosPestanasAsync();
                LoadTurnos();
            }, null, "Error al configurar permisos de reportes");
        }

        private async Task ConfigurarPermisosPestanasAsync()
        {
            var authService = AuthServiceProvider.Instance;
            bool canViewFacturacion = await authService.HasPermissionAsync("reportes.facturacion.leer");

            if (!canViewFacturacion && reportesTabControl.TabPages.Contains(tabFacturacion))
            {
                reportesTabControl.TabPages.Remove(tabFacturacion);
            }
        }

        private void tabTurnos_Enter(object? sender, EventArgs e)
        {
            LoadTurnos();
        }

        private void tabFacturacion_Enter(object? sender, EventArgs e)
        {
            LoadFacturacion();
        }

        private void LoadTurnos()
        {
            if (_reporteTurnos != null) return;

            _reporteTurnos = new ReporteTurnos();
            _reporteTurnos.Dock = DockStyle.Fill;
            tabTurnos.Controls.Add(_reporteTurnos);
        }

        private void LoadFacturacion()
        {
            if (_reporteFacturacion != null) return;

            _reporteFacturacion = new ReporteFacturacion();
            _reporteFacturacion.Dock = DockStyle.Fill;
            tabFacturacion.Controls.Add(_reporteFacturacion);
        }
    }
}

