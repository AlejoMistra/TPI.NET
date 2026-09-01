namespace DTOs
{
  public class PacienteDTO
  {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string TipoDocumento { get; set; } = string.Empty;
    public string NroDocumento { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string ObraSocial { get; set; } = string.Empty;
  }
}