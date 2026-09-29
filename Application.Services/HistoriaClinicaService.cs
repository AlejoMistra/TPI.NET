using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class HistoriaClinicaService : IHistoriaClinicaService
    {
        private readonly IHistoriaClinicaRepository _historiaClinicaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IProfesionalRepository _profesionalRepository;
        private readonly ITurnoRepository _turnoRepository;

        public HistoriaClinicaService(
            IHistoriaClinicaRepository historiaClinicaRepository,
            IPacienteRepository pacienteRepository,
            IProfesionalRepository profesionalRepository,
            ITurnoRepository turnoRepository)
        {
            _historiaClinicaRepository = historiaClinicaRepository;
            _pacienteRepository = pacienteRepository;
            _profesionalRepository = profesionalRepository;
            _turnoRepository = turnoRepository;
        }

        public async Task<HistoriaClinicaDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            var historia = await _historiaClinicaRepository.GetByIdAsync(id, includeRegistros: true);
            return historia is not null ? MapToDTO(historia) : null;
        }

        public async Task<HistoriaClinicaDTO?> GetByPacienteIdAsync(int pacienteId)
        {
            if (pacienteId <= 0)
                throw new ArgumentException("El ID del paciente debe ser mayor que cero.", nameof(pacienteId));

            var historia = await _historiaClinicaRepository.GetByPacienteIdAsync(pacienteId, includeRegistros: true);
            return historia is not null ? MapToDTO(historia) : null;
        }

        public async Task<HistoriaClinicaDTO> CreateAsync(HistoriaClinicaCreateDTO dto)
        {
            if (dto.PacienteId <= 0)
                throw new ArgumentException("El ID del paciente debe ser mayor que cero.", nameof(dto.PacienteId));

            var paciente = await _pacienteRepository.GetByIdAsync(dto.PacienteId);
            if (paciente is null)
                throw new ArgumentException($"No se encontró el paciente con Id {dto.PacienteId}.", nameof(dto.PacienteId));

            var yaExiste = await _historiaClinicaRepository.ExistsForPacienteAsync(dto.PacienteId);
            if (yaExiste)
                throw new InvalidOperationException($"El paciente con Id {dto.PacienteId} ya posee una historia clínica.");

            var grupoSanguineo = TypeGrupoSanguineo.NO_ESPECIFICADO;
            if (!string.IsNullOrWhiteSpace(dto.GrupoSanguineo))
            {
                if (!Enum.TryParse<TypeGrupoSanguineo>(dto.GrupoSanguineo, ignoreCase: true, out grupoSanguineo))
                    throw new ArgumentException($"El grupo sanguíneo '{dto.GrupoSanguineo}' no es válido.", nameof(dto.GrupoSanguineo));
            }

            var historia = new HistoriaClinica(dto.PacienteId, grupoSanguineo);
            await _historiaClinicaRepository.AddAsync(historia);

            return MapToDTO(historia);
        }

        public async Task<HistoriaClinicaDTO?> UpdateGrupoSanguineoAsync(int id, HistoriaClinicaUpdateGrupoDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la historia clínica debe ser mayor que cero.", nameof(id));

            if (string.IsNullOrWhiteSpace(dto.GrupoSanguineo))
                throw new ArgumentException("El grupo sanguíneo no puede ser nulo ni vacío.", nameof(dto.GrupoSanguineo));

            if (!Enum.TryParse<TypeGrupoSanguineo>(dto.GrupoSanguineo, ignoreCase: true, out var nuevoGrupo))
                throw new ArgumentException($"El grupo sanguíneo '{dto.GrupoSanguineo}' no es válido.", nameof(dto.GrupoSanguineo));

            var historia = await _historiaClinicaRepository.GetByIdAsync(id, includeRegistros: true);
            if (historia is null)
                return null;

            historia.ActualizarGrupoSanguineo(nuevoGrupo);
            await _historiaClinicaRepository.UpdateAsync(historia);

            return MapToDTO(historia);
        }

        public async Task<RegistroClinicoDTO> AddRegistroAsync(int historiaClinicaId, RegistroClinicoCreateDTO dto)
        {
            if (historiaClinicaId <= 0)
                throw new ArgumentException("El ID de la historia clínica debe ser mayor que cero.", nameof(historiaClinicaId));

            if (!Enum.TryParse<TipoRegistroClinico>(dto.Tipo, ignoreCase: true, out var tipo))
                throw new ArgumentException($"El tipo de registro clínico '{dto.Tipo}' no es válido.", nameof(dto.Tipo));

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new ArgumentException("La descripción del registro clínico es obligatoria.", nameof(dto.Descripcion));

            if (dto.Descripcion.Trim().Length > 500)
                throw new ArgumentException("La descripción no puede exceder los 500 caracteres.", nameof(dto.Descripcion));

            if (dto.ProfesionalId <= 0)
                throw new ArgumentException("El ID del profesional debe ser mayor que cero.", nameof(dto.ProfesionalId));

            var historia = await _historiaClinicaRepository.GetByIdAsync(historiaClinicaId, includeRegistros: true);
            if (historia is null)
                throw new ArgumentException($"No se encontró la historia clínica con Id {historiaClinicaId}.", nameof(historiaClinicaId));

            var profesional = await _profesionalRepository.GetByIdAsync(dto.ProfesionalId);
            if (profesional is null)
                throw new ArgumentException($"No se encontró el profesional con Id {dto.ProfesionalId}.", nameof(dto.ProfesionalId));

            RegistroClinico registro;

            if (dto.TurnoId.HasValue && dto.TurnoId.Value > 0)
            {
                var turno = await _turnoRepository.GetByIdAsync(dto.TurnoId.Value);
                if (turno is null)
                    throw new ArgumentException($"No se encontró el turno con Id {dto.TurnoId.Value}.", nameof(dto.TurnoId));

                if (turno.PacienteId != historia.PacienteId)
                    throw new ArgumentException("El turno no corresponde al paciente de esta historia clínica.", nameof(dto.TurnoId));

                if (turno.ProfesionalId != profesional.Id)
                    throw new ArgumentException("El profesional especificado no coincide con el profesional asignado al turno.", nameof(dto.ProfesionalId));

                // Valida y utiliza la lógica de dominio de Turno
                registro = turno.Registrar(tipo, dto.Descripcion.Trim(), profesional, historia);
            }
            else
            {
                registro = historia.AgregarRegistro(tipo, dto.Descripcion.Trim(), profesional);
            }

            await _historiaClinicaRepository.AddRegistroClinicoAsync(registro);

            return MapRegistroToDTO(registro);
        }

        private static HistoriaClinicaDTO MapToDTO(HistoriaClinica h) => new HistoriaClinicaDTO
        {
            Id = h.Id,
            PacienteId = h.PacienteId,
            GrupoSanguineo = h.GrupoSanguineo.ToString(),
            FechaCreacion = h.FechaCreacion,
            Registros = h.RegistrosClinicos
                .OrderByDescending(r => r.Fecha)
                .Select(MapRegistroToDTO)
                .ToList()
        };

        private static RegistroClinicoDTO MapRegistroToDTO(RegistroClinico r) => new RegistroClinicoDTO
        {
            Id = r.Id,
            Tipo = r.Tipo.ToString(),
            Descripcion = r.Descripcion,
            Fecha = r.Fecha,
            ProfesionalId = r.ProfesionalId,
            TurnoId = r.TurnoId
        };
    }
}

