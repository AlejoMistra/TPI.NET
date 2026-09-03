using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Model
{
    public class DetalleFactura
    {
        public int IdDetalle { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public int FacturaId { get; set; }
        public Factura Factura { get; set; } = null!;
    }
}
