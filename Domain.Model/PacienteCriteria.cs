namespace Domain.Model
{
    public class PacienteCriteria
    {
        public string Texto { get; private set; }
        public string? ObraSocial { get; private set; }
        public int? EspecialidadId { get; private set; }
        public int? ProfesionalId { get; private set; }

        public PacienteCriteria(string? texto, string? obraSocial = null, int? especialidadId = null, int? profesionalId = null)
        {
            Texto = texto?.Trim() ?? string.Empty;
            ObraSocial = string.IsNullOrWhiteSpace(obraSocial) ? null : obraSocial.Trim();
            EspecialidadId = especialidadId > 0 ? especialidadId : null;
            ProfesionalId = profesionalId > 0 ? profesionalId : null;
        }
    }
}
