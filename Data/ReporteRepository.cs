using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly TPIContext _context;

        public ReporteRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task<List<Turno>> GetTurnosParaReporteAsync(
            DateTime desde,
            DateTime hasta,
            int? profesionalId,
            int? especialidadId)
        {
            var query = _context.Turnos
                .AsNoTracking()
                .Include(t => t.Profesional)
                    .ThenInclude(p => p.Especialidad)
                .Include(t => t.Paciente)
                .Include(t => t.Factura)
                    .ThenInclude(f => f!.DetallesFactura)
                .Where(t => t.FechaHoraInicio >= desde && t.FechaHoraInicio <= hasta);

            if (profesionalId.HasValue && profesionalId.Value > 0)
                query = query.Where(t => t.ProfesionalId == profesionalId.Value);

            if (especialidadId.HasValue && especialidadId.Value > 0)
                query = query.Where(t => t.Profesional != null && t.Profesional.EspecialidadId == especialidadId.Value);

            return await query.OrderByDescending(t => t.FechaHoraInicio).ToListAsync();
        }

        public async Task<List<Factura>> GetFacturasEnPeriodoAsync(
            DateTime desde,
            DateTime hasta,
            int? profesionalId)
        {
            var query = _context.Facturas
                .AsNoTracking()
                .Include(f => f.DetallesFactura)
                .Where(f => f.FechaEmision >= desde && f.FechaEmision <= hasta && f.EstadoFactura != Factura.EstadosFactura.Cancelada);

            return await query.ToListAsync();
        }
    }
}
