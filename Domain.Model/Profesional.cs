namespace Domain.Model
{
    public class Profesional : Persona
    {
        public enum EstadoProfesional
        {
            Activo,
            Inactivo
        }

        public string Matricula { get; private set; } = string.Empty;

        public int EspecialidadId { get; private set; }

        public Especialidad? Especialidad { get; private set; }

        public EstadoProfesional Estado { get; private set; }

        // Constructor para EF Core
        private Profesional() : base()
        {
            Matricula = string.Empty;
            Estado = EstadoProfesional.Activo;
        }

        public Profesional(
            string nombre,
            string apellido,
            string tipoDocumento,
            string nroDocumento,
            string matricula,
            int especialidadId,
            string? telefono = null,
            string? email = null,
            EstadoProfesional estado = EstadoProfesional.Activo)
            : base(nombre, apellido, tipoDocumento, nroDocumento, telefono, email)
        {
            SetMatricula(matricula);
            SetEspecialidadId(especialidadId);
            SetEstado(estado);
        }

        public void SetMatricula(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("La matrícula del profesional es obligatoria.", nameof(matricula));

            var trimmed = matricula.Trim();
            if (trimmed.Length > 50)
                throw new ArgumentException("La matrícula no puede superar los 50 caracteres.", nameof(matricula));

            Matricula = trimmed;
        }

        public void SetEspecialidadId(int especialidadId)
        {
            if (especialidadId <= 0)
                throw new ArgumentException("Debe seleccionar una especialidad válida con un ID mayor a cero.", nameof(especialidadId));

            EspecialidadId = especialidadId;

            // Solo invalidar si hay inconsistencia
            if (Especialidad != null && Especialidad.Id != EspecialidadId)
            {
                Especialidad = null; // Invalidar navigation property
            }
        }

        public void SetEspecialidad(Especialidad especialidad)
        {
            ArgumentNullException.ThrowIfNull(especialidad);

            Especialidad = especialidad;
            EspecialidadId = especialidad.Id; // Asegurar consistencia
        }

        public void SetEstado(EstadoProfesional estado)
        {
            if (!Enum.IsDefined(typeof(EstadoProfesional), estado))
                throw new ArgumentException("El estado del profesional no es válido.", nameof(estado));

            Estado = estado;
        }
    }
}