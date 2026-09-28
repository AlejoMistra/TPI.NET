using API.Auth.WindowsForms;
using API.Clients;
using API.Clients.Exceptions;

namespace WindowsForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            AuthServiceProvider.Register(new WindowsFormsAuthService());

            // TEMPORAL (desarrollo): registra un auth service falso para bypassear
            // la capa de autenticación hasta que el login real esté implementado.
            // Eliminar esta línea y DevAuthService.cs al integrar el login real.
            AuthServiceProvider.Register(new DevAuthService());

            // Login primero: si el usuario cancela o cierra el dialogo, la app no arranca.
            //using (var login = new LoginForm())
            //{
            //    if (login.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}

            Application.Run(new Home());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception is UnauthorizedAccessException or UnauthorizedApiException)
            {
                MessageBox.Show("Su sesión ha expirado. Debe volver a autenticarse.", "Sesión Expirada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Application.Restart();
            }
            else if (e.Exception is ValidationApiException or ConflictApiException or NotFoundApiException or NetworkApiException)
            {
                MessageBox.Show(e.Exception.Message, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show($"Error inesperado: {e.Exception.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                MessageBox.Show($"Error crítico no controlado:\n\n{ex.Message}", "Error Crítico",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}