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

        public int _especialidadId { get; private set; }

        public Especialidad? _especialidad { get; private set; }

        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();

        public Profesional(string nombre, string apellido, string tipoDocumento, string nroDocumento, string matricula, int especialidadId)
            : base(nombre, apellido, tipoDocumento, nroDocumento)
        {
            Matricula = matricula;
            _especialidadId = especialidadId;
        }

        public void SetMatricula(string matricula)
        {
            Matricula = matricula;
        }

        public void SetEspecialidadId(int especialidadId)
        {
            _especialidadId = especialidadId;

            // Solo invalidar si hay inconcistencia
            if (_especialidad != null && _especialidad.Id != _especialidadId)
            {
                _especialidad = null; // Invalidar navigation property
            }
        }

        public void SetEspecialidad(Especialidad especialidad)
        {
            if (especialidad == null)
                throw new ArgumentNullException(nameof(especialidad));

            _especialidad = especialidad;
            _especialidadId = especialidad.Id; // Asegurar consistencia
        }


    }
}


