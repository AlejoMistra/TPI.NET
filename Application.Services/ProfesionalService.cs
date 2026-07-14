using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DTOs;
using Model.Domain;

namespace Application.Services
{
    public class ProfesionalService : IProfesionalService
    {
        private readonly IProfesionalRepository _profesionalRepository;
        private readonly IEspecialidadRepository _especialidadRepository;

        public ProfesionalService(IProfesionalRepository profesionalRepository, IEspecialidadRepository especialidadRepository)
        {
            _profesionalRepository = profesionalRepository;
            _especialidadRepository = especialidadRepository;
        }

        public async Task<ProfesionalDTO> AddAsync(ProfesionalDTO profesionalDto)
        {
            if (string.IsNullOrWhiteSpace(profesionalDto.Nombre))
            {
                throw new ArgumentException("El nombre es requerido", nameof(profesionalDto.Nombre));
            }

            if (string.IsNullOrWhiteSpace(profesionalDto.Apellido))
            {
                throw new ArgumentException("El apellido es requerido", nameof(profesionalDto.Apellido));
            }

            if (string.IsNullOrWhiteSpace(profesionalDto.Matricula))
            {
                throw new ArgumentException("La matrícula es requerida", nameof(profesionalDto.Matricula));
            }

            if (profesionalDto.EspecialidadId <= 0)
            {
                throw new ArgumentException("Debe seleccionar una especialidad válida", nameof(profesionalDto.EspecialidadId));
            }

            var especialidadExists = await _especialidadRepository.GetByIdAsync(profesionalDto.EspecialidadId);
            if (especialidadExists == null)
            {
                throw new ArgumentException($"La especialidad con ID {profesionalDto.EspecialidadId} no existe", nameof(profesionalDto.EspecialidadId));
            }

            var profesional = new Profesional(
                0,
                profesionalDto.Nombre,
                profesionalDto.Apellido,
                profesionalDto.NroDocumento,
                profesionalDto.Matricula,
                profesionalDto.EspecialidadId
            );

            await _profesionalRepository.AddAsync(profesional);

            return new ProfesionalDTO
            {
                Id = profesional.Id,
                Nombre = profesional.Nombre,
                Apellido = profesional.Apellido,
                NroDocumento = profesional.NroDocumento,
                Matricula = profesional.Matricula,
                EspecialidadId = profesional._especialidadId
            };
        }

        public async Task<IEnumerable<ProfesionalDTO>> GetAllAsync()
        {
            var profesionales = await _profesionalRepository.GetAllAsync();
            
            return profesionales.Select(p => new ProfesionalDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                NroDocumento = p.NroDocumento,
                Matricula = p.Matricula,
                EspecialidadId = p._especialidadId
            }).ToList();
        }

        public async Task<ProfesionalDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a 0", nameof(id));
            }

            var profesional = await _profesionalRepository.GetByIdAsync(id);

            if (profesional == null)
            {
                return null;
            }

            return new ProfesionalDTO
            {
                Id = profesional.Id,
                Nombre = profesional.Nombre,
                Apellido = profesional.Apellido,
                NroDocumento = profesional.NroDocumento,
                Matricula = profesional.Matricula,
                EspecialidadId = profesional._especialidadId
            };
        }

        public async Task<ProfesionalDTO?> UpdateAsync(ProfesionalDTO profesionalDto)
        {
            if (profesionalDto.Id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a 0", nameof(profesionalDto.Id));
            }

            if (string.IsNullOrWhiteSpace(profesionalDto.Nombre))
            {
                throw new ArgumentException("El nombre es requerido", nameof(profesionalDto.Nombre));
            }

            if (string.IsNullOrWhiteSpace(profesionalDto.Apellido))
            {
                throw new ArgumentException("El apellido es requerido", nameof(profesionalDto.Apellido));
            }

            if (string.IsNullOrWhiteSpace(profesionalDto.Matricula))
            {
                throw new ArgumentException("La matrícula es requerida", nameof(profesionalDto.Matricula));
            }

            if (profesionalDto.EspecialidadId <= 0)
            {
                throw new ArgumentException("Debe seleccionar una especialidad válida", nameof(profesionalDto.EspecialidadId));
            }

            var especialidadExists = await _especialidadRepository.GetByIdAsync(profesionalDto.EspecialidadId);
            if (especialidadExists == null)
            {
                throw new ArgumentException($"La especialidad con ID {profesionalDto.EspecialidadId} no existe", nameof(profesionalDto.EspecialidadId));
            }

            var profesional = new Profesional(
                profesionalDto.Id,
                profesionalDto.Nombre,
                profesionalDto.Apellido,
                profesionalDto.NroDocumento,
                profesionalDto.Matricula,
                profesionalDto.EspecialidadId
            );

            var updatedProfesional = await _profesionalRepository.UpdateAsync(profesional);

            if (updatedProfesional == null)
            {
                return null;
            }

            return new ProfesionalDTO
            {
                Id = updatedProfesional.Id,
                Nombre = updatedProfesional.Nombre,
                Apellido = updatedProfesional.Apellido,
                NroDocumento = updatedProfesional.NroDocumento,
                Matricula = updatedProfesional.Matricula,
                EspecialidadId = updatedProfesional._especialidadId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a 0", nameof(id));
            }

            return await _profesionalRepository.DeleteAsync(id);
        }
    }
}
