using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, title, detail) = MapException(exception);

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(exception, "Excepción no controlada en la solicitud {Path}: {Message}", httpContext.Request.Path, exception.Message);
            }
            else
            {
                _logger.LogWarning("Fallo de negocio/solicitud en {Path} ({StatusCode}): {Message}", httpContext.Request.Path, statusCode, exception.Message);
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };

            // En desarrollo agregamos la traza para facilitar depuración
            if (_env.IsDevelopment() && statusCode == StatusCodes.Status500InternalServerError)
            {
                problemDetails.Extensions["exception"] = exception.GetType().Name;
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
            }

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private (int statusCode, string title, string detail) MapException(Exception exception)
        {
            return exception switch
            {
                ArgumentException argEx => (
                    StatusCodes.Status400BadRequest,
                    "Error de validación",
                    argEx.Message
                ),
                InvalidOperationException opEx => (
                    StatusCodes.Status409Conflict,
                    "Conflicto en la operación",
                    opEx.Message
                ),
                KeyNotFoundException notFoundEx => (
                    StatusCodes.Status404NotFound,
                    "Recurso no encontrado",
                    notFoundEx.Message
                ),
                UnauthorizedAccessException unauthEx => (
                    StatusCodes.Status401Unauthorized,
                    "No autorizado",
                    unauthEx.Message
                ),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Error interno del servidor",
                    _env.IsDevelopment() ? exception.Message : "Ha ocurrido un error inesperado al procesar la solicitud."
                )
            };
        }
    }
}
