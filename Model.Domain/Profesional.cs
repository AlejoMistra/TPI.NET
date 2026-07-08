using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    internal class Profesional
    {
        public int Id { get; private set; }
        public string Matricula { get; private set; }
        
        public Especialidad Especialidad { get; private set; }
    }
}
