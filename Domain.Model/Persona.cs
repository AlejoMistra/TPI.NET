using System.Net.Mail;

namespace Domain.Model
{
    public abstract class Persona
    {
        public enum TipoDocumentoEnum
        {
            DNI,
            Pasaporte,
            Otro
        }

        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;
        public string TipoDocumento { get; private set; } = string.Empty;
        public string NroDocumento { get; private set; } = string.Empty;
        public string? Email { get; private set; }
        public string? Telefono { get; private set; }

        // Constructor protegido para EF Core
        protected Persona()
        {
        }

        protected Persona(
            string nombre,
            string apellido,
            string tipoDocumento,
            string nroDocumento,
            string? telefono = null,
            string? email = null)
        {
            SetNombre(nombre);
            SetApellido(apellido);
            SetTipoDocumento(tipoDocumento);
            SetNroDocumento(nroDocumento);
            SetTelefono(telefono);
            SetEmail(email);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser menor que 0.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            var trimmed = nombre.Trim();
            if (trimmed.Length > 100)
                throw new ArgumentException("El nombre no puede superar los 100 caracteres.", nameof(nombre));

            Nombre = trimmed;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido es obligatorio.", nameof(apellido));

            var trimmed = apellido.Trim();
            if (trimmed.Length > 100)
                throw new ArgumentException("El apellido no puede superar los 100 caracteres.", nameof(apellido));

            Apellido = trimmed;
        }

        public void SetTipoDocumento(string tipoDocumento)
        {
            if (string.IsNullOrWhiteSpace(tipoDocumento))
                throw new ArgumentException("El tipo de documento es obligatorio.", nameof(tipoDocumento));

            var trimmed = tipoDocumento.Trim();
            if (!Enum.TryParse<TipoDocumentoEnum>(trimmed, ignoreCase: true, out var tipoEnum) || !Enum.IsDefined(typeof(TipoDocumentoEnum), tipoEnum))
                throw new ArgumentException("El tipo de documento no es válido. Valores permitidos: DNI, Pasaporte, Otro.", nameof(tipoDocumento));

            TipoDocumento = tipoEnum.ToString();

            // Si ya hay un NroDocumento asignado y el tipo cambió a DNI, revalidar consistencia
            if (tipoEnum == TipoDocumentoEnum.DNI && !string.IsNullOrWhiteSpace(NroDocumento))
            {
                ValidarFormatoDni(NroDocumento);
            }
        }

        public void SetNroDocumento(string nroDocumento)
        {
            if (string.IsNullOrWhiteSpace(nroDocumento))
                throw new ArgumentException("El número de documento es obligatorio.", nameof(nroDocumento));

            var trimmed = nroDocumento.Trim();
            if (trimmed.Length > 20)
                throw new ArgumentException("El número de documento no puede superar los 20 caracteres.", nameof(nroDocumento));

            if (string.Equals(TipoDocumento, nameof(TipoDocumentoEnum.DNI), StringComparison.OrdinalIgnoreCase))
            {
                ValidarFormatoDni(trimmed);
            }

            NroDocumento = trimmed;
        }

        public void SetEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Email = null;
                return;
            }

            var trimmed = email.Trim();
            if (trimmed.Length > 100)
                throw new ArgumentException("El correo electrónico no puede superar los 100 caracteres.", nameof(email));

            if (!EsEmailValido(trimmed))
                throw new ArgumentException("El formato del correo electrónico no es válido.", nameof(email));

            Email = trimmed;
        }

        public void SetTelefono(string? telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
            {
                Telefono = null;
                return;
            }

            var trimmed = telefono.Trim();
            if (trimmed.Length > 20)
                throw new ArgumentException("El teléfono no puede superar los 20 caracteres.", nameof(telefono));

            Telefono = trimmed;
        }

        private static void ValidarFormatoDni(string documento)
        {
            if (documento.Length < 7 || documento.Length > 8 || !documento.All(char.IsDigit))
                throw new ArgumentException("Para tipo de documento DNI, el número debe contener entre 7 y 8 dígitos numéricos.", nameof(documento));
        }

        private static bool EsEmailValido(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}