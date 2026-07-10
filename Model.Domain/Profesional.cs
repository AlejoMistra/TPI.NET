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

        public Especialidad? EspecialidadProfesional { get; private set; }

        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}


