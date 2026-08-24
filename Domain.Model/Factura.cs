using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Factura
    {
        public enum MetodosPago
        {
            Efectivo,
            TarjetaCredito,
            TarjetaDebito,
            TransferenciaBancaria
        }

        public enum EstadosFactura
        {
            Pendiente,
            Pagada,
            Cancelada
        }

        public int Id { get; private set; }
        public DateTime FechaEmision { get; private set; }

        public float MontoTotal { get; private set; }

        public MetodosPago MetodoPago { get; set; }

        public EstadosFactura EstadoFactura { get; set; }

        public ICollection<DetalleFactura> DetallesFactura { get; set; } = new List<DetalleFactura>();
    }
}
