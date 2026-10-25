namespace DTOs
{
    public class PacienteCriteriaDTO
    {
        public string Texto { get; set; } = string.Empty;          // nombre, apellido o DNI
        public string? ObraSocial { get; set; }                     // null = todas; "Particular" = sin obra social
        public int? EspecialidadId { get; set; }                    // con algún turno de un profesional de la especialidad
        public int? ProfesionalId { get; set; }                     // con algún turno del profesional
    }
}
