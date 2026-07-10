using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Domain
{
    public class ConsultaMedica
    {
        
        public Turno turno { get; private set; } 
        public int idConsultaMedica { get; private set; }

        public ICollection <string> sintomas { get; private set; } = new List<string>();

        public ICollection<string> notasClinicas { get; private set; } = new List<string>();

        public ICollection<string> diagnostico { get; private set; } = new List<string>();


        public ConsultaMedica()
        {
            turno = new Turno();
        }
    }
}