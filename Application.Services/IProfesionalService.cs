using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IProfesionalService
    {
        Task<ProfesionalDTO> AddAsync(ProfesionalDTO profesional);
        Task<IEnumerable<ProfesionalDTO>> GetAllAsync();
        Task<ProfesionalDTO?> GetByIdAsync(int id);
        Task<ProfesionalDTO?> UpdateAsync(ProfesionalDTO profesional);
        Task<bool> DeleteAsync(int id);
    }
}
