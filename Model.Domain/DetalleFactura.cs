using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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