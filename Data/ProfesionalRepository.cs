 using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ProfesionalRepository : IProfesionalRepository
    {
        private readonly TPIContext _context;

        public ProfesionalRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Profesional profesional)
        {
            _context.Profesionales.Add(profesional);
            await _context.SaveChangesAsync();
        }

        public async Task<Profesional?> GetByIdAsync(int id)
        {
            return await _context.Profesionales
                .Include(p => p.Especialidad)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Profesional>> GetAllAsync()
        {
            return await _context.Profesionales
                .Include(p => p.Especialidad)
                .ToListAsync();
        }

        public async Task<Profesional?> UpdateAsync(Profesional profesional)
        {
            var existing = await _context.Profesionales
                .FirstOrDefaultAsync(p => p.Id == profesional.Id);

            if (existing == null)
                return null;

            existing.SetNombre(profesional.Nombre);
            existing.SetApellido(profesional.Apellido);
            existing.SetMatricula(profesional.Matricula);
            existing.SetEspecialidadId(profesional.EspecialidadId);

            await _context.SaveChangesAsync();

            // Recargar la navegación para devolver el objeto completo
            await _context.Entry(existing)
                .Reference(p => p.Especialidad)
                .LoadAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profesional = await _context.Profesionales.FindAsync(id);
            if (profesional == null)
                return false;

            _context.Profesionales.Remove(profesional);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
