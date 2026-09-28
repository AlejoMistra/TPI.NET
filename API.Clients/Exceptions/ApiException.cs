using System.Net;

namespace API.Clients.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode? StatusCode { get; }
        public string? Title { get; }
        public string? Detail { get; }
        public IDictionary<string, string[]>? Errors { get; }

        public ApiException(
            string message,
            HttpStatusCode? statusCode = null,
            string? title = null,
            string? detail = null,
            IDictionary<string, string[]>? errors = null,
            Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Title = title;
            Detail = detail;
            Errors = errors;
        }
    }

    public class ValidationApiException : ApiException
    {
        public ValidationApiException(
            string message,
            string? title = null,
            string? detail = null,
            IDictionary<string, string[]>? errors = null)
            : base(message, HttpStatusCode.BadRequest, title, detail, errors)
        {
        }
    }

    public class NotFoundApiException : ApiException
    {
        public NotFoundApiException(string message, string? title = null, string? detail = null)
            : base(message, HttpStatusCode.NotFound, title, detail)
        {
        }
    }

    public class ConflictApiException : ApiException
    {
        public ConflictApiException(string message, string? title = null, string? detail = null)
            : base(message, HttpStatusCode.Conflict, title, detail)
        {
        }
    }

    public class UnauthorizedApiException : ApiException
    {
        public UnauthorizedApiException(string message = "Su sesión ha expirado o no tiene autorización para realizar esta acción.")
            : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }

    public class NetworkApiException : ApiException
    {
        public NetworkApiException(string message = "No se pudo establecer conexión con el servidor. Verifique si el servicio está activo o compruebe su conexión de red.", Exception? innerException = null)
            : base(message, null, null, null, null, innerException)
        {
        }
    }
}
