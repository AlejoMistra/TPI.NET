namespace Domain.Model
{
    public abstract class Persona(string nombre, string apellido, string tipoDocumento, string nroDocumento)
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
        public string Email { get; private set; } = string.Empty;

        public string Telefono { get; private set; } = string.Empty;

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
    }
}
