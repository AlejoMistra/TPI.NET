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
            DateTime fechaNacimiento = default,
            string obraSocial = "",
            string? telefono = null,
            string? email = null)
            : base(nombre, apellido, tipoDocumento, nroDocumento, telefono, email)
        {
            FechaNacimiento = fechaNacimiento;
            ObraSocial = obraSocial;
            Turnos = new List<Turno>();
        }

        public void SetFechaNacimiento(DateTime fechaNacimiento)
        {
            FechaNacimiento = fechaNacimiento;
        }

        public void SetObraSocial(string obraSocial)
        {
            ObraSocial = obraSocial;
        }
    }
}
