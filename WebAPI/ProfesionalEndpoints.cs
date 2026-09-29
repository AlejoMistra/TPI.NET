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
                ProfesionalDTO createdProfesional = await profesionalService.AddAsync(profesional);
                return Results.Created($"/profesionales/{createdProfesional.Id}", createdProfesional);
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
                    throw new ArgumentException("El ID debe ser mayor a 0", nameof(id));

                ProfesionalDTO? dto = await profesionalService.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetProfesionalById")
            .Produces<ProfesionalDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPut("/profesionales/{id:int}", async (int id, ProfesionalDTO profesional, IProfesionalService profesionalService) =>
            {
                if (id != profesional.Id)
                    throw new ArgumentException("El Id de la URL no coincide con el Id del cuerpo de la solicitud.", nameof(id));

                ProfesionalDTO? updatedProfesional = await profesionalService.UpdateAsync(profesional);
                return updatedProfesional is not null ? Results.Ok(updatedProfesional) : Results.NotFound();
            })
            .WithName("UpdateProfesional")
            .Produces<ProfesionalDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/profesionales/{id:int}", async (int id, IProfesionalService profesionalService) =>
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0", nameof(id));

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
