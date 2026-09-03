using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Profesional : Persona
    {
        public string Matricula { get; private set; }

        public int EspecialidadId { get; private set; }

        public Especialidad? Especialidad { get; private set; }

        // Constructor para EF
        private Profesional() : base(string.Empty, string.Empty, string.Empty, string.Empty)
        {
            Matricula = string.Empty;
        }

        public Profesional(string nombre, string apellido, string tipoDocumento, string nroDocumento, string matricula, int especialidadId)
            : base(nombre, apellido, tipoDocumento, nroDocumento)
        {
            Matricula = matricula;
            EspecialidadId = especialidadId;
        }

        public void SetMatricula(string matricula)
        {
            Matricula = matricula;
        }

        public void SetEspecialidadId(int especialidadId)
        {
            EspecialidadId = especialidadId;

            // Solo invalidar si hay inconsistencia
            if (Especialidad != null && Especialidad.Id != EspecialidadId)
            {
                Especialidad = null; // Invalidar navigation property
            }
        }

        public void SetEspecialidad(Especialidad especialidad)
        {
            if (especialidad == null)
                throw new ArgumentNullException(nameof(especialidad));

            Especialidad = especialidad;
            EspecialidadId = especialidad.Id; // Asegurar consistencia
        }
    }
}
