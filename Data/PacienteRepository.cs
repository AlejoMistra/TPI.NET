using Domain.Model;

namespace Data
{
    public class PacienteRepository : IPacienteRepository
    {
        //private readonly TPIContext _context;

        public PacienteRepository() //TPIContext context
        {
            //this._context = context;
        }

        public async Task AddAsync(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        public Task<Paciente?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Paciente>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            throw new NotImplementedException();
        }
    }
}