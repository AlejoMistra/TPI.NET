using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ProfesionalEndpoints
    {
        public static void MapProfesionalEndpoints(this WebApplication app)
        {
            app.MapGet("/profesionales", async (IProfesionalService profesionalService) =>
            {
                var dtos = await profesionalService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllProfesionales")
            .Produces<List<ProfesionalDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();
        }
    }
}
