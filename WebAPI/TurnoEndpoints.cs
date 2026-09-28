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

      app.MapGet("/turnos/{id:int}", async (int id, ITurnoService turnoService) =>
      {
        if (id <= 0)
          return Results.BadRequest(new { error = "El Id del turno no es válido." });

        var turno = await turnoService.GetByIdAsync(id);
        if (turno == null)
          return Results.NotFound();
        return Results.Ok(turno);
      })
      .WithName("GetTurnoById")
      .Produces<TurnoDTO>(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status404NotFound)
      .WithOpenApi();

      // POST: /turnos
      app.MapPost("/turnos", async (TurnoDTO turnoDto, ITurnoService turnoService) =>
      {
        try
        {
          var createdTurno = await turnoService.AddAsync(turnoDto);
          return Results.Created($"/turnos/{createdTurno.Id}", createdTurno);
        }
        catch (ArgumentException ex)
        {
          return Results.BadRequest(new { error = ex.Message });
        }
      })
      .WithName("CreateTurno")
      .Produces<TurnoDTO>(StatusCodes.Status201Created)
      .Produces(StatusCodes.Status400BadRequest)
      .WithOpenApi();

      // PUT: /turnos/{id:int}
      app.MapPut("/turnos/{id:int}", async (int id, TurnoDTO turnoDto, ITurnoService turnoService) =>
      {
        if (id != turnoDto.Id)
          return Results.BadRequest(new { error = "El Id del turno no coincide con el Id en el cuerpo de la solicitud." });

        try
        {
          var updatedTurno = await turnoService.UpdateAsync(turnoDto);
          return updatedTurno is not null ? Results.Ok(updatedTurno) : Results.NotFound();
        }
        catch (ArgumentException ex)
        {
          return Results.BadRequest(new { error = ex.Message });
        }
      })
      .WithName("UpdateTurno")
      .Produces<TurnoDTO>(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status404NotFound)
      .WithOpenApi();

      // DELETE: /turnos/{id:int}
      app.MapDelete("/turnos/{id:int}", async (int id, ITurnoService turnoService) =>
      {
        if (id <= 0)
          return Results.BadRequest(new { error = "El Id del turno no es válido." });

        try
        {
          var deleted = await turnoService.DeleteAsync(id);
          return deleted ? Results.NoContent() : Results.NotFound();
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
      .WithName("DeleteTurno")
      .Produces(StatusCodes.Status204NoContent)
      .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status404NotFound)
      .WithOpenApi();
    }
  }
}
