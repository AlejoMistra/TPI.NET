using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class TurnoEndpoints
    {
        public static void MapTurnoEndpoints(this WebApplication app)
        {
            // GET: /turnos
            app.MapGet("/turnos", async (ITurnoService turnoService) =>
            {
                var turnos = await turnoService.GetAllAsync();
                return Results.Ok(turnos);
            })
            .WithName("GetAllTurnos")
            .Produces<List<TurnoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // GET: /turnos/{id:int}
            app.MapGet("/turnos/{id:int}", async (int id, ITurnoService turnoService) =>
            {
                var turno = await turnoService.GetByIdAsync(id);
                return turno is not null ? Results.Ok(turno) : Results.NotFound();
            })
            .WithName("GetTurnoById")
            .Produces<TurnoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /turnos
            app.MapPost("/turnos", async (TurnoDTO turnoDto, ITurnoService turnoService) =>
            {
                var createdTurno = await turnoService.AddAsync(turnoDto);
                return Results.Created($"/turnos/{createdTurno.Id}", createdTurno);
            })
            .WithName("CreateTurno")
            .Produces<TurnoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // PUT: /turnos/{id:int}
            app.MapPut("/turnos/{id:int}", async (int id, TurnoDTO turnoDto, ITurnoService turnoService) =>
            {
                if (id != turnoDto.Id)
                    throw new ArgumentException("El Id de la URL no coincide con el Id del cuerpo de la solicitud.", nameof(id));

                var updatedTurno = await turnoService.UpdateAsync(turnoDto);
                return updatedTurno is not null ? Results.Ok(updatedTurno) : Results.NotFound();
            })
            .WithName("UpdateTurno")
            .Produces<TurnoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // DELETE: /turnos/{id:int}
            app.MapDelete("/turnos/{id:int}", async (int id, ITurnoService turnoService) =>
            {
                var deleted = await turnoService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteTurno")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /turnos/{id:int}/asignar
            app.MapPost("/turnos/{id:int}/asignar", async (int id, AsignarTurnoRequestDTO request, ITurnoService turnoService) =>
            {
                var turno = await turnoService.AsignarAsync(id, request.PacienteId, request.Motivo, request.Observaciones);
                return Results.Ok(turno);
            })
            .WithName("AsignarTurno")
            .Produces<TurnoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /turnos/{id:int}/liberar
            app.MapPost("/turnos/{id:int}/liberar", async (int id, ITurnoService turnoService) =>
            {
                var turno = await turnoService.LiberarAsync(id);
                return Results.Ok(turno);
            })
            .WithName("LiberarTurno")
            .Produces<TurnoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /turnos/{id:int}/revertir-llegada
            app.MapPost("/turnos/{id:int}/revertir-llegada", async (int id, ITurnoService turnoService) =>
            {
                var turno = await turnoService.RevertirLlegadaAsync(id);
                return Results.Ok(turno);
            })
            .WithName("RevertirLlegadaTurno")
            .Produces<TurnoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /turnos/{id:int}/llegada
            app.MapPost("/turnos/{id:int}/llegada", async (int id, ITurnoService turnoService) =>
            {
                var turno = await turnoService.RegistrarLlegadaAsync(id);
                return Results.Ok(turno);
            })
            .WithName("RegistrarLlegadaTurno")
            .Produces<TurnoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /turnos/{id:int}/ausente
            app.MapPost("/turnos/{id:int}/ausente", async (int id, ITurnoService turnoService) =>
            {
                var turno = await turnoService.MarcarAusenteAsync(id);
                return Results.Ok(turno);
            })
            .WithName("MarcarAusenteTurno")
            .Produces<TurnoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /turnos/{id:int}/atender
            app.MapPost("/turnos/{id:int}/atender", async (int id, ITurnoService turnoService) =>
            {
                var turno = await turnoService.AtenderAsync(id);
                return Results.Ok(turno);
            })
            .WithName("AtenderTurno")
            .Produces<TurnoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
