using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly TPIContext _context;

        public FacturaRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Factura>> GetAllAsync(bool includeDetalles = true)
        {
            IQueryable<Factura> query = _context.Facturas;
            if (includeDetalles)
            {
                query = query.Include(f => f.DetallesFactura);
            }
            return await query.OrderByDescending(f => f.FechaEmision).ToListAsync();
        }

        public async Task<Factura?> GetByIdAsync(int id, bool includeDetalles = true)
        {
            IQueryable<Factura> query = _context.Facturas;
            if (includeDetalles)
            {
                query = query.Include(f => f.DetallesFactura);
            }
            return await query.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Factura?> GetByTurnoIdAsync(int turnoId, bool includeDetalles = true)
        {
            IQueryable<Factura> query = _context.Facturas;
            if (includeDetalles)
            {
                query = query.Include(f => f.DetallesFactura);
            }
            return await query
                .Where(f => f.TurnoId == turnoId)
                .OrderByDescending(f => f.FechaEmision)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Factura factura)
        {
            _context.Facturas.Update(factura);
            await _context.SaveChangesAsync();
        }
    }
}
