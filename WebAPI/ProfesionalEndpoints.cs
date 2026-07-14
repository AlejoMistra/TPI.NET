using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ProfesionalEndpoints
    {
        public static void MapProfesionalEndpoints(this WebApplication app)
        {
            app.MapPost("/profesionales", async (ProfesionalDTO profesional, IProfesionalService profesionalService) =>
            {
                try
                {
                    ProfesionalDTO createdProfesional = await profesionalService.AddAsync(profesional);
                    return Results.Created($"/profesionales/{createdProfesional.Id}", createdProfesional);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            })
            .WithName("AddProfesional")
            .Produces<ProfesionalDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapGet("/profesionales", async (IProfesionalService profesionalService) =>
            {
                IEnumerable<ProfesionalDTO> dtos = await profesionalService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllProfesionales")
            .Produces<List<ProfesionalDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/profesionales/{id:int}", async (int id, IProfesionalService profesionalService) =>
            {
                if (id <= 0)
                {
                    return Results.BadRequest(new { message = "El ID debe ser mayor a 0" });
                }

                ProfesionalDTO? dto = await profesionalService.GetByIdAsync(id);
                return dto is null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetProfesionalById")
            .Produces<ProfesionalDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPut("/profesionales/{id:int}", async (int id, ProfesionalDTO profesional, IProfesionalService profesionalService) =>
            {
                if (id != profesional.Id)
                {
                    return Results.BadRequest(new { message = "ID in the URL does not match ID in the body." });
                }

                try
                {
                    ProfesionalDTO? updatedProfesional = await profesionalService.UpdateAsync(profesional);
                    return updatedProfesional is null ? Results.NotFound() : Results.Ok(updatedProfesional);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            })
            .WithName("UpdateProfesional")
            .Produces<ProfesionalDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/profesionales/{id:int}", async (int id, IProfesionalService profesionalService) =>
            {
                if (id <= 0)
                {
                    return Results.BadRequest(new { message = "El ID debe ser mayor a 0" });
                }

                bool deleted = await profesionalService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteProfesional")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
