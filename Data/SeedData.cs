using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TPIContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TPIContext>>();

            await SeedEspecialidadesAsync(context, logger);
            await SeedProfesionalesAsync(context, logger);
            await SeedPacientesAsync(context, logger);
            await SeedUsuariosAsync(context, logger);

        }

        // Especialidades
        private static async Task SeedEspecialidadesAsync(TPIContext context, ILogger logger)
        {
            if (await context.Especialidades.AnyAsync())
            {
                logger.LogInformation("Especialidades: ya existen registros, se omite el seed.");
                return;
            }

            var especialidades = new List<Especialidad>
            {
                new Especialidad(0, "Cardiología"),
                new Especialidad(0, "Dermatología"),
                new Especialidad(0, "Neurología"),
                new Especialidad(0, "Pediatría"),
                new Especialidad(0, "Clínica Médica"),
            };

            context.Especialidades.AddRange(especialidades);
            await context.SaveChangesAsync();
            logger.LogInformation("Especialidades: {Count} registros insertados.", especialidades.Count);
        }

        // Profesionales 
        private static async Task SeedProfesionalesAsync(TPIContext context, ILogger logger)
        {
            if (await context.Profesionales.AnyAsync())
            {
                logger.LogInformation("Profesionales: ya existen registros, se omite el seed.");
                return;
            }

            // Cargar IDs reales de especialidades por nombre
            var cardio = await context.Especialidades.FirstAsync(e => e.Nombre == "Cardiología");
            var dermato = await context.Especialidades.FirstAsync(e => e.Nombre == "Dermatología");
            var neuro = await context.Especialidades.FirstAsync(e => e.Nombre == "Neurología");
            var pediatria = await context.Especialidades.FirstAsync(e => e.Nombre == "Pediatría");

            var profesionales = new List<Profesional>
            {
                new Profesional("María",   "Fernández", "DNI", "20111222", "MP-1001", cardio.Id),
                new Profesional("Juan",    "Rodríguez", "DNI", "20222333", "MP-1002", dermato.Id),
                new Profesional("Luciana", "Torres",    "DNI", "20333444", "MP-1003", neuro.Id),
                new Profesional("Martín",  "Suárez",    "DNI", "20444555", "MP-1004", pediatria.Id),
            };

            // Asignar Email y Teléfono opcionales a través de la API pública de Persona
            // (Solo si Persona expone setters; si no, los dejamos vacíos)

            context.Profesionales.AddRange(profesionales);
            await context.SaveChangesAsync();
            logger.LogInformation("Profesionales: {Count} registros insertados.", profesionales.Count);
        }

        // Pacientes + HistoriasClinicas
        private static async Task SeedPacientesAsync(TPIContext context, ILogger logger)
        {
            if (await context.Pacientes.AnyAsync())
            {
                logger.LogInformation("Pacientes: ya existen registros, se omite el seed.");
                return;
            }

            var pacientes = new List<Paciente>
            {
                new Paciente("Ana",     "García",   "DNI", "30111222"),
                new Paciente("Carlos",  "López",    "DNI", "30222333"),
                new Paciente("Sofía",   "Martínez", "DNI", "30333444"),
                new Paciente("Diego",   "Ramírez",  "DNI", "30444555"),
            };

            context.Pacientes.AddRange(pacientes);
            await context.SaveChangesAsync();
            logger.LogInformation("Pacientes: {Count} registros insertados.", pacientes.Count);

            // Crear HistoriaClinica para cada Paciente
            if (!await context.HistoriasClinicas.AnyAsync())
            {
                var historias = pacientes.Select((p, i) => new HistoriaClinica(
                    p.Id,
                    (TypeGrupoSanguineo)(i % 8) // rotar grupos sanguíneos entre los pacientes
                )).ToList();

                context.HistoriasClinicas.AddRange(historias);
                await context.SaveChangesAsync();
                logger.LogInformation("HistoriasClinicas: {Count} registros insertados.", historias.Count);
            }
        }

        private static async Task SeedUsuariosAsync(TPIContext context, ILogger logger)
        {
            if (await context.Usuarios.AnyAsync())
            {
                logger.LogInformation("Usuarios: ya existen registros, se omite el seed.");
                return;
            }
            var usuarios = new List<Usuario>()
                {
                    new Usuario(0, "admin1", "admin@tpi.com", "admin123", DateTime.Now, Usuario.Roles.Administrativo, true)
                };
            context.Usuarios.AddRange(usuarios);
            await context.SaveChangesAsync();
            logger.LogInformation("Usuarios: {Count} registros insertados.", usuarios.Count);



        }
    }
}

