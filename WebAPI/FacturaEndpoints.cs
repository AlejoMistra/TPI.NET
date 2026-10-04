using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class FacturaEndpoints
    {
        public static void MapFacturaEndpoints(this WebApplication app)
        {
            // GET: /facturas
            app.MapGet("/facturas", async (IFacturaService facturaService) =>
            {
                var facturas = await facturaService.GetAllAsync();
                return Results.Ok(facturas);
            })
            .WithName("GetAllFacturas")
            .Produces<List<FacturaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // GET: /facturas/{id:int}
            app.MapGet("/facturas/{id:int}", async (int id, IFacturaService facturaService) =>
            {
                var factura = await facturaService.GetByIdAsync(id);
                return factura is not null ? Results.Ok(factura) : Results.NotFound();
            })
            .WithName("GetFacturaById")
            .Produces<FacturaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // GET: /facturas/turno/{turnoId:int}
            app.MapGet("/facturas/turno/{turnoId:int}", async (int turnoId, IFacturaService facturaService) =>
            {
                var factura = await facturaService.GetByTurnoIdAsync(turnoId);
                return factura is not null ? Results.Ok(factura) : Results.NotFound();
            })
            .WithName("GetFacturaByTurnoId")
            .Produces<FacturaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: /facturas
            app.MapPost("/facturas", async (FacturaCreateDTO dto, IFacturaService facturaService) =>
            {
                var created = await facturaService.CreateAsync(dto);
                return Results.Created($"/facturas/{created.Id}", created);
            })
            .WithName("CreateFactura")
            .Produces<FacturaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // POST: /facturas/{id:int}/anular
            app.MapPost("/facturas/{id:int}/anular", async (int id, IFacturaService facturaService) =>
            {
                var anulada = await facturaService.AnularAsync(id);
                return Results.Ok(anulada);
            })
            .WithName("AnularFactura")
            .Produces<FacturaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();
        }
    }
}
