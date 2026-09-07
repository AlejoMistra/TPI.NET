using Domain.Model;

namespace Data
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetAsync(int id);
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        Task<bool> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
    }
}
