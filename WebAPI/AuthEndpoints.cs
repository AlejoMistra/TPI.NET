using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            // POST: /auth/login
            app.MapPost("/auth/login", async (LoginRequest request, IConfiguration configuration) =>
            {
                AuthService authService = new AuthService(configuration);

                LoginResponse? response = await authService.LoginAsync(request);

                if (response == null)
                {
                    // Credenciales invalidas, usuario inexistente o inactivo:
                    // siempre la misma respuesta, para no revelar cual de los tres fue.
                    return Results.Unauthorized();
                }

                return Results.Ok(response);
            })
            .WithName("Login")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .AllowAnonymous()
            .WithOpenApi();
        }
    }
}
