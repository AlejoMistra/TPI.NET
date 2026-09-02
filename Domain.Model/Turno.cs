namespace Domain.Model
{
    public class Turno
    {
        public enum EstadosTurno
        {
            Pendiente, Confirmado, Atendido, Ausente, Cancelado, Reprogramado
        }

        public int Id { get; private set; }
        public DateTime FechaHoraInicio { get; private set; }
        public DateTime FechaHoraFin { get; private set; }
        public string Motivo { get; private set; } = string.Empty;
        public EstadosTurno EstadoTurno { get; private set; }
        public string Observacion { get; private set; } = string.Empty;

        // Factura — ignorada en EF Core hasta implementar facturación
        public Factura? Factura { get; private set; }
        public int FacturaId { get; private set; }

        // Participantes del turno
        public Profesional? Profesional { get; private set; }
        public int ProfesionalId { get; private set; }

        public Paciente? Paciente { get; private set; }
        public int PacienteId { get; private set; }

        public Usuario? Usuario { get; private set; }
        public int UsuarioId { get; private set; }

        // Registros clínicos originados en este turno (navegación inversa de solo lectura)
        private readonly List<RegistroClinico> _registros = new();
        public IReadOnlyCollection<RegistroClinico> Registros => _registros.AsReadOnly();

       
        /// Registra un RegistroClinico en la historia del paciente a partir de este turno.
        /// Valida que el turno está en estado Atendido, sino InvalidOperationExeption
        public RegistroClinico Registrar(TipoRegistroClinico tipo, string descripcion,
            Profesional profesional, HistoriaClinica historiaClinica)
        {
            if (EstadoTurno != EstadosTurno.Atendido)
                throw new InvalidOperationException(
                    $"Solo se pueden registrar datos clínicos en turnos con estado Atendido. " +
                    $"Estado actual: {EstadoTurno}.");

            return historiaClinica.AgregarRegistro(tipo, descripcion, profesional, this);
        }
    }
}