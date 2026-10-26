using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(IServiceProvider services, bool forceRecreate = false)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TPIContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TPIContext>>();
            var config = scope.ServiceProvider.GetService<IConfiguration>();

            bool forceFromConfig = config != null && bool.TryParse(config["Seed:ForceRecreate"], out var parsedForce) && parsedForce;
            bool force = forceRecreate || forceFromConfig;

            // Detección de base de datos desactualizada con el seed inicial anterior (< 50 turnos o sin facturas)
            if (!force && await context.Turnos.AnyAsync())
            {
                int turnosActuales = await context.Turnos.CountAsync();
                bool tieneFacturas = await context.Facturas.AnyAsync();
                if (turnosActuales < 50 || !tieneFacturas)
                {
                    logger.LogInformation("SeedData: Se detectó un conjunto de datos previo obsoleto ({Count} turnos, facturas: {TieneFacturas}). Se procederá a actualizar al nuevo dataset enriquecido.", turnosActuales, tieneFacturas);
                    force = true;
                }
            }

            if (force)
            {
                await LimpiarDatosOperativosAsync(context, logger);
            }

            await SeedEspecialidadesAsync(context, logger);
            await SeedProfesionalesAsync(context, logger);
            await SeedPacientesAsync(context, logger);
            await SeedTurnosAsync(context, logger);
            await SeedFacturasAsync(context, logger);
            await SeedRegistrosClinicosAsync(context, logger);
            await SeedPermisosYGruposAsync(context, logger);
            await SeedUsuariosAsync(context, logger);
        }

        /// <summary>
        /// Limpia de forma ordenada y segura los datos operativos de prueba (Facturas, Registros, Turnos, Historias, Pacientes, Profesionales y sus Usuarios)
        /// respetando las restricciones de integridad referencial. Preserva Especialidades, Permisos y Grupos.
        /// </summary>
        public static async Task LimpiarDatosOperativosAsync(TPIContext context, ILogger logger)
        {
            logger.LogInformation("SeedData: Iniciando limpieza de datos operativos para re-seeding...");

            // 1. Desvincular Turno.FacturaId para evitar ciclos referenciales
            if (await context.Turnos.AnyAsync(t => t.FacturaId != null))
            {
                var turnosConFactura = await context.Turnos.Where(t => t.FacturaId != null).ToListAsync();
                foreach (var t in turnosConFactura)
                {
                    t.DesasociarFactura();
                }
                await context.SaveChangesAsync();
            }

            // 2. Eliminar Facturas (y DetallesFactura en cascada)
            if (await context.Facturas.AnyAsync())
            {
                var facturas = await context.Facturas.Include(f => f.DetallesFactura).ToListAsync();
                context.Facturas.RemoveRange(facturas);
                await context.SaveChangesAsync();
            }

            // 3. Eliminar Registros Clínicos
            if (await context.RegistrosClinicos.AnyAsync())
            {
                var registros = await context.RegistrosClinicos.ToListAsync();
                context.RegistrosClinicos.RemoveRange(registros);
                await context.SaveChangesAsync();
            }

            // 4. Eliminar Turnos
            if (await context.Turnos.AnyAsync())
            {
                var turnos = await context.Turnos.ToListAsync();
                context.Turnos.RemoveRange(turnos);
                await context.SaveChangesAsync();
            }

            // 5. Eliminar Historias Clínicas
            if (await context.HistoriasClinicas.AnyAsync())
            {
                var historias = await context.HistoriasClinicas.ToListAsync();
                context.HistoriasClinicas.RemoveRange(historias);
                await context.SaveChangesAsync();
            }

            // 6. Eliminar Pacientes
            if (await context.Pacientes.AnyAsync())
            {
                var pacientes = await context.Pacientes.ToListAsync();
                context.Pacientes.RemoveRange(pacientes);
                await context.SaveChangesAsync();
            }

            // 7. Eliminar Usuarios asignados a Profesionales
            var usuariosProf = await context.Usuarios.Where(u => u.PersonaId != null).ToListAsync();
            if (usuariosProf.Any())
            {
                context.Usuarios.RemoveRange(usuariosProf);
                await context.SaveChangesAsync();
            }

            // 8. Eliminar Profesionales
            if (await context.Profesionales.AnyAsync())
            {
                var profesionales = await context.Profesionales.ToListAsync();
                context.Profesionales.RemoveRange(profesionales);
                await context.SaveChangesAsync();
            }

            logger.LogInformation("SeedData: Limpieza de datos operativos completada.");
        }

        // ==========================================
        // 1. Especialidades
        // ==========================================
        private static async Task SeedEspecialidadesAsync(TPIContext context, ILogger logger)
        {
            if (await context.Especialidades.AnyAsync())
            {
                var existentes = await context.Especialidades.ToListAsync();
                foreach (var esp in existentes)
                {
                    decimal precioEsperado = esp.Nombre switch
                    {
                        "Cardiología" => 35000m,
                        "Dermatología" => 28000m,
                        "Neurología" => 32000m,
                        "Pediatría" => 25000m,
                        "Clínica Médica" => 22000m,
                        _ => 25000m
                    };
                    if (esp.Precio != precioEsperado)
                    {
                        esp.SetPrecio(precioEsperado);
                    }
                }
                await context.SaveChangesAsync();
                logger.LogInformation("Especialidades: verificadas y actualizadas.");
                return;
            }

            var especialidades = new List<Especialidad>
            {
                new Especialidad(0, "Cardiología", 35000m),
                new Especialidad(0, "Dermatología", 28000m),
                new Especialidad(0, "Neurología", 32000m),
                new Especialidad(0, "Pediatría", 25000m),
                new Especialidad(0, "Clínica Médica", 22000m),
            };

            context.Especialidades.AddRange(especialidades);
            await context.SaveChangesAsync();
            logger.LogInformation("Especialidades: {Count} registros insertados.", especialidades.Count);
        }

        // ==========================================
        // 2. Profesionales (7 en total: 6 activos, 1 inactivo)
        // ==========================================
        private static async Task SeedProfesionalesAsync(TPIContext context, ILogger logger)
        {
            if (await context.Profesionales.AnyAsync())
            {
                logger.LogInformation("Profesionales: ya existen registros, se omite el seed.");
                return;
            }

            var cardio = await context.Especialidades.FirstAsync(e => e.Nombre == "Cardiología");
            var dermato = await context.Especialidades.FirstAsync(e => e.Nombre == "Dermatología");
            var neuro = await context.Especialidades.FirstAsync(e => e.Nombre == "Neurología");
            var pediatria = await context.Especialidades.FirstAsync(e => e.Nombre == "Pediatría");
            var clinica = await context.Especialidades.FirstAsync(e => e.Nombre == "Clínica Médica");

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
                    estado: Profesional.EstadoProfesional.Inactivo), // En licencia médica prolongada / inactivo

                new Profesional(
                    "Roberto", "Gómez", "DNI", "20555666", "MP-1005", neuro.Id,
                    telefono: "011-4523-1104",
                    email: "r.gomez@clinica.med.ar",
                    estado: Profesional.EstadoProfesional.Activo),

                new Profesional(
                    "Martín", "Suárez", "DNI", "20444555", "MP-1004", pediatria.Id,
                    telefono: "011-4523-1103",
                    email: "m.suarez@clinica.med.ar",
                    estado: Profesional.EstadoProfesional.Activo),

                new Profesional(
                    "Carla", "Méndez", "DNI", "20666777", "MP-1006", clinica.Id,
                    telefono: "011-4523-1105",
                    email: "c.mendez@clinica.med.ar",
                    estado: Profesional.EstadoProfesional.Activo),

                new Profesional(
                    "Esteban", "Bianchi", "DNI", "20777888", "MP-1007", clinica.Id,
                    telefono: "011-4523-1106",
                    email: "e.bianchi@clinica.med.ar",
                    estado: Profesional.EstadoProfesional.Activo),
            };

            context.Profesionales.AddRange(profesionales);
            await context.SaveChangesAsync();
            logger.LogInformation("Profesionales: {Count} registros insertados.", profesionales.Count);
        }

        // ==========================================
        // 3. Pacientes (11 en total) + Historias Clínicas
        // ==========================================
        private static async Task SeedPacientesAsync(TPIContext context, ILogger logger)
        {
            if (await context.Pacientes.AnyAsync())
            {
                logger.LogInformation("Pacientes: ya existen registros, se omite el seed.");
                return;
            }

            var pacientes = new List<Paciente>
            {
                // Adultos habituales
                new Paciente("Ana", "García", "DNI", "30111222", new DateTime(1985, 4, 12), "OSDE", "011-5555-1001", "ana.garcia@email.com"),
                new Paciente("Carlos", "López", "DNI", "30222333", new DateTime(1978, 9, 25), "Swiss Medical", "011-5555-1002", "carlos.lopez@email.com"),
                new Paciente("Sofía", "Martínez", "DNI", "30333444", new DateTime(1992, 11, 3), "Galeno", "011-5555-1003", "sofia.martinez@email.com"),
                new Paciente("Diego", "Ramírez", "DNI", "30444555", new DateTime(1990, 6, 18), "IAPOS", "011-5555-1004", "diego.ramirez@email.com"),

                // Pacientes Pediátricos (para Dr. Suárez en Pediatría)
                new Paciente("Mateo", "Rossi", "DNI", "50111222", new DateTime(2019, 5, 14), "OSDE", "011-5555-1005", "familia.rossi@email.com"),
                new Paciente("Valentina", "Gómez", "DNI", "48222333", new DateTime(2016, 8, 20), "Swiss Medical", "011-5555-1006", "familia.gomez@email.com"),

                // Pacientes Adultos Mayores (para Cardiología, Neurología y Clínica Médica)
                new Paciente("Elena", "Domínguez", "DNI", "12333444", new DateTime(1955, 3, 10), "PAMI", "011-5555-1007", "elena.dominguez@email.com"),
                new Paciente("Jorge", "Morales", "DNI", "16666777", new DateTime(1962, 10, 5), "IAPOS", "011-5555-1010", "jorge.morales@email.com"),

                // Paciente Particular (sin obra social)
                new Paciente("Mariano", "Benítez", "DNI", "29444555", new DateTime(1982, 12, 1), "Particular", "011-5555-1008", "m.benitez@email.com"),

                // Pacientes Jóvenes Adultos adicionales
                new Paciente("Camila", "Navarro", "DNI", "41555666", new DateTime(1999, 7, 22), "Galeno", "011-5555-1009", "camila.navarro@email.com"),
                new Paciente("Lucía", "Herrera", "DNI", "33777888", new DateTime(1988, 2, 17), "Swiss Medical", "011-5555-1011", "lucia.herrera@email.com"),
            };

            context.Pacientes.AddRange(pacientes);
            await context.SaveChangesAsync();
            logger.LogInformation("Pacientes: {Count} registros insertados.", pacientes.Count);

            // Crear HistoriaClinica para cada Paciente con grupos sanguíneos variados
            var historias = pacientes.Select((p, i) => new HistoriaClinica(
                p.Id,
                (TypeGrupoSanguineo)((i % 8) + 1) // A+, A-, B+, B-, AB+, AB-, O+, O-
            )).ToList();

            context.HistoriasClinicas.AddRange(historias);
            await context.SaveChangesAsync();
            logger.LogInformation("HistoriasClinicas: {Count} registros insertados.", historias.Count);
        }

        // ==========================================
        // 4. Turnos (3 meses pasados, hoy, 2 meses futuros)
        // ==========================================
        private static async Task SeedTurnosAsync(TPIContext context, ILogger logger)
        {
            if (await context.Turnos.AnyAsync())
            {
                logger.LogInformation("Turnos: ya existen registros, se omite el seed.");
                return;
            }

            var profesionales = await context.Profesionales.Include(p => p.Especialidad).ToListAsync();
            var pacientes = await context.Pacientes.ToListAsync();

            if (!profesionales.Any() || !pacientes.Any())
            {
                logger.LogWarning("Turnos: faltan profesionales o pacientes para crear turnos.");
                return;
            }

            // Médicos activos
            var draFernandez = profesionales.First(p => p.Apellido == "Fernández");
            var drRodriguez = profesionales.First(p => p.Apellido == "Rodríguez");
            var drGomez = profesionales.First(p => p.Apellido == "Gómez");
            var drSuarez = profesionales.First(p => p.Apellido == "Suárez");
            var draMendez = profesionales.First(p => p.Apellido == "Méndez");
            var drBianchi = profesionales.First(p => p.Apellido == "Bianchi");

            // Pacientes por especialidad
            var pacCardio = pacientes.Where(p => p.Apellido is "García" or "Domínguez" or "Ramírez").ToList();
            var pacDermato = pacientes.Where(p => p.Apellido is "López" or "Navarro" or "Martínez").ToList();
            var pacNeuro = pacientes.Where(p => p.Apellido is "Benítez" or "Herrera" or "Martínez").ToList();
            var pacPediatria = pacientes.Where(p => p.Apellido is "Rossi" or "Gómez").ToList();
            var pacClinica = pacientes.Where(p => p.Apellido is "Morales" or "Ramírez" or "López" or "Herrera" or "García").ToList();

            var today = DateTime.Today;
            var turnos = new List<Turno>();

            // Configuración de horarios por profesional:
            // Cada profesional atiende 2 días a la semana con 3 o 4 franjas horarias de 30 minutos sin solapamiento
            var agendas = new[]
            {
                new { Prof = draFernandez, Dias = new[] { DayOfWeek.Monday, DayOfWeek.Wednesday }, HoraInicio = 8, Minutos = 30, Slots = 3, Pacientes = pacCardio, Especialidad = "Cardiología" },
                new { Prof = drRodriguez,  Dias = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday },  HoraInicio = 9, Minutos = 0,  Slots = 3, Pacientes = pacDermato, Especialidad = "Dermatología" },
                new { Prof = drGomez,      Dias = new[] { DayOfWeek.Monday, DayOfWeek.Thursday },  HoraInicio = 14, Minutos = 0, Slots = 3, Pacientes = pacNeuro,   Especialidad = "Neurología" },
                new { Prof = drSuarez,     Dias = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },    HoraInicio = 14, Minutos = 0, Slots = 3, Pacientes = pacPediatria, Especialidad = "Pediatría" },
                new { Prof = draMendez,    Dias = new[] { DayOfWeek.Wednesday, DayOfWeek.Friday },  HoraInicio = 9, Minutos = 0,  Slots = 3, Pacientes = pacClinica, Especialidad = "Clínica Médica" },
                new { Prof = drBianchi,    Dias = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday },  HoraInicio = 15, Minutos = 30, Slots = 3, Pacientes = pacClinica, Especialidad = "Clínica Médica" }
            };

            int seedIndex = 0;

            // -------------------------------------------------------------
            // A. TURNOS HISTÓRICOS (Últimos 3 meses: Hace 90 días hasta ayer)
            // -------------------------------------------------------------
            for (int diaOffset = -90; diaOffset < 0; diaOffset++)
            {
                var fechaActual = today.AddDays(diaOffset);

                foreach (var agenda in agendas)
                {
                    if (agenda.Dias.Contains(fechaActual.DayOfWeek))
                    {
                        // Para mantener el volumen intermedio acordado (~150 a 250 turnos totales),
                        // programamos una semana sí y una no por profesional, o 2 slots por día
                        if ((diaOffset / 7) % 2 == 0) continue;

                        for (int slot = 0; slot < agenda.Slots; slot++)
                        {
                            var horaInicio = fechaActual.AddHours(agenda.HoraInicio).AddMinutes(agenda.Minutos + (slot * 30));
                            var horaFin = horaInicio.AddMinutes(30);

                            seedIndex++;
                            // Distribución de estados en histórico:
                            // ~80% Atendido, ~15% Ausente, ~5% Libre
                            int mod = seedIndex % 10;
                            if (mod == 0) // ~10% Libre
                            {
                                turnos.Add(new Turno(
                                    id: 0,
                                    fechaHoraInicio: horaInicio,
                                    fechaHoraFin: horaFin,
                                    motivo: string.Empty,
                                    estadoTurno: Turno.EstadosTurno.Libre,
                                    observaciones: "Turno no asignado del período anterior.",
                                    facturaId: null,
                                    profesionalId: agenda.Prof.Id,
                                    pacienteId: null
                                ));
                            }
                            else if (mod == 9) // ~10% Ausente
                            {
                                var pac = agenda.Pacientes[seedIndex % agenda.Pacientes.Count];
                                bool registroLlegada = (seedIndex % 2 == 0);
                                turnos.Add(new Turno(
                                    id: 0,
                                    fechaHoraInicio: horaInicio,
                                    fechaHoraFin: horaFin,
                                    motivo: ObtenerMotivoConsulta(agenda.Especialidad, seedIndex),
                                    estadoTurno: Turno.EstadosTurno.Ausente,
                                    observaciones: registroLlegada
                                        ? "El paciente se anunció en recepción pero se retiró por motivos personales antes de ser llamado."
                                        : "El paciente no se presentó al turno programado ni notificó cancelación previa.",
                                    facturaId: null,
                                    profesionalId: agenda.Prof.Id,
                                    pacienteId: pac.Id,
                                    fechaHoraLlegada: registroLlegada ? horaInicio.AddMinutes(-10) : null
                                ));
                            }
                            else // ~80% Atendido
                            {
                                var pac = agenda.Pacientes[seedIndex % agenda.Pacientes.Count];
                                turnos.Add(new Turno(
                                    id: 0,
                                    fechaHoraInicio: horaInicio,
                                    fechaHoraFin: horaFin,
                                    motivo: ObtenerMotivoConsulta(agenda.Especialidad, seedIndex),
                                    estadoTurno: Turno.EstadosTurno.Atendido,
                                    observaciones: "Consulta médica finalizada correctamente.",
                                    facturaId: null,
                                    profesionalId: agenda.Prof.Id,
                                    pacienteId: pac.Id,
                                    fechaHoraLlegada: horaInicio.AddMinutes(-8)
                                ));
                            }
                        }
                    }
                }
            }

            // -------------------------------------------------------------
            // B. TURNOS DEL DÍA DE HOY (today)
            // -------------------------------------------------------------
            // 1. Dra. Fernández (Cardio) - 1 Atendido (mañana temprana), 1 Presente (en espera)
            turnos.Add(new Turno(
                id: 0,
                fechaHoraInicio: today.AddHours(8).AddMinutes(30),
                fechaHoraFin: today.AddHours(9),
                motivo: "Control post-ajuste de antihipertensivo",
                estadoTurno: Turno.EstadosTurno.Atendido,
                observaciones: "Paciente asistió a primera hora. Presión arterial estabilizada.",
                facturaId: null,
                profesionalId: draFernandez.Id,
                pacienteId: pacCardio[0].Id,
                fechaHoraLlegada: today.AddHours(8).AddMinutes(20)
            ));

            turnos.Add(new Turno(
                id: 0,
                fechaHoraInicio: today.AddHours(10),
                fechaHoraFin: today.AddHours(10).AddMinutes(30),
                motivo: "Chequeo cardiológico preventivo",
                estadoTurno: Turno.EstadosTurno.Presente,
                observaciones: "Paciente anunciado y esperando en sala de recepción.",
                facturaId: null,
                profesionalId: draFernandez.Id,
                pacienteId: pacCardio[1].Id,
                fechaHoraLlegada: today.AddHours(9).AddMinutes(50)
            ));

            // 2. Dr. Rodríguez (Dermato) - 1 Asignado para la tarde y 1 Libre
            turnos.Add(new Turno(
                id: 0,
                fechaHoraInicio: today.AddHours(14),
                fechaHoraFin: today.AddHours(14).AddMinutes(30),
                motivo: "Control evolutivo de lesión cutánea",
                estadoTurno: Turno.EstadosTurno.Asignado,
                observaciones: "Turno del día confirmado telefónicamente.",
                facturaId: null,
                profesionalId: drRodriguez.Id,
                pacienteId: pacDermato[0].Id
            ));

            turnos.Add(new Turno(
                id: 0,
                fechaHoraInicio: today.AddHours(14).AddMinutes(30),
                fechaHoraFin: today.AddHours(15),
                motivo: string.Empty,
                estadoTurno: Turno.EstadosTurno.Libre,
                observaciones: "Slot libre para atención vespertina.",
                facturaId: null,
                profesionalId: drRodriguez.Id,
                pacienteId: null
            ));

            // 3. Dr. Suárez (Pediatría) - 1 Atendido matutino y 1 Asignado tarde
            turnos.Add(new Turno(
                id: 0,
                fechaHoraInicio: today.AddHours(9).AddMinutes(30),
                fechaHoraFin: today.AddHours(10),
                motivo: "Control de niño sano - percentiles",
                estadoTurno: Turno.EstadosTurno.Atendido,
                observaciones: "Crecimiento acorde a la edad. Vacunas al día.",
                facturaId: null,
                profesionalId: drSuarez.Id,
                pacienteId: pacPediatria[0].Id,
                fechaHoraLlegada: today.AddHours(9).AddMinutes(20)
            ));

            turnos.Add(new Turno(
                id: 0,
                fechaHoraInicio: today.AddHours(16),
                fechaHoraFin: today.AddHours(16).AddMinutes(30),
                motivo: "Evaluación por cuadro respiratorio leve",
                estadoTurno: Turno.EstadosTurno.Asignado,
                observaciones: "Turno pediátrico programado para hoy.",
                facturaId: null,
                profesionalId: drSuarez.Id,
                pacienteId: pacPediatria[1].Id
            ));

            // -------------------------------------------------------------
            // C. TURNOS FUTUROS (Próximos 2 meses: Mañana a +60 días)
            // -------------------------------------------------------------
            for (int diaOffset = 1; diaOffset <= 60; diaOffset++)
            {
                var fechaActual = today.AddDays(diaOffset);

                foreach (var agenda in agendas)
                {
                    if (agenda.Dias.Contains(fechaActual.DayOfWeek))
                    {
                        if ((diaOffset / 7) % 2 == 0) continue;

                        for (int slot = 0; slot < agenda.Slots; slot++)
                        {
                            var horaInicio = fechaActual.AddHours(agenda.HoraInicio).AddMinutes(agenda.Minutos + (slot * 30));
                            var horaFin = horaInicio.AddMinutes(30);

                            seedIndex++;
                            // Distribución de estados en futuro:
                            // ~35% Asignado, ~65% Libre (para que haya turnos disponibles para reservar en la app)
                            bool estaAsignado = (seedIndex % 3 == 0);

                            if (estaAsignado)
                            {
                                var pac = agenda.Pacientes[seedIndex % agenda.Pacientes.Count];
                                turnos.Add(new Turno(
                                    id: 0,
                                    fechaHoraInicio: horaInicio,
                                    fechaHoraFin: horaFin,
                                    motivo: ObtenerMotivoConsulta(agenda.Especialidad, seedIndex),
                                    estadoTurno: Turno.EstadosTurno.Asignado,
                                    observaciones: "Turno programado confirmado.",
                                    facturaId: null,
                                    profesionalId: agenda.Prof.Id,
                                    pacienteId: pac.Id
                                ));
                            }
                            else
                            {
                                turnos.Add(new Turno(
                                    id: 0,
                                    fechaHoraInicio: horaInicio,
                                    fechaHoraFin: horaFin,
                                    motivo: string.Empty,
                                    estadoTurno: Turno.EstadosTurno.Libre,
                                    observaciones: "Turno disponible para asignación.",
                                    facturaId: null,
                                    profesionalId: agenda.Prof.Id,
                                    pacienteId: null
                                ));
                            }
                        }
                    }
                }
            }

            context.Turnos.AddRange(turnos);
            await context.SaveChangesAsync();
            logger.LogInformation("Turnos: {Count} registros insertados.", turnos.Count);
        }

        private static string ObtenerMotivoConsulta(string especialidad, int index)
        {
            return especialidad switch
            {
                "Cardiología" => (index % 4) switch
                {
                    0 => "Control de tensión arterial y medicación",
                    1 => "Evaluación cardiológica preventiva con ECG",
                    2 => "Seguimiento de arritmia y disnea de esfuerzo",
                    _ => "Chequeo periódico de salud cardiovascular"
                },
                "Dermatología" => (index % 4) switch
                {
                    0 => "Control y dermatoscopía de nevus (lunares)",
                    1 => "Tratamiento y seguimiento de dermatitis de contacto",
                    2 => "Consulta por brote de eccema en manos y antebrazo",
                    _ => "Evaluación dermatológica general por acné"
                },
                "Neurología" => (index % 4) switch
                {
                    0 => "Evaluación por cefaleas recurrentes y migrañas",
                    1 => "Control de tratamiento profiláctico de cefalea tensional",
                    2 => "Consulta por episodios de mareos y vértigo posicional",
                    _ => "Seguimiento neurológico y revisión de resonancia magnética"
                },
                "Pediatría" => (index % 4) switch
                {
                    0 => "Control periódico de niño sano y percentiles de crecimiento",
                    1 => "Revisión de esquema oficial de vacunación",
                    2 => "Consulta por tos persistente y catarro de vías aéreas superiores",
                    _ => "Certificado de aptitud física escolar y chequeo pediátrico"
                },
                _ => (index % 4) switch
                {
                    0 => "Chequeo médico clínico anual y análisis de laboratorio",
                    1 => "Control de glucemia y hemoglobina glicosilada",
                    2 => "Evaluación clínica por fatiga y dolores musculares",
                    _ => "Seguimiento de perfil lipídico y síndrome metabólico"
                }
            };
        }

        // ==========================================
        // 5. Facturas (Turnos Atendidos: ~75% Pagadas, ~20% Pendientes, 2-3 Canceladas)
        // ==========================================
        private static async Task SeedFacturasAsync(TPIContext context, ILogger logger)
        {
            if (await context.Facturas.AnyAsync())
            {
                logger.LogInformation("Facturas: ya existen registros, se omite el seed.");
                return;
            }

            var turnosAtendidos = await context.Turnos
                .Include(t => t.Profesional)
                    .ThenInclude(p => p.Especialidad)
                .Where(t => t.EstadoTurno == Turno.EstadosTurno.Atendido && t.FacturaId == null)
                .OrderBy(t => t.FechaHoraInicio)
                .ToListAsync();

            if (!turnosAtendidos.Any())
            {
                logger.LogWarning("Facturas: no hay turnos atendidos disponibles para asociar facturas.");
                return;
            }

            int facturadosCount = 0;
            int canceladasCount = 0;
            // Dejamos aproximadamente el 20% de los turnos atendidos como pendientes para que los reportes muestren la columna 'Diferencia'
            int cantidadAFacturar = (int)(turnosAtendidos.Count * 0.78);
            var turnosParaFacturar = turnosAtendidos.Take(cantidadAFacturar).ToList();

            var metodosPago = new[]
            {
                Factura.MetodosPago.TransferenciaBancaria,
                Factura.MetodosPago.TarjetaCredito,
                Factura.MetodosPago.TarjetaDebito,
                Factura.MetodosPago.Efectivo
            };

            foreach (var turno in turnosParaFacturar)
            {
                var metodoPago = metodosPago[facturadosCount % metodosPago.Length];
                var fechaEmision = turno.FechaHoraInicio.AddMinutes(35);
                var factura = new Factura(turno.Id, metodoPago, fechaEmision);

                decimal precioConsulta = turno.Profesional?.Especialidad?.Precio ?? 25000m;
                factura.AgregarDetalle("Consulta Médica Especializada", 1, precioConsulta);

                // Cada 3 facturas agregamos un estudio complementario según especialidad para dotar de realismo
                if (facturadosCount % 3 == 0)
                {
                    string estudio = turno.Profesional?.Especialidad?.Nombre switch
                    {
                        "Cardiología" => "Electrocardiograma de 12 derivaciones",
                        "Dermatología" => "Biopsia de lesión cutánea por punch",
                        "Neurología" => "Electroencefalograma computarizado",
                        "Pediatría" => "Prueba diagnóstica rápida de hisopado",
                        _ => "Estudio de perfil lipídico y metabólico"
                    };

                    decimal precioEstudio = turno.Profesional?.Especialidad?.Nombre switch
                    {
                        "Cardiología" => 15000m,
                        "Dermatología" => 20000m,
                        "Neurología" => 22000m,
                        "Pediatría" => 9000m,
                        _ => 12000m
                    };

                    factura.AgregarDetalle(estudio, 1, precioEstudio);
                }

                context.Facturas.Add(factura);
                await context.SaveChangesAsync();

                turno.AsociarFactura(factura.Id);
                facturadosCount++;

                // Para 2 de las facturas intermedias, probamos el flujo de anulación (Cancelada)
                // Esto comprueba las reglas de negocio de anulación y que no computen en el facturado activo del reporte
                if (canceladasCount < 2 && facturadosCount > 5 && facturadosCount % 8 == 0)
                {
                    factura.Anular();
                    turno.DesasociarFactura();
                    canceladasCount++;
                }
            }

            await context.SaveChangesAsync();
            logger.LogInformation("Facturas: {Count} facturas insertadas ({Canceladas} canceladas, resto activas pagadas).", facturadosCount, canceladasCount);
        }

        // ==========================================
        // 6. Registros Clínicos (Antecedentes/Alergias basales + Registros por cada Turno Atendido)
        // ==========================================
        private static async Task SeedRegistrosClinicosAsync(TPIContext context, ILogger logger)
        {
            if (await context.RegistrosClinicos.AnyAsync())
            {
                logger.LogInformation("RegistrosClinicos: ya existen registros, se omite el seed.");
                return;
            }

            var historias = await context.HistoriasClinicas
                .Include(h => h.RegistrosClinicos)
                .ToListAsync();

            var profesionales = await context.Profesionales
                .Include(p => p.Especialidad)
                .ToListAsync();

            var turnosAtendidos = await context.Turnos
                .Include(t => t.Profesional)
                    .ThenInclude(p => p.Especialidad)
                .Where(t => t.EstadoTurno == Turno.EstadosTurno.Atendido)
                .OrderBy(t => t.FechaHoraInicio)
                .ToListAsync();

            if (!historias.Any() || !profesionales.Any())
            {
                logger.LogWarning("RegistrosClinicos: no se encontraron historias clínicas o profesionales.");
                return;
            }

            var draFernandez = profesionales.First(p => p.Apellido == "Fernández");
            var drRodriguez = profesionales.First(p => p.Apellido == "Rodríguez");
            var drGomez = profesionales.First(p => p.Apellido == "Gómez");
            var drSuarez = profesionales.First(p => p.Apellido == "Suárez");
            var draMendez = profesionales.First(p => p.Apellido == "Méndez");

            // 1. Cargar Antecedentes y Alergias Basales en las historias clínicas
            var pacientes = await context.Pacientes.ToListAsync();
            foreach (var hc in historias)
            {
                var pac = pacientes.FirstOrDefault(p => p.Id == hc.PacienteId);
                if (pac == null) continue;

                var fechaBase = DateTime.UtcNow.AddMonths(-4);

                switch (pac.Apellido)
                {
                    case "García":
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Alergia a la penicilina y derivados betalactámicos. Reacción: eritema cutáneo y prurito.", draFernandez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Madre con hipertensión arterial severa. Diagnóstico personal de HTA primaria a los 35 años.", draFernandez, null, fechaBase);
                        break;
                    case "López":
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Alergia de contacto a sulfatos y fragancias artificiales.", drRodriguez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Asma bronquial durante la infancia, sin episodios en los últimos 5 años.", drRodriguez, null, fechaBase);
                        break;
                    case "Martínez":
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Sin alergias medicamentosas conocidas.", draMendez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Cirugía de apendicectomía en 2018 sin complicaciones posteriores.", draMendez, null, fechaBase);
                        break;
                    case "Ramírez":
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Antecedentes familiares de cardiopatía isquémica precoz (padre fallecido a los 58 años).", draFernandez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Lumbalgia mecánica recurrente por sedentarismo y postura laboral.", draMendez, null, fechaBase);
                        break;
                    case "Rossi":
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Nacido a término (39 semanas, peso 3.400 g). Sin antecedentes perinatológicos patológicos.", drSuarez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Intolerancia leve a la lactosa durante el primer año de vida, superada.", drSuarez, null, fechaBase);
                        break;
                    case "Gómez":
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Broncoespasmo a repetición a los 3 años. Esquema de vacunas completo al día.", drSuarez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Rinitis alérgica estacional asociada a polen y ácaros.", drSuarez, null, fechaBase);
                        break;
                    case "Domínguez":
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Fibrilación auricular paroxística diagnosticada en 2021. Hipotiroidismo en tratamiento con Levotiroxina.", draFernandez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Sin alergias conocidas.", draFernandez, null, fechaBase);
                        break;
                    case "Benítez":
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Cefaleas pulsátiles hemicraneanas desde los 25 años. Tabaquismo ocasional.", drGomez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Alergia a AINEs (Dipirona) manifestada con urticaria cutánea.", drGomez, null, fechaBase);
                        break;
                    case "Navarro":
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Acné moderado desde la adolescencia. Buena salud general.", drRodriguez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Sin alergias medicamentosas.", drRodriguez, null, fechaBase);
                        break;
                    case "Morales":
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Diabetes mellitus tipo 2 diagnosticada hace 4 años. Sobrepeso grado 1.", draMendez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Hipertensión arterial estadio 1.", draMendez, null, fechaBase);
                        break;
                    case "Herrera":
                        hc.AgregarRegistro(TipoRegistroClinico.Antecedente, "Contracturas cervicales frecuentes por estrés laboral.", drGomez, null, fechaBase);
                        hc.AgregarRegistro(TipoRegistroClinico.Alergia, "Sin alergias medicamentosas conocidas.", drGomez, null, fechaBase);
                        break;
                }
            }

            // 2. Cargar Registros Clínicos originados en cada Turno Atendido con coherencia clínica y temporal
            int registrosTurnosCount = 0;
            foreach (var turno in turnosAtendidos)
            {
                var hc = historias.FirstOrDefault(h => h.PacienteId == turno.PacienteId);
                var prof = turno.Profesional ?? profesionales.First(p => p.Id == turno.ProfesionalId);
                if (hc == null || prof == null) continue;

                var fechaTurno = turno.FechaHoraInicio;
                var espNombre = prof.Especialidad?.Nombre ?? "Clínica Médica";

                switch (espNombre)
                {
                    case "Cardiología":
                        turno.Registrar(
                            TipoRegistroClinico.NotaClinica,
                            "Control cardiológico. TA: 135/85 mmHg, FC: 72 lpm. Ruidos cardíacos netos, sin soplos.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Diagnostico,
                            "Hipertensión arterial primaria grado 1 en seguimiento clínico.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Tratamiento,
                            "Continuar con Enalapril 10mg diario. Se pauta dieta hiposódica y control de presión en 30 días.",
                            prof, hc, fechaTurno);
                        if (registrosTurnosCount % 2 == 0)
                        {
                            turno.Registrar(
                                TipoRegistroClinico.Evolucion,
                                "Evolución clínica favorable con adecuada tolerancia a la medicación prescrita.",
                                prof, hc, fechaTurno.AddMinutes(15));
                        }
                        break;

                    case "Dermatología":
                        turno.Registrar(
                            TipoRegistroClinico.NotaClinica,
                            "Examen dermatoscópico. Lesiones eritemato-escamosas pruriginosas en zonas de flexión.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Diagnostico,
                            "Dermatitis atópica reactiva con sobreinfección leve por rascado.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Tratamiento,
                            "Betametasona tópica 0.05% cada 12 hs durante 7 días y crema emoliente hidratante diaria.",
                            prof, hc, fechaTurno);
                        break;

                    case "Neurología":
                        turno.Registrar(
                            TipoRegistroClinico.NotaClinica,
                            "Evaluación neurológica: pares craneales normales, sensibilidad y motilidad simétricas. Fondo de ojo normal.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Diagnostico,
                            "Cefalea tensional episódica desencadenada por sobrecarga laboral.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Tratamiento,
                            "Ibuprofeno 400mg al rescate de crisis. Pautas de higiene postural y reducción de pantallas nocturnas.",
                            prof, hc, fechaTurno);
                        break;

                    case "Pediatría":
                        turno.Registrar(
                            TipoRegistroClinico.NotaClinica,
                            "Control de salud pediátrico. Peso y talla en percentil 50. Auscultación cardiopulmonar normal.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Diagnostico,
                            "Crecimiento y desarrollo infantil acorde a la edad cronológica. Niño sano.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Tratamiento,
                            "Pautas de nutrición balanceada y estímulo de actividad recreativa al aire libre.",
                            prof, hc, fechaTurno);
                        break;

                    default: // Clínica Médica
                        turno.Registrar(
                            TipoRegistroClinico.NotaClinica,
                            "Examen clínico preventivo. TA 120/80 mmHg, abdomen blando e indoloro, pulsos periféricos conservados.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Diagnostico,
                            "Examen médico periódico general. Sin hallazgos patológicos agudos.",
                            prof, hc, fechaTurno);
                        turno.Registrar(
                            TipoRegistroClinico.Tratamiento,
                            "Se indican análisis de laboratorio de rutina (hemograma, glucemia, perfil lipídico y orina completa).",
                            prof, hc, fechaTurno);
                        break;
                }

                registrosTurnosCount++;
            }

            await context.SaveChangesAsync();
            var totalRegistros = await context.RegistrosClinicos.CountAsync();
            logger.LogInformation("RegistrosClinicos: {Count} registros totales insertados con fechas concordantes a sus turnos.", totalRegistros);
        }

        // ==========================================
        // 7. Permisos y Grupos
        // ==========================================
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
                ("datosmaestros", "leer", "Acceso a datos maestros"),

                // Reportes
                ("reportes", "turnos.leer", "Leer reporte de turnos"),
                ("reportes", "facturacion.leer", "Leer reporte de facturación")
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

            // Asignar permisos a Administrador (Historias Clínicas son exclusivas de Profesional)
            var todosLosPermisos = await context.Permisos.Where(p => p.Activo).ToListAsync();
            foreach (var permiso in todosLosPermisos)
            {
                if (permiso.Categoria.Equals("historiasclinicas", StringComparison.OrdinalIgnoreCase))
                {
                    var permisoExistente = grupoAdmin.Permisos.FirstOrDefault(p => p.Id == permiso.Id);
                    if (permisoExistente != null)
                    {
                        grupoAdmin.RemoverPermiso(permisoExistente);
                    }
                }
                else
                {
                    if (!grupoAdmin.Permisos.Any(p => p.Id == permiso.Id))
                    {
                        grupoAdmin.AgregarPermiso(permiso);
                    }
                }
            }

            // Asignar permisos a Profesional
            var categoriasYAccionesProfesional = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "pacientes.leer",
                "turnos.leer",
                "turnos.atender",
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
                else
                {
                    var permisoExistente = grupoProfesional.Permisos.FirstOrDefault(p => p.Id == permiso.Id);
                    if (permisoExistente != null)
                    {
                        grupoProfesional.RemoverPermiso(permisoExistente);
                    }
                }
            }

            await context.SaveChangesAsync();
            logger.LogInformation("Permisos y Grupos: seed completado.");
        }

        // ==========================================
        // 8. Usuarios (Admin + Usuarios para cada uno de los 7 Profesionales)
        // ==========================================
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
