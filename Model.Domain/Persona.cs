namespace Model.Domain
{
    internal abstract class Persona
    {
        public string Nombre { get; private set; }

        public string Apellido { get; private set; }
        
        public string NroDocumento { get; private set; }

        public string Email { get; private set; }

        public string Telefono { get; private set; 
    }
}
