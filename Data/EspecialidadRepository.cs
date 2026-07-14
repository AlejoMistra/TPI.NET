using Model.Domain;

namespace Data;

public class EspecialidadRepository : IEspecialidadRepository
{
    private static readonly List<Especialidad> _especialidades = new List<Especialidad>
    {
        new Especialidad(1, "Cardiología"),
        new Especialidad(2, "Dermatología"),
        new Especialidad(3, "Neurología")
    };
    private static int _nextId = _especialidades.Max(e => e.Id) + 1;

    public Task AddAsync(Especialidad especialidad)
    {
        // Simula auto-incremento de ID
        especialidad.SetId(_nextId++);

        _especialidades.Add(especialidad);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Especialidad>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Especialidad>>(_especialidades.OrderBy(e => e.Nombre).ToList());
    }

    //Metodo interno sincrono para obtener todas las especialidades
    internal IEnumerable<Especialidad> GetAllSync()
    {
        return _especialidades.OrderBy(e => e.Nombre).ToList();
    }

    public Task<Especialidad?> GetByIdAsync(int id)
    {
        var especialidad = _especialidades.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(especialidad);
    }


    public Task<Especialidad?> UpdateAsync(Especialidad especialidad)
    {
        var existingEspecialidad = _especialidades.FirstOrDefault(e => e.Id == especialidad.Id);
        if (existingEspecialidad != null)
        {
            // Actualizar propiedades
            existingEspecialidad.SetNombre(especialidad.Nombre);
            return Task.FromResult<Especialidad?>(existingEspecialidad);
        }
        return Task.FromResult<Especialidad?>(null);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var especialidad = _especialidades.FirstOrDefault(e => e.Id == id);
        if (especialidad != null)
        {
            _especialidades.Remove(especialidad);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
