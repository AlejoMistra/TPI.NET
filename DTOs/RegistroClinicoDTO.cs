namespace DTOs
{
    public class RegistroClinicoDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int ProfesionalId { get; set; }
        public int? TurnoId { get; set; }
    }

    public class RegistroClinicoCreateDTO
    {
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int ProfesionalId { get; set; }
        public int? TurnoId { get; set; }
    }
}
