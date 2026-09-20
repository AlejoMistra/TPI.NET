using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class HistoriaClinicaEndpoints
    {
        public static void MapHistoriaClinicaEndpoints(this WebApplication app)
        {
            // GET: /historias-clinicas/{id}
            app.MapGet("/historias-clinicas/{id:int}", async (int id, IHistoriaClinicaService service) =>
            {
                if (id <= 0)
                    return Results.BadRequest(new { error = "El Id de la historia clínica no es válido." });

                try
                {
                    var historia = await service.GetByIdAsync(id);
                    return historia is not null ? Results.Ok(historia) : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetHistoriaClinicaById")
            .Produces<HistoriaClinicaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // GET: /historias-clinicas/paciente/{pacienteId}
            app.MapGet("/historias-clinicas/paciente/{pacienteId:int}", async (int pacienteId, IHistoriaClinicaService service) =>
            {
                if (pacienteId <= 0)
                    return Results.BadRequest(new { error = "El Id del paciente no es válido." });

                try
                {
                    var historia = await service.GetByPacienteIdAsync(pacienteId);
                    return historia is not null ? Results.Ok(historia) : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("GetHistoriaClinicaByPacienteId")
            .Produces<HistoriaClinicaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /historias-clinicas
            app.MapPost("/historias-clinicas", async (HistoriaClinicaCreateDTO dto, IHistoriaClinicaService service) =>
            {
                try
                {
                    var created = await service.CreateAsync(dto);
                    return Results.Created($"/historias-clinicas/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("CreateHistoriaClinica")
            .Produces<HistoriaClinicaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            // PUT: /historias-clinicas/{id}/grupo-sanguineo
            app.MapPut("/historias-clinicas/{id:int}/grupo-sanguineo", async (int id, HistoriaClinicaUpdateGrupoDTO dto, IHistoriaClinicaService service) =>
            {
                if (id <= 0)
                    return Results.BadRequest(new { error = "El Id de la historia clínica no es válido." });

                try
                {
                    var updated = await service.UpdateGrupoSanguineoAsync(id, dto);
                    return updated is not null ? Results.Ok(updated) : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateGrupoSanguineo")
            .Produces<HistoriaClinicaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /historias-clinicas/{id}/registros
            app.MapPost("/historias-clinicas/{id:int}/registros", async (int id, RegistroClinicoCreateDTO dto, IHistoriaClinicaService service) =>
            {
                if (id <= 0)
                    return Results.BadRequest(new { error = "El Id de la historia clínica no es válido." });

                try
                {
                    var created = await service.AddRegistroAsync(id, dto);
                    return Results.Created($"/historias-clinicas/{id}/registros/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddRegistroClinico")
            .Produces<RegistroClinicoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}

