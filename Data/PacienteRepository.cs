using Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Data
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly TPIContext _context;

        public PacienteRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Paciente paciente)
        {
            // La conexión usa reintentos automáticos (EnableRetryOnFailure): una transacción
            // manual tiene que ejecutarse dentro de la estrategia de ejecución del contexto
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await _context.Pacientes.AddAsync(paciente);
                    await _context.SaveChangesAsync();

                    // Asegurar que cada paciente nuevo tenga su historia clínica inicializada en la misma transacción
                    var historiaClinica = new HistoriaClinica(paciente.Id, TypeGrupoSanguineo.NO_ESPECIFICADO);
                    await _context.HistoriasClinicas.AddAsync(historiaClinica);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<bool> UpdateAsync(Paciente paciente)
        {
            var existing = await _context.Pacientes.FindAsync(paciente.Id);
            if (existing == null)
                return false;

            existing.SetNombre(paciente.Nombre);
            existing.SetApellido(paciente.Apellido);
            existing.SetTipoDocumento(paciente.TipoDocumento);
            existing.SetNroDocumento(paciente.NroDocumento);
            existing.SetTelefono(paciente.Telefono);
            existing.SetEmail(paciente.Email);
            existing.SetFechaNacimiento(paciente.FechaNacimiento);
            existing.SetObraSocial(paciente.ObraSocial);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return false;

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _context.Pacientes.AnyAsync(p => p.Email == email && (excludeId == null || p.Id != excludeId));
        }

        public async Task<bool> DocumentoExistsAsync(string nroDocumento, int? excludeId = null)
        {
            return await _context.Pacientes.AnyAsync(p => p.NroDocumento == nroDocumento && (excludeId == null || p.Id != excludeId));
        }

        public async Task<bool> HasTurnosAsync(int pacienteId)
        {
            return await _context.Turnos.AnyAsync(t => t.PacienteId == pacienteId);
        }

        public async Task<IEnumerable<Paciente>> GetByCriteriaAsync(PacienteCriteria criteria)
        {
            const string sql = @"
                SELECT p.Id, pe.Nombre, pe.Apellido, pe.TipoDocumento, pe.NroDocumento,
                       pe.Telefono, pe.Email, p.FechaNacimiento, p.ObraSocial
                FROM Pacientes p
                INNER JOIN Personas pe ON pe.Id = p.Id
                WHERE (pe.Nombre LIKE @SearchTerm
                    OR pe.Apellido LIKE @SearchTerm
                    OR pe.NroDocumento LIKE @SearchTerm
                    OR CONCAT(pe.Apellido, ', ', pe.Nombre) LIKE @SearchTerm
                    OR CONCAT(pe.Nombre, ' ', pe.Apellido) LIKE @SearchTerm)
                  AND (@ObraSocial IS NULL
                    OR (@ObraSocial = 'Particular' AND (p.ObraSocial IS NULL OR p.ObraSocial = '' OR p.ObraSocial = 'Particular'))
                    OR p.ObraSocial = @ObraSocial)
                  AND (@ProfesionalId IS NULL
                    OR EXISTS (SELECT 1 FROM Turnos t WHERE t.PacienteId = p.Id AND t.ProfesionalId = @ProfesionalId))
                  AND (@EspecialidadId IS NULL
                    OR EXISTS (SELECT 1 FROM Turnos t
                               INNER JOIN Profesionales pr ON pr.Id = t.ProfesionalId
                               WHERE t.PacienteId = p.Id AND pr.EspecialidadId = @EspecialidadId))
                ORDER BY pe.Apellido, pe.Nombre";

            var pacientes = new List<Paciente>();
            string connectionString = _context.Database.GetConnectionString()!;

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@SearchTerm", SqlDbType.NVarChar).Value = $"%{criteria.Texto}%";
            command.Parameters.Add("@ObraSocial", SqlDbType.NVarChar).Value = (object?)criteria.ObraSocial ?? DBNull.Value;
            command.Parameters.Add("@ProfesionalId", SqlDbType.Int).Value = (object?)criteria.ProfesionalId ?? DBNull.Value;
            command.Parameters.Add("@EspecialidadId", SqlDbType.Int).Value = (object?)criteria.EspecialidadId ?? DBNull.Value;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var paciente = new Paciente(
                    nombre: reader.GetString(1),
                    apellido: reader.GetString(2),
                    tipoDocumento: reader.GetString(3),
                    nroDocumento: reader.GetString(4),
                    fechaNacimiento: reader.GetDateTime(7),
                    obraSocial: reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                    telefono: reader.IsDBNull(5) ? null : reader.GetString(5),
                    email: reader.IsDBNull(6) ? null : reader.GetString(6));
                paciente.SetId(reader.GetInt32(0));

                pacientes.Add(paciente);
            }

            return pacientes;
        }
    }
}