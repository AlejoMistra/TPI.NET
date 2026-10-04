namespace Domain.Model
{
    public class Turno
    {
        public enum EstadosTurno
        {
            Libre, Asignado, Presente, Atendido, Ausente
        }

        public int Id { get; private set; }
        public DateTime FechaHoraInicio { get; private set; }
        public DateTime FechaHoraFin { get; private set; }
        public string Motivo { get; private set; } = string.Empty;
        public EstadosTurno EstadoTurno { get; private set; }
        public string Observaciones { get; private set; } = string.Empty;
        public DateTime? FechaHoraLlegada { get; private set; }

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
            int? pacienteId,
            DateTime? fechaHoraLlegada = null
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
            FechaHoraLlegada = fechaHoraLlegada;
        }

        /// <summary>
        /// Asigna el turno a un paciente, pasando su estado a Asignado.
        /// Valida que el turno esté en estado Libre y que su fecha/hora no esté en el pasado.
        /// </summary>
        public void Asignar(int pacienteId, string? motivo = null, string? observaciones = null)
        {
            if (EstadoTurno != EstadosTurno.Libre)
                throw new InvalidOperationException($"Solo se puede asignar un turno en estado Libre. Estado actual: {EstadoTurno}.");

            if (FechaHoraInicio < DateTime.Now)
                throw new InvalidOperationException("No se puede asignar un turno cuya fecha y hora ya ha pasado.");

            if (pacienteId <= 0)
                throw new ArgumentException("El ID de paciente debe ser mayor que cero.", nameof(pacienteId));

            PacienteId = pacienteId;
            EstadoTurno = EstadosTurno.Asignado;
            if (motivo != null)
                Motivo = motivo;
            if (observaciones != null)
                Observaciones = observaciones;
        }

        /// <summary>
        /// Libera un turno asignado, quitando el paciente y devolviéndolo a estado Libre sin conservar historial.
        /// </summary>
        public void Liberar()
        {
            if (EstadoTurno != EstadosTurno.Asignado)
                throw new InvalidOperationException($"Solo se puede liberar un turno en estado Asignado. Estado actual: {EstadoTurno}.");

            if (FacturaId.HasValue)
                throw new InvalidOperationException("No se puede liberar un turno que posee una factura activa. Debe anular la factura primero.");

            PacienteId = null;
            Paciente = null;
            Motivo = string.Empty;
            EstadoTurno = EstadosTurno.Libre;
        }

        /// <summary>
        /// Registra la llegada del paciente a la sala de espera, pasando el turno a estado Presente y registrando la fecha/hora de llegada.
        /// </summary>
        public void RegistrarLlegada()
        {
            if (EstadoTurno != EstadosTurno.Asignado)
                throw new InvalidOperationException($"Solo se puede registrar la llegada de un turno en estado Asignado. Estado actual: {EstadoTurno}.");

            EstadoTurno = EstadosTurno.Presente;
            FechaHoraLlegada = DateTime.Now;
        }

        /// <summary>
        /// Revierte la llegada de un paciente registrado por error como Presente, devolviéndolo a Asignado y limpiando FechaHoraLlegada.
        /// </summary>
        public void RevertirLlegada()
        {
            if (EstadoTurno != EstadosTurno.Presente)
                throw new InvalidOperationException($"Solo se puede revertir la llegada de un turno en estado Presente. Estado actual: {EstadoTurno}.");

            EstadoTurno = EstadosTurno.Asignado;
            FechaHoraLlegada = null;
        }

        /// <summary>
        /// Marca el turno como Ausente si el paciente no se presentó (desde Asignado, FechaHoraLlegada queda null)
        /// o si se retiró antes de ser atendido (desde Presente, FechaHoraLlegada conserva su valor).
        /// </summary>
        public void MarcarAusente()
        {
            if (EstadoTurno != EstadosTurno.Asignado && EstadoTurno != EstadosTurno.Presente)
                throw new InvalidOperationException($"Solo se puede marcar como ausente un turno en estado Asignado o Presente. Estado actual: {EstadoTurno}.");

            EstadoTurno = EstadosTurno.Ausente;
        }

        /// <summary>
        /// Registra el inicio de atención clínica por parte del profesional, pasando el turno a estado Atendido.
        /// </summary>
        public void Atender()
        {
            if (EstadoTurno != EstadosTurno.Presente)
                throw new InvalidOperationException($"Solo se puede atender un turno cuando el paciente está Presente. Estado actual: {EstadoTurno}.");

            EstadoTurno = EstadosTurno.Atendido;
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

        /// <summary>
        /// Asocia una factura emitida al turno.
        /// Valida que el turno esté en estado Asignado, Presente o Atendido y que no tenga otra factura activa.
        /// </summary>
        public void AsociarFactura(int facturaId)
        {
            if (EstadoTurno != EstadosTurno.Asignado &&
                EstadoTurno != EstadosTurno.Presente &&
                EstadoTurno != EstadosTurno.Atendido)
            {
                throw new InvalidOperationException(
                    $"Solo se puede facturar un turno en estado Asignado, Presente o Atendido. Estado actual: {EstadoTurno}.");
            }

            if (FacturaId.HasValue)
                throw new InvalidOperationException("El turno ya posee una factura activa asociada.");

            if (facturaId <= 0)
                throw new ArgumentException("El ID de la factura debe ser mayor que cero.", nameof(facturaId));

            FacturaId = facturaId;
        }

        /// <summary>
        /// Desasocia la factura activa del turno (por ejemplo, cuando la factura se anula),
        /// permitiendo volver a facturar el turno.
        /// </summary>
        public void DesasociarFactura()
        {
            if (!FacturaId.HasValue)
                throw new InvalidOperationException("El turno no posee una factura activa para desasociar.");

            FacturaId = null;
            Factura = null;
        }
    }
}