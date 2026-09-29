namespace Domain.Model
{
    public class Turno
    {
        public enum EstadosTurno
        {
            Libre, Asignado, Confirmado, Atendido, Cancelado
        }

        public int Id { get; private set; }
        public DateTime FechaHoraInicio { get; private set; }
        public DateTime FechaHoraFin { get; private set; }
        public string Motivo { get; private set; } = string.Empty;
        public EstadosTurno EstadoTurno { get; private set; }
        public string Observaciones { get; private set; } = string.Empty;

        // Factura — ignorada en EF Core hasta implementar facturación
        public Factura? Factura { get; private set; }
        public int? FacturaId { get; private set; }

        // Participantes del turno
        public Profesional Profesional { get; private set; } = null!;
        public int ProfesionalId { get; private set; }

        public Paciente? Paciente { get; private set; }
        public int? PacienteId { get; private set; }

        // Registros clínicos originados en este turno (navegación inversa de solo lectura)
        private readonly List<RegistroClinico> _registros = new();
        public IReadOnlyCollection<RegistroClinico> Registros => _registros.AsReadOnly();

        // Constructor privado para EF Core
        private Turno()
        {
        }

        public Turno(
            int id,
            DateTime fechaHoraInicio,
            DateTime fechaHoraFin,
            string motivo,
            EstadosTurno estadoTurno,
            string observaciones,
            int? facturaId,
            int profesionalId,
            int? pacienteId
            )
        {
            Id = id;
            FechaHoraInicio = fechaHoraInicio;
            FechaHoraFin = fechaHoraFin;
            Motivo = motivo;
            EstadoTurno = estadoTurno;
            Observaciones = observaciones;
            FacturaId = facturaId;
            ProfesionalId = profesionalId;
            PacienteId = pacienteId;
        }

        /// <summary>
        /// Asigna el turno a un paciente, pasando su estado a Asignado.
        /// </summary>
        public void Asignar(int pacienteId, string? motivo = null)
        {
            if (EstadoTurno != EstadosTurno.Libre)
                throw new InvalidOperationException($"Solo se puede asignar un turno en estado Libre. Estado actual: {EstadoTurno}.");

            if (pacienteId <= 0)
                throw new ArgumentException("El ID de paciente debe ser mayor que cero.", nameof(pacienteId));

            PacienteId = pacienteId;
            EstadoTurno = EstadosTurno.Asignado;
            if (motivo != null)
                Motivo = motivo;
        }

        /// <summary>
        /// Libera un turno asignado o confirmado, quitando el paciente y devolviéndolo a estado Libre.
        /// </summary>
        public void Liberar()
        {
            if (EstadoTurno == EstadosTurno.Atendido)
                throw new InvalidOperationException("No se puede liberar un turno que ya fue atendido.");

            PacienteId = null;
            Paciente = null;
            EstadoTurno = EstadosTurno.Libre;
        }

        /// <summary>
        /// Cancela el turno manteniendo el paciente asociado (si lo hubiera) para trazabilidad.
        /// </summary>
        public void Cancelar()
        {
            if (EstadoTurno == EstadosTurno.Atendido)
                throw new InvalidOperationException("No se puede cancelar un turno que ya fue atendido.");

            EstadoTurno = EstadosTurno.Cancelado;
        }

        /// <summary>
        /// Confirma un turno previamente asignado.
        /// </summary>
        public void Confirmar()
        {
            if (EstadoTurno != EstadosTurno.Asignado)
                throw new InvalidOperationException($"Solo se puede confirmar un turno en estado Asignado. Estado actual: {EstadoTurno}.");

            EstadoTurno = EstadosTurno.Confirmado;
        }

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