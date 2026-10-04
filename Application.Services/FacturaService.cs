using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly ITurnoRepository _turnoRepository;

        public FacturaService(IFacturaRepository facturaRepository, ITurnoRepository turnoRepository)
        {
            _facturaRepository = facturaRepository;
            _turnoRepository = turnoRepository;
        }

        public async Task<IEnumerable<FacturaDTO>> GetAllAsync()
        {
            var facturas = await _facturaRepository.GetAllAsync(includeDetalles: true);
            return facturas.Select(MapToDTO).ToList();
        }

        public async Task<FacturaDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la factura debe ser mayor que cero.", nameof(id));

            var factura = await _facturaRepository.GetByIdAsync(id, includeDetalles: true);
            return factura is not null ? MapToDTO(factura) : null;
        }

        public async Task<FacturaDTO?> GetByTurnoIdAsync(int turnoId)
        {
            if (turnoId <= 0)
                throw new ArgumentException("El ID del turno debe ser mayor que cero.", nameof(turnoId));

            var turno = await _turnoRepository.GetByIdAsync(turnoId);
            if (turno is null)
                return null;

            if (turno.FacturaId.HasValue)
            {
                var facturaActiva = await _facturaRepository.GetByIdAsync(turno.FacturaId.Value, includeDetalles: true);
                if (facturaActiva is not null)
                    return MapToDTO(facturaActiva);
            }

            var facturaPorTurno = await _facturaRepository.GetByTurnoIdAsync(turnoId, includeDetalles: true);
            return facturaPorTurno is not null ? MapToDTO(facturaPorTurno) : null;
        }

        public async Task<FacturaDTO> CreateAsync(FacturaCreateDTO dto)
        {
            if (dto.TurnoId <= 0)
                throw new ArgumentException("El ID del turno debe ser mayor que cero.", nameof(dto.TurnoId));

            if (string.IsNullOrWhiteSpace(dto.MetodoPago) ||
                !Enum.TryParse<Factura.MetodosPago>(dto.MetodoPago, ignoreCase: true, out var metodoPago))
            {
                throw new ArgumentException($"El método de pago '{dto.MetodoPago}' no es válido.", nameof(dto.MetodoPago));
            }

            if (dto.Detalles is null || dto.Detalles.Count == 0)
                throw new ArgumentException("La factura debe contener al menos un renglón de detalle.", nameof(dto.Detalles));

            var turno = await _turnoRepository.GetByIdAsync(dto.TurnoId);
            if (turno is null)
                throw new KeyNotFoundException($"No se encontró el turno con Id {dto.TurnoId}.");

            if (turno.FacturaId.HasValue)
                throw new InvalidOperationException($"El turno N° {turno.Id} ya posee una factura activa (Factura N° {turno.FacturaId.Value}).");

            if (turno.EstadoTurno != Turno.EstadosTurno.Asignado &&
                turno.EstadoTurno != Turno.EstadosTurno.Presente &&
                turno.EstadoTurno != Turno.EstadosTurno.Atendido)
            {
                throw new InvalidOperationException(
                    $"Solo se pueden facturar turnos en estado Asignado, Presente o Atendido. Estado actual: {turno.EstadoTurno}.");
            }

            var factura = new Factura(dto.TurnoId, metodoPago);

            foreach (var item in dto.Detalles)
            {
                factura.AgregarDetalle(item.Concepto, item.Cantidad, item.PrecioUnitario);
            }

            await _facturaRepository.AddAsync(factura);

            turno.AsociarFactura(factura.Id);
            await _turnoRepository.UpdateAsync(turno);

            return MapToDTO(factura);
        }

        public async Task<FacturaDTO> AnularAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la factura debe ser mayor que cero.", nameof(id));

            var factura = await _facturaRepository.GetByIdAsync(id, includeDetalles: true);
            if (factura is null)
                throw new KeyNotFoundException($"No se encontró la factura con Id {id}.");

            factura.Anular();
            await _facturaRepository.UpdateAsync(factura);

            var turno = await _turnoRepository.GetByIdAsync(factura.TurnoId);
            if (turno is not null && turno.FacturaId == factura.Id)
            {
                turno.DesasociarFactura();
                await _turnoRepository.UpdateAsync(turno);
            }

            return MapToDTO(factura);
        }

        private static FacturaDTO MapToDTO(Factura f) => new FacturaDTO
        {
            Id = f.Id,
            TurnoId = f.TurnoId,
            FechaEmision = f.FechaEmision,
            MontoTotal = f.MontoTotal,
            MetodoPago = f.MetodoPago.ToString(),
            EstadoFactura = f.EstadoFactura.ToString(),
            Detalles = f.DetallesFactura
                .Select(d => new DetalleFacturaDTO
                {
                    Id = d.Id,
                    FacturaId = d.FacturaId,
                    Concepto = d.Concepto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                })
                .ToList()
        };
    }
}
