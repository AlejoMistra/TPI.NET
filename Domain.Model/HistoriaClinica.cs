namespace Domain.Model
{
    public class HistoriaClinica
    {
        public int Id { get; private set; }
        public TypeGrupoSanguineo GrupoSanguineo { get; private set; }
        public int PacienteId { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        private readonly List<RegistroClinico> _registrosClinicos = new();
        public IReadOnlyCollection<RegistroClinico> RegistrosClinicos => _registrosClinicos.AsReadOnly();

        // Para EF Core
        private HistoriaClinica() { }

        public HistoriaClinica(int pacienteId, TypeGrupoSanguineo grupoSanguineo)
        {
            PacienteId = pacienteId;
            GrupoSanguineo = grupoSanguineo;
            FechaCreacion = DateTime.UtcNow;
        }

        /// Agrega un registro clínico a la historia. El turno origen es opcional;
        /// cuando se provee, permite trazabilidad del evento clínico que originó el registro.
        public RegistroClinico AgregarRegistro(TipoRegistroClinico tipo, string descripcion,
            Profesional profesional, Turno? turnoOrigen = null)
        {
            var registro = new RegistroClinico(tipo, descripcion, profesional, this, turnoOrigen);
            _registrosClinicos.Add(registro);
            return registro;
        }
    }

    public enum TypeGrupoSanguineo
    {
        A_POSITIVO,
        A_NEGATIVO,
        B_POSITIVO,
        B_NEGATIVO,
        AB_POSITIVO,
        AB_NEGATIVO,
        O_POSITIVO,
        O_NEGATIVO
    }
}
