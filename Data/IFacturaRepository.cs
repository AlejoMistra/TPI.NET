using Domain.Model;

namespace Data
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> GetAllAsync(bool includeDetalles = true);
        Task<Factura?> GetByIdAsync(int id, bool includeDetalles = true);
        Task<Factura?> GetByTurnoIdAsync(int turnoId, bool includeDetalles = true);
        Task AddAsync(Factura factura);
        Task UpdateAsync(Factura factura);
    }
}
