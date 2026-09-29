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
                var historia = await service.GetByIdAsync(id);
                return historia is not null ? Results.Ok(historia) : Results.NotFound();
            })
            .WithName("GetHistoriaClinicaById")
            .Produces<HistoriaClinicaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // GET: /historias-clinicas/paciente/{pacienteId}
            app.MapGet("/historias-clinicas/paciente/{pacienteId:int}", async (int pacienteId, IHistoriaClinicaService service) =>
            {
                var historia = await service.GetByPacienteIdAsync(pacienteId);
                return historia is not null ? Results.Ok(historia) : Results.NotFound();
            })
            .WithName("GetHistoriaClinicaByPacienteId")
            .Produces<HistoriaClinicaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /historias-clinicas
            app.MapPost("/historias-clinicas", async (HistoriaClinicaCreateDTO dto, IHistoriaClinicaService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/historias-clinicas/{created.Id}", created);
            })
            .WithName("CreateHistoriaClinica")
            .Produces<HistoriaClinicaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // PUT: /historias-clinicas/{id}/grupo-sanguineo
            app.MapPut("/historias-clinicas/{id:int}/grupo-sanguineo", async (int id, HistoriaClinicaUpdateGrupoDTO dto, IHistoriaClinicaService service) =>
            {
                var updated = await service.UpdateGrupoSanguineoAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateGrupoSanguineo")
            .Produces<HistoriaClinicaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /historias-clinicas/{id}/registros
            app.MapPost("/historias-clinicas/{id:int}/registros", async (int id, RegistroClinicoCreateDTO dto, IHistoriaClinicaService service) =>
            {
                var created = await service.AddRegistroAsync(id, dto);
                return Results.Created($"/historias-clinicas/{id}/registros/{created.Id}", created);
            })
            .WithName("AddRegistroClinico")
            .Produces<RegistroClinicoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();
        }
    }
}
