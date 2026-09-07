using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (LoginRequest request, AuthService authService) =>
            {
                var response = await authService.LoginAsync(request);

                if (response == null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(response);
            })
            .WithName("Login")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi()
            .AllowAnonymous();
        }
    }
}
