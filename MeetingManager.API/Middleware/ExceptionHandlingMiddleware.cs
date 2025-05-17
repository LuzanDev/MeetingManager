using MeetingManager.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace MeetingManager.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception");

            var response = context.Response;
            response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                ArgumentNullException => (HttpStatusCode.BadRequest, "Невалидные данные запроса."),
                NotFoundException => (HttpStatusCode.NotFound, exception.Message),
                DataBaseException => (HttpStatusCode.InternalServerError, exception.Message), 
                _ => (HttpStatusCode.InternalServerError, "Произошла внутренняя ошибка.")
            };

            response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error = message,
                statusCode = response.StatusCode
            });

            await response.WriteAsync(result);
        }

    }
}
