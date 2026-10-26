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
                .AsNoTracking()
                .Include(p => p.Especialidad)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Profesional>> GetAllAsync()
        {
            return await _context.Profesionales
                .AsNoTracking()
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
            existing.SetTipoDocumento(profesional.TipoDocumento);
            existing.SetNroDocumento(profesional.NroDocumento);
            existing.SetMatricula(profesional.Matricula);
            existing.SetEspecialidadId(profesional.EspecialidadId);
            existing.SetTelefono(profesional.Telefono);
            existing.SetEmail(profesional.Email);
            existing.SetEstado(profesional.Estado);

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

        public async Task<bool> ExistsWithEspecialidadAsync(int especialidadId)
        {
            return await _context.Profesionales
                .AnyAsync(p => p.EspecialidadId == especialidadId);
        }

        public async Task<bool> MatriculaExistsAsync(string matricula, int? excludeId = null)
        {
            var mat = matricula.Trim();
            return await _context.Profesionales
                .AnyAsync(p => p.Matricula == mat && (excludeId == null || p.Id != excludeId));
        }

        public async Task<bool> DocumentoExistsAsync(string nroDocumento, int? excludeId = null)
        {
            var doc = nroDocumento.Trim();
            return await _context.Profesionales
                .AnyAsync(p => p.NroDocumento == doc && (excludeId == null || p.Id != excludeId));
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var em = email.Trim();
            return await _context.Profesionales
                .AnyAsync(p => p.Email == em && (excludeId == null || p.Id != excludeId));
        }

        public async Task<bool> HasTurnosAsync(int profesionalId)
        {
            return await _context.Turnos
                .AnyAsync(t => t.ProfesionalId == profesionalId);
        }

        public async Task<bool> HasRegistrosClinicosAsync(int profesionalId)
        {
            return await _context.RegistrosClinicos
                .AnyAsync(r => r.ProfesionalId == profesionalId);
        }
    }
}