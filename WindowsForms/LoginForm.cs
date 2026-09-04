using System;
using System.Net.Http;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;

            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Ingrese usuario y contrase\u00f1a.";
                return;
            }

            SetBusy(true);

            try
            {
                bool autenticado = await AuthServiceProvider.Instance.LoginAsync(usuario, password);

                if (autenticado)
                {
                    // Cierra el dialogo y le avisa a Program que puede seguir.
                    DialogResult = DialogResult.OK;
                    return;
                }

                // Credenciales invalidas: la API devuelve 401 y el cliente traduce a false.
                lblError.Text = "Usuario o contrase\u00f1a incorrectos.";
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (HttpRequestException)
            {
                lblError.Text = "No se pudo conectar con el servidor. \u00bfEsta levantada la API?";
            }
            catch (Exception ex)
            {
                lblError.Text = "Error inesperado: " + ex.Message;
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Evita doble submit mientras la peticion esta en curso.
        private void SetBusy(bool busy)
        {
            txtUsuario.Enabled = !busy;
            txtPassword.Enabled = !busy;
            btnIngresar.Enabled = !busy;
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}
