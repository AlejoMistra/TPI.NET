using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    internal class Factura
    {
        public enum MetodosPago
        {
            Efectivo,
            TarjetaCredito,
            TarjetaDebito,
            TransferenciaBancaria
        }

        public int Id { get; private set; }
        public DateTime FechaEmision { get; private set; }

        public float MontoTotal { get; private set; }

        public MetodosPago MetodoPago { get; set; }
    }
}
