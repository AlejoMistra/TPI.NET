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
            ArgumentNullException.ThrowIfNull(profesionalDto);

            if (profesionalDto.EspecialidadId <= 0)
                throw new ArgumentException("Debe seleccionar una especialidad válida con un ID mayor a cero.", nameof(profesionalDto.EspecialidadId));

            var especialidadExists = await _especialidadRepository.GetByIdAsync(profesionalDto.EspecialidadId);
            if (especialidadExists == null)
                throw new ArgumentException($"La especialidad con ID {profesionalDto.EspecialidadId} no existe.", nameof(profesionalDto.EspecialidadId));

            if (!string.IsNullOrWhiteSpace(profesionalDto.Matricula) && await _profesionalRepository.MatriculaExistsAsync(profesionalDto.Matricula))
                throw new ArgumentException("Ya existe un profesional registrado con la matrícula ingresada.", nameof(profesionalDto.Matricula));

            if (!string.IsNullOrWhiteSpace(profesionalDto.NroDocumento) && await _profesionalRepository.DocumentoExistsAsync(profesionalDto.NroDocumento))
                throw new ArgumentException("Ya existe un profesional registrado con el número de documento ingresado.", nameof(profesionalDto.NroDocumento));

            if (!string.IsNullOrWhiteSpace(profesionalDto.Email) && await _profesionalRepository.EmailExistsAsync(profesionalDto.Email))
                throw new ArgumentException("Ya existe un profesional registrado con el correo electrónico ingresado.", nameof(profesionalDto.Email));

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
                throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));

            var profesional = await _profesionalRepository.GetByIdAsync(id);

            if (profesional == null)
                return null;

            return MapToDTO(profesional);
        }

        public async Task<ProfesionalDTO?> UpdateAsync(ProfesionalDTO profesionalDto)
        {
            ArgumentNullException.ThrowIfNull(profesionalDto);

            if (profesionalDto.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.", nameof(profesionalDto.Id));

            if (profesionalDto.EspecialidadId <= 0)
                throw new ArgumentException("Debe seleccionar una especialidad válida con un ID mayor a cero.", nameof(profesionalDto.EspecialidadId));

            var especialidadExists = await _especialidadRepository.GetByIdAsync(profesionalDto.EspecialidadId);
            if (especialidadExists == null)
                throw new ArgumentException($"La especialidad con ID {profesionalDto.EspecialidadId} no existe.", nameof(profesionalDto.EspecialidadId));

            if (!string.IsNullOrWhiteSpace(profesionalDto.Matricula) && await _profesionalRepository.MatriculaExistsAsync(profesionalDto.Matricula, profesionalDto.Id))
                throw new ArgumentException("Ya existe otro profesional registrado con la matrícula ingresada.", nameof(profesionalDto.Matricula));

            if (!string.IsNullOrWhiteSpace(profesionalDto.NroDocumento) && await _profesionalRepository.DocumentoExistsAsync(profesionalDto.NroDocumento, profesionalDto.Id))
                throw new ArgumentException("Ya existe otro profesional registrado con el número de documento ingresado.", nameof(profesionalDto.NroDocumento));

            if (!string.IsNullOrWhiteSpace(profesionalDto.Email) && await _profesionalRepository.EmailExistsAsync(profesionalDto.Email, profesionalDto.Id))
                throw new ArgumentException("Ya existe otro profesional registrado con el correo electrónico ingresado.", nameof(profesionalDto.Email));

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
            profesional.SetId(profesionalDto.Id);

            var updatedProfesional = await _profesionalRepository.UpdateAsync(profesional);

            if (updatedProfesional == null)
                return null;

            return MapToDTO(updatedProfesional);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));

            if (await _profesionalRepository.HasTurnosAsync(id))
                throw new InvalidOperationException("No se puede eliminar el profesional porque tiene turnos asociados.");

            if (await _profesionalRepository.HasRegistrosClinicosAsync(id))
                throw new InvalidOperationException("No se puede eliminar el profesional porque tiene registros clínicos asociados.");

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

            return Profesional.EstadoProfesional.Activo;
        }
    }
}
