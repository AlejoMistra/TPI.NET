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
        public decimal Precio { get; private set; }

        // Constructor privado para EF
        private Especialidad()
        {
            Nombre = string.Empty;
            Precio = 0m;
        }

        public Especialidad(int id, string nombre, decimal precio = 0m)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            Nombre = nombre;
        }

        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ArgumentException("El precio de la especialidad no puede ser negativo.", nameof(precio));

            Precio = precio;
        }
    }
}