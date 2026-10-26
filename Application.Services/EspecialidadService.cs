using Data;
using DTOs;
using Domain.Model;

namespace Application.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IEspecialidadRepository _especialidadRepository;
        private readonly IProfesionalRepository _profesionalRepository;

        public EspecialidadService(
            IEspecialidadRepository especialidadRepository,
            IProfesionalRepository profesionalRepository)
        {
            _especialidadRepository = especialidadRepository;
            _profesionalRepository = profesionalRepository;
        }

        public async Task<EspecialidadDTO> AddAsync(EspecialidadDTO especialidadDto)
        {
            ArgumentNullException.ThrowIfNull(especialidadDto);

            var especialidad = new Especialidad(0, especialidadDto.Nombre, especialidadDto.Precio);

            if (await _especialidadRepository.ExistsByNameAsync(especialidad.Nombre))
            {
                throw new ArgumentException($"Ya existe una especialidad con el nombre '{especialidad.Nombre}'.", nameof(especialidadDto.Nombre));
            }

            await _especialidadRepository.AddAsync(especialidad);

            return new EspecialidadDTO
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
                Precio = especialidad.Precio
            };
        }

        public async Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
        {
            var especialidades = await _especialidadRepository.GetAllAsync();
            return especialidades.Select(e => new EspecialidadDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Precio = e.Precio
            }).ToList();
        }

        public async Task<EspecialidadDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            }

            var especialidad = await _especialidadRepository.GetByIdAsync(id);

            if (especialidad == null)
            {
                return null;
            }

            return new EspecialidadDTO
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
                Precio = especialidad.Precio
            };
        }

        public async Task<EspecialidadDTO?> UpdateAsync(EspecialidadDTO especialidadDto)
        {
            ArgumentNullException.ThrowIfNull(especialidadDto);

            if (especialidadDto.Id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(especialidadDto.Id));
            }

            var especialidad = new Especialidad(especialidadDto.Id, especialidadDto.Nombre, especialidadDto.Precio);

            if (await _especialidadRepository.ExistsByNameAsync(especialidad.Nombre, especialidad.Id))
            {
                throw new ArgumentException($"Ya existe otra especialidad con el nombre '{especialidad.Nombre}'.", nameof(especialidadDto.Nombre));
            }

            var updatedEspecialidad = await _especialidadRepository.UpdateAsync(especialidad);

            if (updatedEspecialidad == null)
            {
                return null;
            }

            return new EspecialidadDTO
            {
                Id = updatedEspecialidad.Id,
                Nombre = updatedEspecialidad.Nombre,
                Precio = updatedEspecialidad.Precio
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            }

            // Regla de negocio: no se puede eliminar una especialidad con profesionales asociados
            bool tieneProfesionales = await _profesionalRepository.ExistsWithEspecialidadAsync(id);
            if (tieneProfesionales)
            {
                throw new InvalidOperationException(
                    "No se puede eliminar la especialidad porque tiene profesionales asociados.");
            }

            return await _especialidadRepository.DeleteAsync(id);
        }
    }
}