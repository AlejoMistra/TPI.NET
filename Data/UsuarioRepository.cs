using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TPIContext _context;

        public UsuarioRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Usuario?> GetAsync(int id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == username && u.Activo);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
