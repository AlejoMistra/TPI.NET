namespace DTOs
{
    public class FacturaDTO
    {
        public int Id { get; set; }
        public int TurnoId { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal MontoTotal { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string EstadoFactura { get; set; } = string.Empty;
        public List<DetalleFacturaDTO> Detalles { get; set; } = new();
    }

    public class DetalleFacturaDTO
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class FacturaCreateDTO
    {
        public int TurnoId { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public List<DetalleFacturaCreateDTO> Detalles { get; set; } = new();
    }

    public class DetalleFacturaCreateDTO
    {
        public string Concepto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
