using API.Clients;
using DTOs;
using System.Net.Mail;
using WindowsForms.Helpers;

namespace WindowsForms
{
    public partial class EditarPacienteModalForm : Form
    {
        private static readonly DateTime MinFechaNacimiento = new(1900, 1, 1);

        private readonly int _pacienteId;
        private readonly List<string> _obrasSocialesDisponibles = new();
        private PacienteDTO? _pacienteOriginal;

        public PacienteDTO? PacienteActualizado { get; private set; }

        public EditarPacienteModalForm(int pacienteId, IEnumerable<string>? obrasSocialesDisponibles = null)
        {
            _pacienteId = pacienteId;
            if (obrasSocialesDisponibles != null)
            {
                _obrasSocialesDisponibles.AddRange(obrasSocialesDisponibles.Where(os => !string.IsNullOrWhiteSpace(os)).Distinct(StringComparer.OrdinalIgnoreCase));
            }

            InitializeComponent();
        }

        private async void EditarPacienteModalForm_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                PoblarCombos();
                await CargarDatosPacienteAsync();
            }, this, "Error al cargar los datos del paciente");
        }

        private void PoblarCombos()
        {
            tipoDocumentoComboBox.Items.Clear();
            tipoDocumentoComboBox.Items.AddRange(new object[] { "DNI", "Pasaporte", "Otro" });
            tipoDocumentoComboBox.SelectedIndex = 0;

            obraSocialComboBox.Items.Clear();
            obraSocialComboBox.Items.Add("Particular");
            foreach (var os in _obrasSocialesDisponibles)
            {
                if (!string.Equals(os, "Particular", StringComparison.OrdinalIgnoreCase))
                {
                    obraSocialComboBox.Items.Add(os);
                }
            }

            fechaNacimientoDateTimePicker.MinDate = MinFechaNacimiento;
            fechaNacimientoDateTimePicker.MaxDate = DateTime.Today;
        }

        private async Task CargarDatosPacienteAsync()
        {
            _pacienteOriginal = await PacienteApiClient.GetAsync(_pacienteId);
            if (_pacienteOriginal == null)
            {
                MessageBox.Show("No se encontró la información del paciente solicitado.", "Paciente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            this.Text = $"Editar Paciente - {_pacienteOriginal.Apellido}, {_pacienteOriginal.Nombre} (ID: {_pacienteOriginal.Id})";

            apellidoTextBox.Text = _pacienteOriginal.Apellido;
            nombreTextBox.Text = _pacienteOriginal.Nombre;

            var tipoDoc = string.IsNullOrWhiteSpace(_pacienteOriginal.TipoDocumento) ? "DNI" : _pacienteOriginal.TipoDocumento.Trim();
            int tipoIndex = tipoDocumentoComboBox.FindStringExact(tipoDoc);
            tipoDocumentoComboBox.SelectedIndex = tipoIndex >= 0 ? tipoIndex : 0;

            nroDocumentoTextBox.Text = _pacienteOriginal.NroDocumento;

            if (_pacienteOriginal.FechaNacimiento >= MinFechaNacimiento && _pacienteOriginal.FechaNacimiento <= DateTime.Today)
            {
                fechaNacimientoDateTimePicker.Value = _pacienteOriginal.FechaNacimiento.Date;
            }
            else
            {
                fechaNacimientoDateTimePicker.Value = DateTime.Today;
            }

            var obraSocial = string.IsNullOrWhiteSpace(_pacienteOriginal.ObraSocial) ? "Particular" : _pacienteOriginal.ObraSocial.Trim();
            if (!obraSocialComboBox.Items.Contains(obraSocial))
            {
                obraSocialComboBox.Items.Add(obraSocial);
            }
            obraSocialComboBox.Text = obraSocial;

            telefonoTextBox.Text = _pacienteOriginal.Telefono ?? string.Empty;
            emailTextBox.Text = _pacienteOriginal.Email ?? string.Empty;
        }

        private void TipoDocumentoComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var tipo = tipoDocumentoComboBox.SelectedItem?.ToString();
            if (string.Equals(tipo, "DNI", StringComparison.OrdinalIgnoreCase))
            {
                nroDocumentoTextBox.MaxLength = 8;
                nroDocumentoTextBox.PlaceholderText = "Ej: 12345678";
            }
            else
            {
                nroDocumentoTextBox.MaxLength = 20;
                nroDocumentoTextBox.PlaceholderText = string.Empty;
            }
        }

        private async void GuardarButton_Click(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                if (!ValidarCampos(out var pacienteDto)) return;

                guardarButton.Enabled = false;
                cancelarButton.Enabled = false;

                try
                {
                    PacienteActualizado = await PacienteApiClient.UpdateAsync(pacienteDto);
                    MessageBox.Show(
                        $"Los datos de {pacienteDto.Apellido}, {pacienteDto.Nombre} han sido actualizados exitosamente.",
                        "Datos Actualizados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                finally
                {
                    if (!this.IsDisposed)
                    {
                        guardarButton.Enabled = true;
                        cancelarButton.Enabled = true;
                    }
                }
            }, this, "Error al guardar los cambios del paciente");
        }

        private bool ValidarCampos(out PacienteDTO pacienteDto)
        {
            pacienteDto = new PacienteDTO();

            var apellido = apellidoTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("El apellido del paciente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                apellidoTextBox.Focus();
                return false;
            }
            if (apellido.Length > 100)
            {
                MessageBox.Show("El apellido no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                apellidoTextBox.Focus();
                return false;
            }

            var nombre = nombreTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del paciente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nombreTextBox.Focus();
                return false;
            }
            if (nombre.Length > 100)
            {
                MessageBox.Show("El nombre no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nombreTextBox.Focus();
                return false;
            }

            var tipoDocumento = tipoDocumentoComboBox.SelectedItem?.ToString() ?? "DNI";
            var nroDocumento = nroDocumentoTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nroDocumento))
            {
                MessageBox.Show("El número de documento es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nroDocumentoTextBox.Focus();
                return false;
            }
            if (nroDocumento.Length > 20)
            {
                MessageBox.Show("El número de documento no puede superar los 20 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nroDocumentoTextBox.Focus();
                return false;
            }

            if (string.Equals(tipoDocumento, "DNI", StringComparison.OrdinalIgnoreCase))
            {
                if (nroDocumento.Length < 7 || nroDocumento.Length > 8 || !nroDocumento.All(char.IsDigit))
                {
                    MessageBox.Show("Para tipo de documento DNI, el número debe contener entre 7 y 8 dígitos numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nroDocumentoTextBox.Focus();
                    return false;
                }
            }

            var fechaNac = fechaNacimientoDateTimePicker.Value.Date;
            if (fechaNac < MinFechaNacimiento || fechaNac > DateTime.Today)
            {
                MessageBox.Show($"La fecha de nacimiento debe estar entre {MinFechaNacimiento:dd/MM/yyyy} y hoy.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaNacimientoDateTimePicker.Focus();
                return false;
            }

            var obraSocial = string.IsNullOrWhiteSpace(obraSocialComboBox.Text) ? "Particular" : obraSocialComboBox.Text.Trim();
            if (obraSocial.Length > 100)
            {
                MessageBox.Show("La obra social no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                obraSocialComboBox.Focus();
                return false;
            }

            var telefono = telefonoTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(telefono) && telefono.Length > 20)
            {
                MessageBox.Show("El teléfono no puede superar los 20 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                telefonoTextBox.Focus();
                return false;
            }

            var email = emailTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.Length > 100)
                {
                    MessageBox.Show("El correo electrónico no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    emailTextBox.Focus();
                    return false;
                }

                if (!MailAddress.TryCreate(email, out _))
                {
                    MessageBox.Show("El formato del correo electrónico ingresado no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    emailTextBox.Focus();
                    return false;
                }
            }

            pacienteDto = new PacienteDTO
            {
                Id = _pacienteId,
                Nombre = nombre,
                Apellido = apellido,
                TipoDocumento = tipoDocumento,
                NroDocumento = nroDocumento,
                FechaNacimiento = fechaNac,
                ObraSocial = obraSocial,
                Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono,
                Email = string.IsNullOrWhiteSpace(email) ? null : email
            };

            return true;
        }

        private void CancelarButton_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void EditarPacienteModalForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                e.Handled = true;
            }
        }
    }
}

