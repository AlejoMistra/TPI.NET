namespace Domain.Model
{
    public abstract class Persona(string nombre, string apellido, string tipoDocumento, string nroDocumento, string? telefono = null, string? email = null)
    {
        public enum TipoDocumentoEnum
        {
            DNI,
            Pasaporte,
            Otro
        }
        public int Id { get; private set; }
        public string Nombre { get; private set; } = nombre;

        public string Apellido { get; private set; } = apellido;

        public string TipoDocumento { get; private set; } = tipoDocumento;
        public string NroDocumento { get; private set; } = nroDocumento;

        /// <summary>Correo electrónico de contacto (institucional o personal).</summary>
        public string? Email { get; private set; } = email;

        /// <summary>Teléfono de contacto.</summary>
        public string? Telefono { get; private set; } = telefono;

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            Apellido = apellido;
        }

        public void SetNroDocumento(string nroDocumento)
        {
            NroDocumento = nroDocumento;
        }

        public void SetEmail(string? email)
        {
            Email = email;
        }

        public void SetTelefono(string? telefono)
        {
            Telefono = telefono;
        }
    }
}
