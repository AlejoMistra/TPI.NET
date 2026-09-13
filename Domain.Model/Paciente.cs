namespace Domain.Model
{
    public class Paciente : Persona
    {
        public DateTime FechaNacimiento { get; private set; }
        public string ObraSocial { get; private set; } = string.Empty;

        // Turnos — ignorados en EF Core hasta implementar
        public ICollection<Turno> Turnos { get; private set; } = new List<Turno>();

        // Navegación inversa hacia HistoriaClinica (FK está en HistoriaClinica.PacienteId)
        public HistoriaClinica? HistoriaClinica { get; private set; }

        public Paciente(
            string nombre,
            string apellido,
            string tipoDocumento,
            string nroDocumento,
            string? telefono = null,
            string? email = null)
            : base(nombre, apellido, tipoDocumento, nroDocumento, telefono, email)
        {
            Turnos = new List<Turno>();
        }
    }
}
