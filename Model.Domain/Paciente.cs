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

        public ICollection<Turno> Turnos { get; set; }

        public HistoriaClinica historia_clinica { get; private set; }

        public Paciente()
        {
            Turnos = new List<Turno>();
            historia_clinica = new HistoriaClinica();
        }
    }
}
