namespace Model.Domain
{
    public abstract class Persona
    {
        public string Nombre { get; private set; } = string.Empty;

        public string Apellido { get; private set; } = string.Empty;

        public string NroDocumento { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        public string Telefono { get; private set; } = string.Empty;

        public Persona(string nombre, string apellido, string nroDocumento)
        {
            Nombre = nombre;
            Apellido = apellido;
            NroDocumento = nroDocumento;
        }
    }
}
