using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Especialidad
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        // Constructor privado para EF
        private Especialidad()
        {
            Nombre = string.Empty;
        }

        public Especialidad(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            Nombre = nombre;
        }
    }
}