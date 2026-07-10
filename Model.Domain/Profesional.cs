using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Profesional : Persona
    {
        public int Id { get; private set; }
        public string Matricula { get; private set; } = string.Empty;

        public int EspecialidadId { get; private set; }

        public Especialidad Especialidad { get; private set; } = null!;

        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();

        public Profesional(int id, string nombre, string apellido, string dni, string matricula, int especialidadId)
            : base(nombre, apellido, dni)
        {
            Id = id;
            Matricula = matricula;
            EspecialidadId = especialidadId;
        }
    }
}
