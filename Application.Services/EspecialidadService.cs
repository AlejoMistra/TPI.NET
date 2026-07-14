using Data;
using DTOs;
using Model.Domain;

namespace Application.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IEspecialidadRepository _especialidadRepository;

        public EspecialidadService(IEspecialidadRepository especialidadRepository)
        {
            _especialidadRepository = especialidadRepository;
        }
        
        public async Task<EspecialidadDTO> AddAsync(EspecialidadDTO especialidadDto)
        {
            if (string.IsNullOrWhiteSpace(especialidadDto.Nombre))
            {
                throw new ArgumentException("El nombre de la especialidad no puede estar vacío.");
            }
            
            var especialidad = new Especialidad(0, especialidadDto.Nombre);

            await _especialidadRepository.AddAsync(especialidad);
            
            return new EspecialidadDTO
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre
            };
        }

        public async Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
        {
            var especialidades = await _especialidadRepository.GetAllAsync();
            return especialidades.Select(e => new EspecialidadDTO
            {
                Id = e.Id,
                Nombre = e.Nombre
            }).ToList();
        }

        public async Task<EspecialidadDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.");
            }

            var especialidad = await _especialidadRepository.GetByIdAsync(id);

            if (especialidad == null)
            {
                return null;
            }
            
            return new EspecialidadDTO
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre
            };
        }

        public async Task<EspecialidadDTO?> UpdateAsync(EspecialidadDTO especialidadDto)
        {
            if (especialidadDto.Id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.");
            }
            
            if (string.IsNullOrWhiteSpace(especialidadDto.Nombre))
            {
                throw new ArgumentException("El nombre de la especialidad no puede estar vacío.", nameof(especialidadDto));
            }

            var especialidad = new Especialidad(especialidadDto.Id, especialidadDto.Nombre);
            var updatedEspecialidad = await _especialidadRepository.UpdateAsync(especialidad);
            
            if (updatedEspecialidad == null)
            {
                return null;
            }
            
            return new EspecialidadDTO
            {
                Id = updatedEspecialidad.Id,
                Nombre = updatedEspecialidad.Nombre
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            }
            return await _especialidadRepository.DeleteAsync(id);
        }
    }
}
