using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PacienteEndpoints
    {
        public static void MapPacienteEndpoints(this WebApplication app)
        {
            // GET: /pacientes
            app.MapGet("/pacientes", async (IPacienteService pacienteService) =>
            {
                var pacientes = await pacienteService.GetAllAsync();
                return Results.Ok(pacientes);
            })
            .WithName("GetAllPacientes")
            .Produces<List<PacienteDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("PacientesLeer");

            // GET: /pacientes/{id:int}
            app.MapGet("/pacientes/{id:int}", async (int id, IPacienteService pacienteService) =>
            {
                var paciente = await pacienteService.GetByIdAsync(id);
                return paciente is not null ? Results.Ok(paciente) : Results.NotFound();
            })
            .WithName("GetPacienteById")
            .Produces<PacienteDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization("PacientesLeer");

            // GET: /pacientes/criteria?texto=&obraSocial=&especialidadId=&profesionalId=
            app.MapGet("/pacientes/criteria", async (string? texto, string? obraSocial, int? especialidadId, int? profesionalId, IPacienteService pacienteService) =>
            {
                var criteria = new PacienteCriteriaDTO
                {
                    Texto = texto ?? string.Empty,
                    ObraSocial = obraSocial,
                    EspecialidadId = especialidadId,
                    ProfesionalId = profesionalId
                };
                var pacientes = await pacienteService.GetByCriteriaAsync(criteria);
                return Results.Ok(pacientes);
            })
            .WithName("GetPacientesByCriteria")
            .Produces<List<PacienteDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("PacientesLeer");

            // POST: /pacientes
            app.MapPost("/pacientes", async (PacienteDTO pacienteDto, IPacienteService pacienteService) =>
            {
                var created = await pacienteService.AddAsync(pacienteDto);
                return Results.Created($"/pacientes/{created.Id}", created);
            })
            .WithName("CreatePaciente")
            .Produces<PacienteDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("PacientesAgregar");

            // PUT: /pacientes/{id:int}
            app.MapPut("/pacientes/{id:int}", async (int id, PacienteDTO pacienteDto, IPacienteService pacienteService) =>
            {
                if (id != pacienteDto.Id)
                    throw new ArgumentException("El Id de la URL no coincide con el Id del cuerpo de la solicitud.", nameof(id));

                var updated = await pacienteService.UpdateAsync(pacienteDto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdatePaciente")
            .Produces<PacienteDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization("PacientesActualizar");

            // DELETE: /pacientes/{id:int}
            app.MapDelete("/pacientes/{id:int}", async (int id, IPacienteService pacienteService) =>
            {
                var deleted = await pacienteService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeletePaciente")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi()
            .RequireAuthorization("PacientesEliminar");
        }
    }
}
