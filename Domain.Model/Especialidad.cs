namespace Domain.Model
{
    public class Especialidad
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public decimal Precio { get; private set; }

        // Constructor privado para EF Core
        private Especialidad()
        {
            Nombre = string.Empty;
            Precio = 0m;
        }

        public Especialidad(int id, string nombre, decimal precio = 0m)
        {
            SetId(id);
            SetNombre(nombre);
            SetPrecio(precio);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser menor que 0.", nameof(id));

            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la especialidad es obligatorio.", nameof(nombre));

            var trimmed = nombre.Trim();
            if (trimmed.Length > 100)
                throw new ArgumentException("El nombre de la especialidad no puede superar los 100 caracteres.", nameof(nombre));

            Nombre = trimmed;
        }

        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ArgumentException("El precio de la especialidad no puede ser negativo.", nameof(precio));

            Precio = precio;
        }
    }
}