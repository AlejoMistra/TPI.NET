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

    public Task<IEnumerable<Especialidad>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Especialidad>>(_especialidades.OrderBy(e => e.Nombre).ToList());
    }

    //Metodo interno sincrono para obtener todas las especialidades
    internal IEnumerable<Especialidad> GetAllSync()
    {
        return _especialidades.OrderBy(e => e.Nombre).ToList();
    }
}
