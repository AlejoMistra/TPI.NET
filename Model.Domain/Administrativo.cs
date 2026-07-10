using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Administrativo : Persona(string nombre, string apellido, string dni)
    {
        public int Id { get; private set; }

        public string Legajo { get; private set; };

        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
