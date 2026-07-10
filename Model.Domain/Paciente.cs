using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Paciente : Persona
    {
        public int Id { get; private set; }

        public DateTime FechaNacimiento { get; private set; }

        public string ObraSocial { get; private set; } = string.Empty;

        public ICollection<Turno> Turnos { get; set; }

        public HistoriaClinica historia_clinica { get; private set; }

        public int id_historia_c {  get; private set; }

        public Paciente(string nombre, string apellido, string nroDocumento)  : base (nombre, apellido, nroDocumento)
        {
            
            Turnos = new List<Turno>();
            historia_clinica = new HistoriaClinica();
        }
    }
}
