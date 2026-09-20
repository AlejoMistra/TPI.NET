using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly TPIContext _context;

        public PacienteRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Paciente paciente)
        {
            await _context.Pacientes.AddAsync(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Paciente paciente)
        {
            var existing = await _context.Pacientes.FindAsync(paciente.Id);
            if (existing == null)
                return false;

            existing.SetNombre(paciente.Nombre);
            existing.SetApellido(paciente.Apellido);
            existing.SetNroDocumento(paciente.NroDocumento);
            existing.SetTelefono(paciente.Telefono);
            existing.SetEmail(paciente.Email);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return false;

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _context.Pacientes.AnyAsync(p => p.Email == email && (excludeId == null || p.Id != excludeId));
        }
    }
}