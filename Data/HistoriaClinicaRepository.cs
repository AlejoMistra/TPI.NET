using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HistoriaClinicaRepository : IHistoriaClinicaRepository
    {
        private readonly TPIContext _context;

        public HistoriaClinicaRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task<HistoriaClinica?> GetByIdAsync(int id, bool includeRegistros = true)
        {
            IQueryable<HistoriaClinica> query = _context.HistoriasClinicas;
            if (includeRegistros)
            {
                query = query.Include(h => h.RegistrosClinicos);
            }
            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<HistoriaClinica?> GetByPacienteIdAsync(int pacienteId, bool includeRegistros = true)
        {
            IQueryable<HistoriaClinica> query = _context.HistoriasClinicas;
            if (includeRegistros)
            {
                query = query.Include(h => h.RegistrosClinicos);
            }
            return await query.FirstOrDefaultAsync(h => h.PacienteId == pacienteId);
        }

        public async Task AddAsync(HistoriaClinica historiaClinica)
        {
            await _context.HistoriasClinicas.AddAsync(historiaClinica);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HistoriaClinica historiaClinica)
        {
            _context.HistoriasClinicas.Update(historiaClinica);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsForPacienteAsync(int pacienteId)
        {
            return await _context.HistoriasClinicas.AnyAsync(h => h.PacienteId == pacienteId);
        }

        public async Task AddRegistroClinicoAsync(RegistroClinico registro)
        {
            if (_context.Entry(registro).State == EntityState.Detached)
            {
                await _context.RegistrosClinicos.AddAsync(registro);
            }
            await _context.SaveChangesAsync();
        }
    }
}
