using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class EspecialidadEndpoints
    {
        public static void MapEspecialidadEndpoints(this WebApplication app)
        {
            // POST: /especialidades
            app.MapPost("/especialidades", async (EspecialidadDTO especialidadDto, IEspecialidadService especialidadService) =>
            {
                try
                {
                    var createdDto = await especialidadService.AddAsync(especialidadDto);
                    return Results.Created($"/especialidades/{createdDto.Id}", createdDto);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("CreateEspecialidad")
            .Produces<EspecialidadDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            // GET: /especialidades
            app.MapGet("/especialidades", async (IEspecialidadService especialidadService) =>
            {
                var dtos = await especialidadService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllEspecialidades")
            .Produces<List<EspecialidadDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // GET: /especialidades/{id}
            app.MapGet("/especialidades/{id:int}", async (int id, IEspecialidadService especialidadService) =>
            {
                if (id <= 0)
                    return Results.BadRequest(new { error = "El ID debe ser un número positivo." });

                var dto = await especialidadService.GetByIdAsync(id);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetEspecialidadById")
            .Produces<EspecialidadDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // PUT: /especialidades/{id}
            app.MapPut("/especialidades/{id:int}", async (int id, EspecialidadDTO especialidadDto, IEspecialidadService especialidadService) =>
            {
                if (id != especialidadDto.Id)
                    return Results.BadRequest(new { error = "El ID en la URL no coincide con el ID en el cuerpo." });

                try
                {
                    var updatedDto = await especialidadService.UpdateAsync(especialidadDto);
                    return updatedDto is not null ? Results.Ok(updatedDto) : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateEspecialidad")
            .Produces<EspecialidadDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // DELETE: /especialidades/{id}
            app.MapDelete("/especialidades/{id:int}", async (int id, IEspecialidadService especialidadService) =>
            {
                if (id <= 0)
                    return Results.BadRequest(new { error = "El ID debe ser un número positivo." });

                try
                {
                    var deleted = await especialidadService.DeleteAsync(id);
                    return deleted ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { error = ex.Message });
                }
            })
            .WithName("DeleteEspecialidad")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();
        }
    }
}