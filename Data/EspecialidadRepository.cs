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
}
