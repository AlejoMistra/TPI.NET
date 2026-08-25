using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Administrativo : Persona
    {
        public string Legajo { get; private set; }

        public Administrativo(string nombre, string apellido, string tipoDocumento, string nroDocumento, string legajo) : base(nombre, apellido, tipoDocumento, nroDocumento)
        {
            Legajo = legajo;
        }
    }
}