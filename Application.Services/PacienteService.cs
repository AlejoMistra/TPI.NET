using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IEnumerable<PacienteDTO>> GetAllAsync()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return pacientes.Select(MapToDTO).ToList();
        }

        public async Task<PacienteDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            var paciente = await _pacienteRepository.GetByIdAsync(id);
            return paciente is not null ? MapToDTO(paciente) : null;
        }

        public async Task<PacienteDTO> AddAsync(PacienteDTO dto)
        {
            ValidarDatosPaciente(dto);

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailEnUso = await _pacienteRepository.EmailExistsAsync(dto.Email.Trim());
                if (emailEnUso)
                    throw new ArgumentException("Ya existe un paciente con el correo electrónico ingresado.", nameof(dto.Email));
            }

            var paciente = new Paciente(
                nombre: dto.Nombre.Trim(),
                apellido: dto.Apellido.Trim(),
                tipoDocumento: string.IsNullOrWhiteSpace(dto.TipoDocumento) ? "DNI" : dto.TipoDocumento.Trim(),
                nroDocumento: dto.NroDocumento.Trim(),
                fechaNacimiento: dto.FechaNacimiento,
                obraSocial: dto.ObraSocial?.Trim() ?? string.Empty,
                telefono: dto.Telefono?.Trim(),
                email: dto.Email?.Trim()
            );

            await _pacienteRepository.AddAsync(paciente);
            return MapToDTO(paciente);
        }

        public async Task<PacienteDTO?> UpdateAsync(PacienteDTO dto)
        {
            if (dto.Id <= 0)
                throw new ArgumentException("El ID del paciente no es válido.", nameof(dto.Id));

            ValidarDatosPaciente(dto);

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailEnUso = await _pacienteRepository.EmailExistsAsync(dto.Email.Trim(), dto.Id);
                if (emailEnUso)
                    throw new ArgumentException("Ya existe otro paciente con el correo electrónico ingresado.", nameof(dto.Email));
            }

            var paciente = new Paciente(
                nombre: dto.Nombre.Trim(),
                apellido: dto.Apellido.Trim(),
                tipoDocumento: string.IsNullOrWhiteSpace(dto.TipoDocumento) ? "DNI" : dto.TipoDocumento.Trim(),
                nroDocumento: dto.NroDocumento.Trim(),
                fechaNacimiento: dto.FechaNacimiento,
                obraSocial: dto.ObraSocial?.Trim() ?? string.Empty,
                telefono: dto.Telefono?.Trim(),
                email: dto.Email?.Trim()
            );
            paciente.SetId(dto.Id);

            var updated = await _pacienteRepository.UpdateAsync(paciente);
            return updated ? MapToDTO(paciente) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del paciente no es válido.", nameof(id));

            return await _pacienteRepository.DeleteAsync(id);
        }

        private static void ValidarDatosPaciente(PacienteDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre del paciente es obligatorio.", nameof(dto.Nombre));

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                throw new ArgumentException("El apellido del paciente es obligatorio.", nameof(dto.Apellido));

            if (string.IsNullOrWhiteSpace(dto.NroDocumento))
                throw new ArgumentException("El número de documento es obligatorio.", nameof(dto.NroDocumento));
        }

        private static PacienteDTO MapToDTO(Paciente p) => new PacienteDTO
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Apellido = p.Apellido,
            TipoDocumento = p.TipoDocumento,
            NroDocumento = p.NroDocumento,
            Telefono = p.Telefono,
            Email = p.Email,
            FechaNacimiento = p.FechaNacimiento,
            ObraSocial = p.ObraSocial
        };
    }
}
