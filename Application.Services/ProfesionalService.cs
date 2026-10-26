using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using DTOs;
using Domain.Model;

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
                throw new ArgumentException("El nombre es requerido", nameof(profesionalDto.Nombre));

            if (string.IsNullOrWhiteSpace(profesionalDto.Apellido))
                throw new ArgumentException("El apellido es requerido", nameof(profesionalDto.Apellido));

            if (string.IsNullOrWhiteSpace(profesionalDto.Matricula))
                throw new ArgumentException("La matrícula es requerida", nameof(profesionalDto.Matricula));

            if (profesionalDto.EspecialidadId <= 0)
                throw new ArgumentException("Debe seleccionar una especialidad válida", nameof(profesionalDto.EspecialidadId));

            var especialidadExists = await _especialidadRepository.GetByIdAsync(profesionalDto.EspecialidadId);
            if (especialidadExists == null)
                throw new ArgumentException($"La especialidad con ID {profesionalDto.EspecialidadId} no existe", nameof(profesionalDto.EspecialidadId));

            var estado = ParseEstado(profesionalDto.Estado);

            var profesional = new Profesional(
                profesionalDto.Nombre,
                profesionalDto.Apellido,
                profesionalDto.TipoDocumento,
                profesionalDto.NroDocumento,
                profesionalDto.Matricula,
                profesionalDto.EspecialidadId,
                profesionalDto.Telefono,
                profesionalDto.Email,
                estado
            );

            await _profesionalRepository.AddAsync(profesional);

            return MapToDTO(profesional);
        }

        public async Task<IEnumerable<ProfesionalDTO>> GetAllAsync()
        {
            var profesionales = await _profesionalRepository.GetAllAsync();
            return profesionales.Select(MapToDTO).ToList();
        }

        public async Task<ProfesionalDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0", nameof(id));

            var profesional = await _profesionalRepository.GetByIdAsync(id);

            if (profesional == null)
                return null;

            return MapToDTO(profesional);
        }

        public async Task<ProfesionalDTO?> UpdateAsync(ProfesionalDTO profesionalDto)
        {
            if (profesionalDto.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0", nameof(profesionalDto.Id));

            if (string.IsNullOrWhiteSpace(profesionalDto.Nombre))
                throw new ArgumentException("El nombre es requerido", nameof(profesionalDto.Nombre));

            if (string.IsNullOrWhiteSpace(profesionalDto.Apellido))
                throw new ArgumentException("El apellido es requerido", nameof(profesionalDto.Apellido));

            if (string.IsNullOrWhiteSpace(profesionalDto.Matricula))
                throw new ArgumentException("La matrícula es requerida", nameof(profesionalDto.Matricula));

            if (profesionalDto.EspecialidadId <= 0)
                throw new ArgumentException("Debe seleccionar una especialidad válida", nameof(profesionalDto.EspecialidadId));

            var especialidadExists = await _especialidadRepository.GetByIdAsync(profesionalDto.EspecialidadId);
            if (especialidadExists == null)
                throw new ArgumentException($"La especialidad con ID {profesionalDto.EspecialidadId} no existe", nameof(profesionalDto.EspecialidadId));

            var estado = ParseEstado(profesionalDto.Estado);

            // Construir un objeto portador con el Id correcto para que el repositorio lo encuentre
            var profesional = new Profesional(
                profesionalDto.Nombre,
                profesionalDto.Apellido,
                profesionalDto.TipoDocumento,
                profesionalDto.NroDocumento,
                profesionalDto.Matricula,
                profesionalDto.EspecialidadId,
                profesionalDto.Telefono,
                profesionalDto.Email,
                estado
            );
            profesional.SetId(profesionalDto.Id); // FIX: asignar Id antes de llamar al repositorio

            var updatedProfesional = await _profesionalRepository.UpdateAsync(profesional);

            if (updatedProfesional == null)
                return null;

            return MapToDTO(updatedProfesional);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0", nameof(id));

            return await _profesionalRepository.DeleteAsync(id);
        }

        private static ProfesionalDTO MapToDTO(Profesional p) => new ProfesionalDTO
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Apellido = p.Apellido,
            TipoDocumento = p.TipoDocumento,
            NroDocumento = p.NroDocumento,
            Matricula = p.Matricula,
            EspecialidadId = p.EspecialidadId,
            Telefono = p.Telefono,
            Email = p.Email,
            Estado = p.Estado.ToString()
        };

        private static Profesional.EstadoProfesional ParseEstado(string? estado)
        {
            if (Enum.TryParse<Profesional.EstadoProfesional>(estado, ignoreCase: true, out var result))
                return result;

            return Profesional.EstadoProfesional.Activo; // Default seguro
        }
    }
}
