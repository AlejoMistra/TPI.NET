using Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IProfesionalRepository
    {
        Task<IEnumerable<Profesional>> GetAllAsync();
        Task<Profesional?> GetByIdAsync(int id);
        Task AddAsync(Profesional profesional);
        Task<bool> UpdateAsync(Profesional profesional);
        Task<bool> DeleteAsync(int id);
    }
}
