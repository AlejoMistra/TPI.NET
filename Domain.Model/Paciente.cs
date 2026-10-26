namespace Domain.Model
{
    public class Paciente : Persona
    {
        private static readonly DateTime MinFechaNacimiento = new(1900, 1, 1);

        public DateTime FechaNacimiento { get; private set; }
        public string ObraSocial { get; private set; } = string.Empty;

        // Turnos — ignorados en EF Core hasta implementar
        public ICollection<Turno> Turnos { get; private set; } = new List<Turno>();

        // Navegación inversa hacia HistoriaClinica (FK está en HistoriaClinica.PacienteId)
        public HistoriaClinica? HistoriaClinica { get; private set; }

        // Constructor privado para EF Core
        private Paciente() : base()
        {
        }

        public Paciente(
            string nombre,
            string apellido,
            string tipoDocumento,
            string nroDocumento,
            DateTime fechaNacimiento,
            string obraSocial = "",
            string? telefono = null,
            string? email = null)
            : base(nombre, apellido, tipoDocumento, nroDocumento, telefono, email)
        {
            SetFechaNacimiento(fechaNacimiento);
            SetObraSocial(obraSocial);
            Turnos = new List<Turno>();
        }

        public void SetFechaNacimiento(DateTime fechaNacimiento)
        {
            var fecha = fechaNacimiento.Date;
            if (fecha < MinFechaNacimiento || fecha > DateTime.Today)
            {
                throw new ArgumentException($"La fecha de nacimiento debe ser una fecha válida entre {MinFechaNacimiento:dd/MM/yyyy} y hoy.", nameof(fechaNacimiento));
            }

            FechaNacimiento = fecha;
        }

        public void SetObraSocial(string? obraSocial)
        {
            if (string.IsNullOrWhiteSpace(obraSocial))
            {
                ObraSocial = string.Empty;
                return;
            }

            var trimmed = obraSocial.Trim();
            if (trimmed.Length > 100)
            {
                throw new ArgumentException("La obra social no puede superar los 100 caracteres.", nameof(obraSocial));
            }

            ObraSocial = trimmed;
        }
    }
}
