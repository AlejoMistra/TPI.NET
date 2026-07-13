using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class EspecialidadEndpoints
    {
        public static void MapEspecialidadEndpoints(this WebApplication app)
        {
            app.MapGet("/especialidades", async () =>
            {
                EspecialidadService paisService = new EspecialidadService();
                var dtos = await especialidadService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllEspecialidades")
            .Produces<List<EspecialidadDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();
        }
    }
}
