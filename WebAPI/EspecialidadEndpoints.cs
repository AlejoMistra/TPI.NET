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
                var createdDto = await especialidadService.AddAsync(especialidadDto);
                return Results.Created($"/especialidades/{createdDto.Id}", createdDto);
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
                    throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));

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
                    throw new ArgumentException("El ID en la URL no coincide con el ID en el cuerpo.", nameof(id));

                var updatedDto = await especialidadService.UpdateAsync(especialidadDto);
                return updatedDto is not null ? Results.Ok(updatedDto) : Results.NotFound();
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
                    throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));

                var deleted = await especialidadService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
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