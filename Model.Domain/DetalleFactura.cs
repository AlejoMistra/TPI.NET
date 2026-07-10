using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD

namespace Model.Domain
{
    public class DetalleFactura
    {
        public int idDetalle { get; private set; }

        public string concepto { get; private set; }

        public int cantidad { get; private set; }

        public float precioUnitario { get; private set; }

        public float subtotal { get; private set; }
    }
}
=======
using System.Threading.Tasks;

namespace Model.Domain
{
    public class DetalleFactura
    {
        public int IdDetalle { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public float Subtotal => Cantidad * PrecioUnitario; 

        public int FacturaId { get; set; }
        public Factura Factura { get; set; } = null!;
    }
}
>>>>>>> 05db86fee04b42228f930689ebfc7a8c4756b331
