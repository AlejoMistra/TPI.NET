namespace DTOs
{
    public class HistoriaClinicaDTO
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string GrupoSanguineo { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public List<RegistroClinicoDTO> Registros { get; set; } = new();
    }

    public class HistoriaClinicaCreateDTO
    {
        public int PacienteId { get; set; }
        public string? GrupoSanguineo { get; set; }
    }

    public class HistoriaClinicaUpdateGrupoDTO
    {
        public string GrupoSanguineo { get; set; } = string.Empty;
    }
}
