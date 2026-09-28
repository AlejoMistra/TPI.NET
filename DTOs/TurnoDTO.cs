namespace DTOs
{
  public class TurnoDTO
  {
    public int Id { get; set; }
    public DateTime FechaHoraInicio { get; set; }
    public DateTime FechaHoraFin { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string EstadoTurno { get; set; } = string.Empty;
    public string Observaciones { get; set; } = string.Empty;

    public int? FacturaId { get; set; }

    public int ProfesionalId { get; set; }
    public int? PacienteId { get; set; }
  }
}