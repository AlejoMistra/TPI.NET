using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Model
{
    public class DetalleFactura
    {
        public int Id { get; private set; }
        public int FacturaId { get; private set; }
        public string Concepto { get; private set; }
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        // Constructor privado para EF Core
        private DetalleFactura()
        {
            Concepto = string.Empty;
        }

        public DetalleFactura(string concepto, int cantidad, decimal precioUnitario, int facturaId = 0)
        {
            if (string.IsNullOrWhiteSpace(concepto))
                throw new ArgumentException("El concepto del detalle de factura es obligatorio.", nameof(concepto));

            var conceptoLimpio = concepto.Trim();
            if (conceptoLimpio.Length > 200)
                throw new ArgumentException("El concepto no puede exceder los 200 caracteres.", nameof(concepto));

            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cantidad));

            if (precioUnitario <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor que cero.", nameof(precioUnitario));

            Concepto = conceptoLimpio;
            Cantidad = cantidad;
            PrecioUnitario = decimal.Round(precioUnitario, 2);
            FacturaId = facturaId;
        }
    }
}
