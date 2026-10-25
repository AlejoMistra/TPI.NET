using System.Net.Mail;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class PacienteService : IPacienteService
    {
        private static readonly DateTime MinFechaNacimiento = new DateTime(1900, 1, 1);
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

        public async Task<IEnumerable<PacienteDTO>> GetByCriteriaAsync(PacienteCriteriaDTO criteriaDTO)
        {
            // Mapear DTO a Domain Model
            var criteria = new PacienteCriteria(criteriaDTO.Texto, criteriaDTO.ObraSocial, criteriaDTO.EspecialidadId, criteriaDTO.ProfesionalId);

            // Llamar al repositorio (ADO.NET)
            var pacientes = await _pacienteRepository.GetByCriteriaAsync(criteria);

            // Mapear Domain Model a DTO
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
            ValidarDatosPaciente(dto, out var tipoDocNormalizado);

            var nroDoc = dto.NroDocumento.Trim();
            if (await _pacienteRepository.DocumentoExistsAsync(nroDoc))
                throw new ArgumentException("Ya existe un paciente registrado con el número de documento ingresado.", nameof(dto.NroDocumento));

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailEnUso = await _pacienteRepository.EmailExistsAsync(dto.Email.Trim());
                if (emailEnUso)
                    throw new ArgumentException("Ya existe un paciente con el correo electrónico ingresado.", nameof(dto.Email));
            }

            var paciente = new Paciente(
                nombre: dto.Nombre.Trim(),
                apellido: dto.Apellido.Trim(),
                tipoDocumento: tipoDocNormalizado,
                nroDocumento: nroDoc,
                fechaNacimiento: dto.FechaNacimiento.Date,
                obraSocial: dto.ObraSocial?.Trim() ?? string.Empty,
                telefono: string.IsNullOrWhiteSpace(dto.Telefono) ? null : dto.Telefono.Trim(),
                email: string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim()
            );

            await _pacienteRepository.AddAsync(paciente);
            return MapToDTO(paciente);
        }

        public async Task<PacienteDTO?> UpdateAsync(PacienteDTO dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Los datos del paciente son requeridos.");

            if (dto.Id <= 0)
                throw new ArgumentException("El ID del paciente no es válido.", nameof(dto.Id));

            ValidarDatosPaciente(dto, out var tipoDocNormalizado);

            var nroDoc = dto.NroDocumento.Trim();
            if (await _pacienteRepository.DocumentoExistsAsync(nroDoc, dto.Id))
                throw new ArgumentException("Ya existe otro paciente registrado con el número de documento ingresado.", nameof(dto.NroDocumento));

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailEnUso = await _pacienteRepository.EmailExistsAsync(dto.Email.Trim(), dto.Id);
                if (emailEnUso)
                    throw new ArgumentException("Ya existe otro paciente con el correo electrónico ingresado.", nameof(dto.Email));
            }

            var paciente = new Paciente(
                nombre: dto.Nombre.Trim(),
                apellido: dto.Apellido.Trim(),
                tipoDocumento: tipoDocNormalizado,
                nroDocumento: nroDoc,
                fechaNacimiento: dto.FechaNacimiento.Date,
                obraSocial: dto.ObraSocial?.Trim() ?? string.Empty,
                telefono: string.IsNullOrWhiteSpace(dto.Telefono) ? null : dto.Telefono.Trim(),
                email: string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim()
            );
            paciente.SetId(dto.Id);

            var updated = await _pacienteRepository.UpdateAsync(paciente);
            return updated ? MapToDTO(paciente) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del paciente no es válido.", nameof(id));

            if (await _pacienteRepository.HasTurnosAsync(id))
                throw new InvalidOperationException("No se puede eliminar el paciente porque tiene turnos asociados.");

            return await _pacienteRepository.DeleteAsync(id);
        }

        private static void ValidarDatosPaciente(PacienteDTO dto, out string tipoDocNormalizado)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "Los datos del paciente son requeridos.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre del paciente es obligatorio.", nameof(dto.Nombre));

            if (dto.Nombre.Trim().Length > 100)
                throw new ArgumentException("El nombre del paciente no puede superar los 100 caracteres.", nameof(dto.Nombre));

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                throw new ArgumentException("El apellido del paciente es obligatorio.", nameof(dto.Apellido));

            if (dto.Apellido.Trim().Length > 100)
                throw new ArgumentException("El apellido del paciente no puede superar los 100 caracteres.", nameof(dto.Apellido));

            var rawTipoDoc = string.IsNullOrWhiteSpace(dto.TipoDocumento) ? "DNI" : dto.TipoDocumento.Trim();
            if (rawTipoDoc.Length > 20 || !Enum.TryParse<Persona.TipoDocumentoEnum>(rawTipoDoc, ignoreCase: true, out var tipoDocEnum) || !Enum.IsDefined(typeof(Persona.TipoDocumentoEnum), tipoDocEnum))
                throw new ArgumentException("El tipo de documento no es válido. Valores permitidos: DNI, Pasaporte, Otro.", nameof(dto.TipoDocumento));

            tipoDocNormalizado = tipoDocEnum.ToString();

            if (string.IsNullOrWhiteSpace(dto.NroDocumento))
                throw new ArgumentException("El número de documento es obligatorio.", nameof(dto.NroDocumento));

            var nroDoc = dto.NroDocumento.Trim();
            if (nroDoc.Length > 20)
                throw new ArgumentException("El número de documento no puede superar los 20 caracteres.", nameof(dto.NroDocumento));

            if (tipoDocEnum == Persona.TipoDocumentoEnum.DNI && (nroDoc.Length < 7 || nroDoc.Length > 8 || !nroDoc.All(char.IsDigit)))
                throw new ArgumentException("Para tipo de documento DNI, el número debe contener entre 7 y 8 dígitos numéricos.", nameof(dto.NroDocumento));

            var fechaNac = dto.FechaNacimiento.Date;
            if (fechaNac < MinFechaNacimiento || fechaNac > DateTime.Today)
                throw new ArgumentException($"La fecha de nacimiento debe ser una fecha válida entre {MinFechaNacimiento:dd/MM/yyyy} y hoy.", nameof(dto.FechaNacimiento));

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var email = dto.Email.Trim();
                if (email.Length > 100)
                    throw new ArgumentException("El correo electrónico no puede superar los 100 caracteres.", nameof(dto.Email));

                if (!MailAddress.TryCreate(email, out _))
                    throw new ArgumentException("El formato del correo electrónico no es válido.", nameof(dto.Email));
            }

            if (!string.IsNullOrWhiteSpace(dto.Telefono) && dto.Telefono.Trim().Length > 20)
                throw new ArgumentException("El teléfono no puede superar los 20 caracteres.", nameof(dto.Telefono));

            if (!string.IsNullOrWhiteSpace(dto.ObraSocial) && dto.ObraSocial.Trim().Length > 100)
                throw new ArgumentException("La obra social no puede superar los 100 caracteres.", nameof(dto.ObraSocial));
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
