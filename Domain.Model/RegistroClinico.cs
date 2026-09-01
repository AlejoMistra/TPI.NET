namespace Domain.Model
{
    public class RegistroClinico
    {
        public int Id { get; private set; }
        public TipoRegistroClinico Tipo { get; private set; }
        public string Descripcion { get; private set; }
        public DateTime Fecha { get; private set; }

        public int HistoriaClinicaId { get; private set; }
        public int ProfesionalId { get; private set; }   // quién lo registró
        public int? TurnoId { get; private set; }        // en qué turno se originó (opcional)

        // Para EF Core (materialización sin constructor público)
        private RegistroClinico() { Descripcion = string.Empty; }

        public RegistroClinico(TipoRegistroClinico tipo, string descripcion, Profesional profesional, HistoriaClinica historiaClinica, Turno? turnoOrigen = null)
        {
            Tipo = tipo;
            Descripcion = descripcion;
            Fecha = DateTime.UtcNow;

            ProfesionalId = profesional.Id;
            HistoriaClinicaId = historiaClinica.Id;
            TurnoId = turnoOrigen?.Id;
        }
    }

    public enum TipoRegistroClinico
    {
        Alergia,
        Antecedente,
        Tratamiento,
        Evolucion,
        Diagnostico,
        NotaClinica,
    }
}