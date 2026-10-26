using API.Clients;
using DTOs;
using System.Net.Mail;
using WindowsForms.Helpers;

namespace WindowsForms.DatosMaestros
{
    public partial class ProfesionalModalForm : Form
    {
        private readonly int? _profesionalId;
        private readonly List<EspecialidadDTO> _especialidades = new();
        private ProfesionalDTO? _profesionalOriginal;

        public ProfesionalDTO? ProfesionalGuardado { get; private set; }

        public ProfesionalModalForm(int? profesionalId = null, IEnumerable<EspecialidadDTO>? especialidadesDisponibles = null)
        {
            _profesionalId = profesionalId;
            if (especialidadesDisponibles != null)
            {
                _especialidades.AddRange(especialidadesDisponibles);
            }

            InitializeComponent();
        }

        private async void ProfesionalModalForm_Load(object? sender, EventArgs e)
        {
            await UiErrorHandler.ExecuteAsync(async () =>
            {
                await InicializarCombosAsync();

                if (_profesionalId.HasValue)
                {
                    this.Text = "Editar Profesional";
                    tituloLabel.Text = "Editar Datos del Profesional";
                    subtituloLabel.Text = "Actualice los datos personales, matrícula y especialidad del profesional.";
                    guardarButton.Text = "Guardar Cambios";

                    await CargarDatosProfesionalAsync(_profesionalId.Value);
                }
                else
                {
                    this.Text = "Nuevo Profesional";
                    tituloLabel.Text = "Nuevo Profesional";
                    subtituloLabel.Text = "Complete los datos personales, matrícula y especialidad del profesional.";
                    guardarButton.Text = "Guardar Profesional";
                    activoCheckBox.Checked = true;
                }
            }, this, "Error al cargar datos del formulario");
        }

        private async Task InicializarCombosAsync()
        {
            tipoDocumentoComboBox.Items.Clear();
            tipoDocumentoComboBox.Items.AddRange(new object[] { "DNI", "Pasaporte", "Otro" });
            tipoDocumentoComboBox.SelectedIndex = 0;

            if (_especialidades.Count == 0)
            {
                var especialidadesFromApi = await EspecialidadApiClient.GetAllAsync();
                _especialidades.AddRange(especialidadesFromApi.OrderBy(esp => esp.Nombre));
            }

            var listaEspecialidades = new List<EspecialidadDTO>
            {
                new EspecialidadDTO { Id = 0, Nombre = "Seleccionar especialidad" }
            };
            listaEspecialidades.AddRange(_especialidades);

            especialidadComboBox.DataSource = listaEspecialidades;
            especialidadComboBox.DisplayMember = "Nombre";
            especialidadComboBox.ValueMember = "Id";
            especialidadComboBox.SelectedIndex = 0;
        }

        private async Task CargarDatosProfesionalAsync(int id)
        {
            _profesionalOriginal = await ProfesionalApiClient.GetAsync(id);
            if (_profesionalOriginal == null)
            {
                MessageBox.Show("No se encontró la información del profesional solicitado.", "Profesional no encontrado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            this.Text = $"Editar Profesional - {_profesionalOriginal.Apellido}, {_profesionalOriginal.Nombre} (ID: {_profesionalOriginal.Id})";

            apellidoTextBox.Text = _profesionalOriginal.Apellido;
            nombreTextBox.Text = _profesionalOriginal.Nombre;
            matriculaTextBox.Text = _profesionalOriginal.Matricula;

            var tipoDoc = string.IsNullOrWhiteSpace(_profesionalOriginal.TipoDocumento) ? "DNI" : _profesionalOriginal.TipoDocumento.Trim();
            int tipoIndex = tipoDocumentoComboBox.FindStringExact(tipoDoc);
            tipoDocumentoComboBox.SelectedIndex = tipoIndex >= 0 ? tipoIndex : 0;

            nroDocumentoTextBox.Text = _profesionalOriginal.NroDocumento;

            if (_profesionalOriginal.EspecialidadId > 0)
            {
                especialidadComboBox.SelectedValue = _profesionalOriginal.EspecialidadId;
            }

            telefonoTextBox.Text = _profesionalOriginal.Telefono ?? string.Empty;
            emailTextBox.Text = _profesionalOriginal.Email ?? string.Empty;
            activoCheckBox.Checked = string.Equals(_profesionalOriginal.Estado, "Activo", StringComparison.OrdinalIgnoreCase);
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
                if (!ValidarCampos(out var profesionalDto)) return;

                guardarButton.Enabled = false;
                cancelarButton.Enabled = false;

                try
                {
                    if (_profesionalId == null)
                    {
                        await ProfesionalApiClient.AddAsync(profesionalDto);
                        ProfesionalGuardado = profesionalDto;
                        MessageBox.Show(
                            $"El profesional {profesionalDto.Apellido}, {profesionalDto.Nombre} ha sido registrado exitosamente.",
                            "Profesional Registrado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        profesionalDto.Id = _profesionalId.Value;
                        await ProfesionalApiClient.UpdateAsync(profesionalDto);
                        ProfesionalGuardado = profesionalDto;
                        MessageBox.Show(
                            $"Los datos de {profesionalDto.Apellido}, {profesionalDto.Nombre} han sido actualizados exitosamente.",
                            "Datos Actualizados",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

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
            }, this, "Error al guardar los datos del profesional");
        }

        private bool ValidarCampos(out ProfesionalDTO dto)
        {
            dto = new ProfesionalDTO();

            var apellido = apellidoTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("El apellido del profesional es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("El nombre del profesional es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nombreTextBox.Focus();
                return false;
            }
            if (nombre.Length > 100)
            {
                MessageBox.Show("El nombre no puede superar los 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nombreTextBox.Focus();
                return false;
            }

            var matricula = matriculaTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(matricula))
            {
                MessageBox.Show("La matrícula del profesional es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                matriculaTextBox.Focus();
                return false;
            }
            if (matricula.Length > 50)
            {
                MessageBox.Show("La matrícula no puede superar los 50 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                matriculaTextBox.Focus();
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

            var especialidadId = (especialidadComboBox.SelectedValue as int?) ?? 0;
            if (especialidadId <= 0)
            {
                MessageBox.Show("Debe seleccionar una especialidad válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                especialidadComboBox.Focus();
                return false;
            }

            var telefono = telefonoTextBox.Text.Trim();
            if (telefono.Length > 20)
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

                try
                {
                    _ = new MailAddress(email);
                }
                catch (FormatException)
                {
                    MessageBox.Show("El formato del correo electrónico ingresado no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    emailTextBox.Focus();
                    return false;
                }
            }

            dto.Apellido = apellido;
            dto.Nombre = nombre;
            dto.Matricula = matricula;
            dto.TipoDocumento = tipoDocumento;
            dto.NroDocumento = nroDocumento;
            dto.EspecialidadId = especialidadId;
            dto.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono;
            dto.Email = string.IsNullOrWhiteSpace(email) ? null : email;
            dto.Estado = activoCheckBox.Checked ? "Activo" : "Inactivo";

            return true;
        }

        private void CancelarButton_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ProfesionalModalForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}

