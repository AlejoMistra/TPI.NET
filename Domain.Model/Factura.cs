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
        public int TurnoId { get; private set; }
        public DateTime FechaEmision { get; private set; }
        public decimal MontoTotal { get; private set; }
        public MetodosPago MetodoPago { get; private set; }
        public EstadosFactura EstadoFactura { get; private set; }

        private readonly List<DetalleFactura> _detallesFactura = new();
        public IReadOnlyCollection<DetalleFactura> DetallesFactura => _detallesFactura.AsReadOnly();

        // Constructor privado para EF Core
        private Factura()
        {
        }

        public Factura(int turnoId, MetodosPago metodoPago, DateTime? fechaEmision = null)
        {
            if (turnoId <= 0)
                throw new ArgumentException("El ID del turno debe ser mayor que cero.", nameof(turnoId));

            TurnoId = turnoId;
            MetodoPago = metodoPago;
            FechaEmision = fechaEmision ?? DateTime.Now;
            EstadoFactura = EstadosFactura.Pagada;
            MontoTotal = 0m;
        }

        /// <summary>
        /// Agrega un renglón de detalle a la factura y recalcula el monto total de la cabecera.
        /// </summary>
        public DetalleFactura AgregarDetalle(string concepto, int cantidad, decimal precioUnitario)
        {
            if (EstadoFactura == EstadosFactura.Cancelada)
                throw new InvalidOperationException("No se pueden agregar detalles a una factura cancelada.");

            var detalle = new DetalleFactura(concepto, cantidad, precioUnitario, Id);
            _detallesFactura.Add(detalle);
            RecalcularMontoTotal();
            return detalle;
        }

        /// <summary>
        /// Anula la factura pasándola al estado Cancelada.
        /// </summary>
        public void Anular()
        {
            if (EstadoFactura == EstadosFactura.Cancelada)
                throw new InvalidOperationException("La factura ya se encuentra cancelada.");

            EstadoFactura = EstadosFactura.Cancelada;
        }

        private void RecalcularMontoTotal()
        {
            MontoTotal = _detallesFactura.Sum(d => d.Subtotal);
        }
    }
}
