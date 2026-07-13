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

<<<<<<< HEAD
        public string? Legajo { get; private set; }
=======
        public string Legajo { get; private set; };
>>>>>>> 179740643451a3894a187559ec63247f100cf54e


        public Administrativo(string nombre, string apellido, string nroDocumento, string legajo) : base(nombre, apellido, nroDocumento)
        {
            
            
        }
    }
}