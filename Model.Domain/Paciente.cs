using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    internal class Paciente : Persona
    {
        public int Id { get; private set; }

        public DateTime FechaNacimiento { get; private set; }

        public string ObraSocial { get; private set; } = string.Empty;
    }
}
