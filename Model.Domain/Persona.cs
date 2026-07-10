namespace Model.Domain
{
    public abstract class Persona (string nombre, string apellido, string nroDocumento)
    {
        public string Nombre { get; private set; } = nombre;

        public string Apellido { get; private set; } = apellido;

        public string NroDocumento { get; private set; } = nroDocumento;
        public string Email { get; private set; } = string.Empty;

        public string Telefono { get; private set; } = string.Empty;

        
    }
}
