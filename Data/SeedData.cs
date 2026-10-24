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
            await SeedTurnosAsync(context, logger);
            await SeedRegistrosClinicosAsync(context, logger);
            await SeedPermisosYGruposAsync(context, logger);
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
                new Profesional(
                    "María", "Fernández", "DNI", "20111222", "MP-1001", cardio.Id,
                    telefono: "011-4523-1100",
                    email: "m.fernandez@clinica.med.ar",
                    estado: Profesional.EstadoProfesional.Activo),

                new Profesional(
                    "Juan", "Rodríguez", "DNI", "20222333", "MP-1002", dermato.Id,
                    telefono: "011-4523-1101",
                    email: "j.rodriguez@clinica.med.ar",
                    estado: Profesional.EstadoProfesional.Activo),

                new Profesional(
                    "Luciana", "Torres", "DNI", "20333444", "MP-1003", neuro.Id,
                    telefono: "011-4523-1102",
                    email: "l.torres@clinica.med.ar",
                    estado: Profesional.EstadoProfesional.Inactivo), // de baja temporaria

                new Profesional(
                    "Martín", "Suárez", "DNI", "20444555", "MP-1004", pediatria.Id,
                    telefono: "011-4523-1103",
                    email: "m.suarez@clinica.med.ar",
                    estado: Profesional.EstadoProfesional.Activo),
            };

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
                new Paciente("Ana",    "García",   "DNI", "30111222", new DateTime(1985, 4, 12),  "OSDE",        "011-5555-1001", "ana.garcia@email.com"),
                new Paciente("Carlos", "López",    "DNI", "30222333", new DateTime(1978, 9, 25),  "Swiss Medical", "011-5555-1002", "carlos.lopez@email.com"),
                new Paciente("Sofía",  "Martínez", "DNI", "30333444", new DateTime(1992, 11, 3),  "Galeno",      "011-5555-1003", "sofia.martinez@email.com"),
                new Paciente("Diego",  "Ramírez",  "DNI", "30444555", new DateTime(1990, 6, 18),  "IAPOS",       "011-5555-1004", "diego.ramirez@email.com"),
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

        // Turnos
        private static async Task SeedTurnosAsync(TPIContext context, ILogger logger)
        {
            if (await context.Turnos.AnyAsync())
            {
                logger.LogInformation("Turnos: ya existen registros, se omite el seed.");
                return;
            }

            var profesionales = await context.Profesionales.ToListAsync();
            var pacientes = await context.Pacientes.ToListAsync();

            if (!profesionales.Any() || !pacientes.Any())
            {
                logger.LogWarning("Turnos: faltan profesionales o pacientes para crear turnos.");
                return;
            }

            var draFernandez = profesionales.FirstOrDefault(p => p.Apellido == "Fernández") ?? profesionales.First();
            var drRodriguez = profesionales.FirstOrDefault(p => p.Apellido == "Rodríguez") ?? profesionales.First();
            var drSuarez = profesionales.FirstOrDefault(p => p.Apellido == "Suárez") ?? profesionales.First();

            var ana = pacientes.FirstOrDefault(p => p.Nombre == "Ana") ?? pacientes.First();
            var carlos = pacientes.FirstOrDefault(p => p.Nombre == "Carlos") ?? pacientes.First();
            var sofia = pacientes.FirstOrDefault(p => p.Nombre == "Sofía") ?? pacientes.First();
            var diego = pacientes.FirstOrDefault(p => p.Nombre == "Diego") ?? pacientes.First();

            var today = DateTime.Today;

            var turnos = new List<Turno>
            {
                // Turnos Atendidos (semana pasada)
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(-6).AddHours(9),
                    fechaHoraFin: today.AddDays(-6).AddHours(9).AddMinutes(30),
                    motivo: "Control de presión arterial",
                    estadoTurno: Turno.EstadosTurno.Atendido,
                    observaciones: "Paciente asistió en término. Se ajusta medicación.",
                    facturaId: null,
                    profesionalId: draFernandez.Id,
                    pacienteId: ana.Id
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(-4).AddHours(10),
                    fechaHoraFin: today.AddDays(-4).AddHours(10).AddMinutes(30),
                    motivo: "Consulta por erupción en piel",
                    estadoTurno: Turno.EstadosTurno.Atendido,
                    observaciones: "Se constata dermatitis de contacto en antebrazo.",
                    facturaId: null,
                    profesionalId: drRodriguez.Id,
                    pacienteId: carlos.Id
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(-3).AddHours(11),
                    fechaHoraFin: today.AddDays(-3).AddHours(11).AddMinutes(30),
                    motivo: "Chequeo médico anual preventivo",
                    estadoTurno: Turno.EstadosTurno.Atendido,
                    observaciones: "Examen físico completo sin particularidades.",
                    facturaId: null,
                    profesionalId: draFernandez.Id,
                    pacienteId: sofia.Id
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(-2).AddHours(16),
                    fechaHoraFin: today.AddDays(-2).AddHours(16).AddMinutes(30),
                    motivo: "Consulta de control y antecedentes",
                    estadoTurno: Turno.EstadosTurno.Atendido,
                    observaciones: "Revisión de antecedentes respiratorios de la infancia.",
                    facturaId: null,
                    profesionalId: drSuarez.Id,
                    pacienteId: carlos.Id
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(-1).AddHours(14),
                    fechaHoraFin: today.AddDays(-1).AddHours(14).AddMinutes(30),
                    motivo: "Evaluación cardiológica preventiva",
                    estadoTurno: Turno.EstadosTurno.Atendido,
                    observaciones: "Se solicitan estudios complementarios por antecedentes familiares.",
                    facturaId: null,
                    profesionalId: draFernandez.Id,
                    pacienteId: diego.Id
                ),

                // Turnos del día y próximos días con diversos estados
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddHours(8),
                    fechaHoraFin: today.AddHours(8).AddMinutes(30),
                    motivo: "Control de rutina",
                    estadoTurno: Turno.EstadosTurno.Ausente,
                    observaciones: "El paciente nunca se presentó al turno.",
                    facturaId: null,
                    profesionalId: drRodriguez.Id,
                    pacienteId: carlos.Id,
                    fechaHoraLlegada: null
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddHours(8).AddMinutes(30),
                    fechaHoraFin: today.AddHours(9),
                    motivo: "Control de laboratorio",
                    estadoTurno: Turno.EstadosTurno.Ausente,
                    observaciones: "El paciente se anunció en sala de espera pero se retiró por demora antes de la atención.",
                    facturaId: null,
                    profesionalId: drRodriguez.Id,
                    pacienteId: sofia.Id,
                    fechaHoraLlegada: today.AddHours(8).AddMinutes(25)
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddHours(10),
                    fechaHoraFin: today.AddHours(10).AddMinutes(30),
                    motivo: "Consulta por dolor lumbar",
                    estadoTurno: Turno.EstadosTurno.Presente,
                    observaciones: "Paciente anunciado en sala de espera.",
                    facturaId: null,
                    profesionalId: drSuarez.Id,
                    pacienteId: diego.Id,
                    fechaHoraLlegada: today.AddHours(9).AddMinutes(50)
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(1).AddHours(9),
                    fechaHoraFin: today.AddDays(1).AddHours(9).AddMinutes(30),
                    motivo: "Control de respuesta a Enalapril",
                    estadoTurno: Turno.EstadosTurno.Asignado,
                    observaciones: "Control a los 7 días de inicio del tratamiento.",
                    facturaId: null,
                    profesionalId: draFernandez.Id,
                    pacienteId: ana.Id
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(1).AddHours(14),
                    fechaHoraFin: today.AddDays(1).AddHours(14).AddMinutes(30),
                    motivo: string.Empty,
                    estadoTurno: Turno.EstadosTurno.Libre,
                    observaciones: "Turno disponible para asignación.",
                    facturaId: null,
                    profesionalId: draFernandez.Id,
                    pacienteId: null
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(2).AddHours(10),
                    fechaHoraFin: today.AddDays(2).AddHours(10).AddMinutes(30),
                    motivo: "Seguimiento de dermatitis",
                    estadoTurno: Turno.EstadosTurno.Asignado,
                    observaciones: "Evaluar evolución tras tratamiento tópico.",
                    facturaId: null,
                    profesionalId: drRodriguez.Id,
                    pacienteId: carlos.Id
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(3).AddHours(15),
                    fechaHoraFin: today.AddDays(3).AddHours(15).AddMinutes(30),
                    motivo: string.Empty,
                    estadoTurno: Turno.EstadosTurno.Libre,
                    observaciones: "Turno disponible para asignación.",
                    facturaId: null,
                    profesionalId: draFernandez.Id,
                    pacienteId: null
                ),
                new Turno(
                    id: 0,
                    fechaHoraInicio: today.AddDays(4).AddHours(11),
                    fechaHoraFin: today.AddDays(4).AddHours(11).AddMinutes(30),
                    motivo: "Consulta médica general",
                    estadoTurno: Turno.EstadosTurno.Asignado,
                    observaciones: "Turno programado de rutina.",
                    facturaId: null,
                    profesionalId: drSuarez.Id,
                    pacienteId: sofia.Id
                )
            };

            context.Turnos.AddRange(turnos);
            await context.SaveChangesAsync();
            logger.LogInformation("Turnos: {Count} registros insertados.", turnos.Count);
        }

        // Registros Clínicos
        private static async Task SeedRegistrosClinicosAsync(TPIContext context, ILogger logger)
        {
            if (await context.RegistrosClinicos.AnyAsync())
            {
                logger.LogInformation("RegistrosClinicos: ya existen registros, se omite el seed.");
                return;
            }

            var historias = await context.HistoriasClinicas.Include(h => h.RegistrosClinicos).ToListAsync();
            var profesionales = await context.Profesionales.ToListAsync();
            var turnosAtendidos = await context.Turnos.Where(t => t.EstadoTurno == Turno.EstadosTurno.Atendido).ToListAsync();

            if (!historias.Any() || !profesionales.Any())
            {
                logger.LogWarning("RegistrosClinicos: no se encontraron historias clínicas o profesionales para asociar registros.");
                return;
            }

            var draFernandez = profesionales.FirstOrDefault(p => p.Apellido == "Fernández") ?? profesionales.First();
            var drRodriguez = profesionales.FirstOrDefault(p => p.Apellido == "Rodríguez") ?? profesionales.First();
            var drSuarez = profesionales.FirstOrDefault(p => p.Apellido == "Suárez") ?? profesionales.First();

            // Paciente 1 - Ana García
            var hc1 = historias.ElementAtOrDefault(0);
            if (hc1 != null)
            {
                // Alergia registrada como antecedente general
                hc1.AgregarRegistro(
                    TipoRegistroClinico.Alergia,
                    "Alergia a la penicilina y derivados betalactámicos. Reacción: eritema cutáneo y prurito.",
                    draFernandez
                );

                // Diagnóstico y Tratamiento vinculados al turno atendido con Dra. Fernández
                var turnoAna = turnosAtendidos.FirstOrDefault(t => t.PacienteId == hc1.PacienteId && t.ProfesionalId == draFernandez.Id);
                if (turnoAna != null)
                {
                    turnoAna.Registrar(
                        TipoRegistroClinico.Diagnostico,
                        "Hipertensión arterial primaria grado 1 detectada en examen de rutina.",
                        draFernandez,
                        hc1
                    );
                    turnoAna.Registrar(
                        TipoRegistroClinico.Tratamiento,
                        "Enalapril 10mg diario por la mañana. Se indica dieta hiposódica y control de presión en 15 días.",
                        draFernandez,
                        hc1
                    );
                }
                else
                {
                    hc1.AgregarRegistro(TipoRegistroClinico.Diagnostico, "Hipertensión arterial primaria grado 1 detectada en examen de rutina.", draFernandez);
                    hc1.AgregarRegistro(TipoRegistroClinico.Tratamiento, "Enalapril 10mg diario por la mañana. Se indica dieta hiposódica y control de presión en 15 días.", draFernandez);
                }
            }

            // Paciente 2 - Carlos López
            var hc2 = historias.ElementAtOrDefault(1);
            if (hc2 != null)
            {
                // Consulta con Dr. Suárez (antecedentes)
                var turnoCarlosSuarez = turnosAtendidos.FirstOrDefault(t => t.PacienteId == hc2.PacienteId && t.ProfesionalId == drSuarez.Id);
                if (turnoCarlosSuarez != null)
                {
                    turnoCarlosSuarez.Registrar(
                        TipoRegistroClinico.Antecedente,
                        "Antecedente de asma bronquial durante la infancia, sin episodios en los últimos 5 años.",
                        drSuarez,
                        hc2
                    );
                }
                else
                {
                    hc2.AgregarRegistro(TipoRegistroClinico.Antecedente, "Antecedente de asma bronquial durante la infancia, sin episodios en los últimos 5 años.", drSuarez);
                }

                // Consulta con Dr. Rodríguez (dermatología)
                var turnoCarlosRodriguez = turnosAtendidos.FirstOrDefault(t => t.PacienteId == hc2.PacienteId && t.ProfesionalId == drRodriguez.Id);
                if (turnoCarlosRodriguez != null)
                {
                    turnoCarlosRodriguez.Registrar(
                        TipoRegistroClinico.Diagnostico,
                        "Dermatitis por contacto en antebrazo derecho.",
                        drRodriguez,
                        hc2
                    );
                    turnoCarlosRodriguez.Registrar(
                        TipoRegistroClinico.Tratamiento,
                        "Hidrocortisona en crema al 1% cada 12 hs durante 7 días y humectación regular.",
                        drRodriguez,
                        hc2
                    );
                }
                else
                {
                    hc2.AgregarRegistro(TipoRegistroClinico.Diagnostico, "Dermatitis por contacto en antebrazo derecho.", drRodriguez);
                    hc2.AgregarRegistro(TipoRegistroClinico.Tratamiento, "Hidrocortisona en crema al 1% cada 12 hs durante 7 días y humectación regular.", drRodriguez);
                }
            }

            // Paciente 3 - Sofía Martínez
            var hc3 = historias.ElementAtOrDefault(2);
            if (hc3 != null)
            {
                var turnoSofia = turnosAtendidos.FirstOrDefault(t => t.PacienteId == hc3.PacienteId && t.ProfesionalId == draFernandez.Id);
                if (turnoSofia != null)
                {
                    turnoSofia.Registrar(
                        TipoRegistroClinico.NotaClinica,
                        "Chequeo anual preventivo. Paciente sin síntomas de relevancia. Tensión arterial 110/70 mmHg.",
                        draFernandez,
                        hc3
                    );
                    turnoSofia.Registrar(
                        TipoRegistroClinico.Evolucion,
                        "Evolución favorable, análisis de laboratorio generales dentro de los rangos normales.",
                        draFernandez,
                        hc3
                    );
                }
                else
                {
                    hc3.AgregarRegistro(TipoRegistroClinico.NotaClinica, "Chequeo anual preventivo. Paciente sin síntomas de relevancia. Tensión arterial 110/70 mmHg.", draFernandez);
                    hc3.AgregarRegistro(TipoRegistroClinico.Evolucion, "Evolución favorable, análisis de laboratorio generales dentro de los rangos normales.", draFernandez);
                }
            }

            // Paciente 4 - Diego Ramírez
            var hc4 = historias.ElementAtOrDefault(3);
            if (hc4 != null)
            {
                hc4.AgregarRegistro(
                    TipoRegistroClinico.Antecedente,
                    "Antecedentes heredofamiliares de dislipidemia y cardiopatía isquémica paterna.",
                    draFernandez
                );

                var turnoDiego = turnosAtendidos.FirstOrDefault(t => t.PacienteId == hc4.PacienteId && t.ProfesionalId == draFernandez.Id);
                if (turnoDiego != null)
                {
                    turnoDiego.Registrar(
                        TipoRegistroClinico.NotaClinica,
                        "Se solicitan estudios complementarios: lipidograma completo y electrocardiograma de control.",
                        draFernandez,
                        hc4
                    );
                }
                else
                {
                    hc4.AgregarRegistro(TipoRegistroClinico.NotaClinica, "Se solicitan estudios complementarios: lipidograma completo y electrocardiograma de control.", draFernandez);
                }
            }

            await context.SaveChangesAsync();
            var totalRegistros = await context.RegistrosClinicos.CountAsync();
            logger.LogInformation("RegistrosClinicos: {Count} registros insertados.", totalRegistros);
        }

        private static async Task SeedPermisosYGruposAsync(TPIContext context, ILogger logger)
        {
            var permisosDefinidos = new List<(string Categoria, string Nombre, string Descripcion)>
            {
                // Especialidades
                ("especialidades", "leer", "Leer especialidades"),
                ("especialidades", "agregar", "Agregar especialidades"),
                ("especialidades", "actualizar", "Actualizar especialidades"),
                ("especialidades", "eliminar", "Eliminar especialidades"),

                // Profesionales
                ("profesionales", "leer", "Leer profesionales"),
                ("profesionales", "agregar", "Agregar profesionales"),
                ("profesionales", "actualizar", "Actualizar profesionales"),
                ("profesionales", "eliminar", "Eliminar profesionales"),

                // Pacientes
                ("pacientes", "leer", "Leer pacientes"),
                ("pacientes", "agregar", "Agregar pacientes"),
                ("pacientes", "actualizar", "Actualizar pacientes"),
                ("pacientes", "eliminar", "Eliminar pacientes"),

                // Turnos
                ("turnos", "leer", "Leer turnos"),
                ("turnos", "agregar", "Agregar turnos"),
                ("turnos", "actualizar", "Actualizar turnos"),
                ("turnos", "eliminar", "Eliminar turnos"),
                ("turnos", "atender", "Atender turnos"),

                // Historias Clínicas
                ("historiasclinicas", "leer", "Leer historias clínicas"),
                ("historiasclinicas", "agregar", "Agregar historias clínicas"),
                ("historiasclinicas", "actualizar", "Actualizar historias clínicas"),

                // Facturas
                ("facturas", "leer", "Leer facturas"),
                ("facturas", "agregar", "Agregar facturas"),
                ("facturas", "actualizar", "Actualizar facturas"),

                // Usuarios
                ("usuarios", "leer", "Leer usuarios"),
                ("usuarios", "agregar", "Agregar usuarios"),
                ("usuarios", "actualizar", "Actualizar usuarios"),
                ("usuarios", "eliminar", "Eliminar usuarios"),

                // Datos Maestros (UI)
                ("datosmaestros", "leer", "Acceso a datos maestros")
            };

            foreach (var (categoria, nombre, descripcion) in permisosDefinidos)
            {
                if (!await context.Permisos.AnyAsync(p => p.Categoria == categoria && p.Nombre == nombre))
                {
                    context.Permisos.Add(new Permiso(0, nombre, descripcion, categoria, true));
                }
            }
            await context.SaveChangesAsync();

            // Seed Grupos
            var grupoAdmin = await context.GruposPermisos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Nombre == "Administrador");
            if (grupoAdmin == null)
            {
                grupoAdmin = new GrupoPermiso(0, "Administrador", "Acceso completo a todas las funcionalidades", DateTime.Now, true);
                context.GruposPermisos.Add(grupoAdmin);
                await context.SaveChangesAsync();
            }

            var grupoProfesional = await context.GruposPermisos.Include(g => g.Permisos).FirstOrDefaultAsync(g => g.Nombre == "Profesional");
            if (grupoProfesional == null)
            {
                grupoProfesional = new GrupoPermiso(0, "Profesional", "Acceso a agenda de turnos, atención e historias clínicas", DateTime.Now, true);
                context.GruposPermisos.Add(grupoProfesional);
                await context.SaveChangesAsync();
            }

            // Asignar permisos a Administrador
            var todosLosPermisos = await context.Permisos.Where(p => p.Activo).ToListAsync();
            foreach (var permiso in todosLosPermisos)
            {
                if (!grupoAdmin.Permisos.Any(p => p.Id == permiso.Id))
                {
                    grupoAdmin.AgregarPermiso(permiso);
                }
            }

            // Asignar permisos a Profesional
            var categoriasYAccionesProfesional = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "pacientes.leer",
                "turnos.leer",
                "turnos.atender",
                "turnos.actualizar",
                "historiasclinicas.leer",
                "historiasclinicas.agregar",
                "historiasclinicas.actualizar",
                "especialidades.leer",
                "profesionales.leer"
            };

            foreach (var permiso in todosLosPermisos)
            {
                if (categoriasYAccionesProfesional.Contains(permiso.ToString()))
                {
                    if (!grupoProfesional.Permisos.Any(p => p.Id == permiso.Id))
                    {
                        grupoProfesional.AgregarPermiso(permiso);
                    }
                }
            }

            await context.SaveChangesAsync();
            logger.LogInformation("Permisos y Grupos: seed completado.");
        }

        private static async Task SeedUsuariosAsync(TPIContext context, ILogger logger)
        {
            const string adminUsername = "admin";
            const string adminEmail = "admin@tpi.com";

            var grupoAdmin = await context.GruposPermisos.FirstOrDefaultAsync(g => g.Nombre == "Administrador");
            var grupoProfesional = await context.GruposPermisos.FirstOrDefaultAsync(g => g.Nombre == "Profesional");

            // Seed o actualización de Administrador
            var adminExistente = await context.Usuarios.Include(u => u.Grupo).FirstOrDefaultAsync(u => u.Username == adminUsername || u.Email == adminEmail);
            if (adminExistente == null)
            {
                var admin = new Usuario(0, adminUsername, adminEmail, "admin123", DateTime.Now, Usuario.Roles.Administrativo, true, grupoPermisoId: grupoAdmin?.Id);
                if (grupoAdmin != null)
                {
                    admin.SetGrupo(grupoAdmin);
                }
                context.Usuarios.Add(admin);
                await context.SaveChangesAsync();
                logger.LogInformation("Usuarios: usuario '{Username}' insertado.", adminUsername);
            }
            else
            {
                if (adminExistente.GrupoPermisoId == null && grupoAdmin != null)
                {
                    adminExistente.SetGrupo(grupoAdmin);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Usuarios: grupo asignado a usuario '{Username}'.", adminUsername);
                }
                else
                {
                    logger.LogInformation("Usuarios: ya existe un usuario con username '{Username}' o email '{Email}', se omite el seed de admin.", adminUsername, adminEmail);
                }
            }

            // Seed de Usuarios para cada Profesional registrado
            var profesionales = await context.Profesionales.ToListAsync();
            foreach (var prof in profesionales)
            {
                var username = !string.IsNullOrWhiteSpace(prof.Email) && prof.Email.Contains('@')
                    ? prof.Email.Split('@')[0]
                    : $"{prof.Nombre.ToLower()}.{prof.Apellido.ToLower()}";

                if (username.Length < 5)
                {
                    username = $"{prof.Nombre.ToLower()}.{prof.Apellido.ToLower()}";
                }

                var email = !string.IsNullOrWhiteSpace(prof.Email)
                    ? prof.Email
                    : $"{username}@clinica.med.ar";

                var usuarioExistente = await context.Usuarios.Include(u => u.Grupo).FirstOrDefaultAsync(u => u.PersonaId == prof.Id || u.Username == username || u.Email == email);
                if (usuarioExistente != null)
                {
                    if (usuarioExistente.GrupoPermisoId == null && grupoProfesional != null)
                    {
                        usuarioExistente.SetGrupo(grupoProfesional);
                        await context.SaveChangesAsync();
                        logger.LogInformation("Usuarios: grupo asignado a profesional '{Username}'.", username);
                    }
                    else
                    {
                        logger.LogInformation("Usuarios: ya existe usuario para el profesional Id {ProfId} ('{Username}'), se omite.", prof.Id, username);
                    }
                    continue;
                }

                var usuarioProf = new Usuario(
                    id: 0,
                    username: username,
                    email: email,
                    password: "profesional123",
                    fechaCreacion: DateTime.Now,
                    rol: Usuario.Roles.Profesional,
                    activo: prof.Estado == Profesional.EstadoProfesional.Activo,
                    personaId: prof.Id,
                    grupoPermisoId: grupoProfesional?.Id);

                if (grupoProfesional != null)
                {
                    usuarioProf.SetGrupo(grupoProfesional);
                }

                context.Usuarios.Add(usuarioProf);
                await context.SaveChangesAsync();
                logger.LogInformation("Usuarios: usuario profesional '{Username}' (PersonaId: {PersonaId}) insertado.", username, prof.Id);
            }
        }
    }
}

