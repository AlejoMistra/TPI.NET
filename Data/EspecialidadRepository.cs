using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class EspecialidadRepository : IEspecialidadRepository
{
    private readonly TPIContext _context;

    public EspecialidadRepository(TPIContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Especialidad especialidad)
    {
        _context.Especialidades.Add(especialidad);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Especialidad>> GetAllAsync()
    {
        return await _context.Especialidades
            .OrderBy(e => e.Nombre)
            .ToListAsync();
    }

    public async Task<Especialidad?> GetByIdAsync(int id)
    {
        return await _context.Especialidades.FindAsync(id);
    }

    public async Task<Especialidad?> UpdateAsync(Especialidad especialidad)
    {
        var existing = await _context.Especialidades.FindAsync(especialidad.Id);
        if (existing == null)
            return null;

        existing.SetNombre(especialidad.Nombre);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var especialidad = await _context.Especialidades.FindAsync(id);
        if (especialidad == null)
            return false;

        _context.Especialidades.Remove(especialidad);

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            // FK Restrict: hay Profesionales que referencian esta Especialidad
            return false;
        }
    }
}
