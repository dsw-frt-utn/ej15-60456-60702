using System.Text.Json;

namespace Dsw2026Ej15.Api.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next(context);
            }
            catch (Dsw2026Ej15.Domain.Exceptions.ValidationException ex)
            {

                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Ha ocurrido un problema interno en el servidor.");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;


            var response = new
            {
                Error = message
            };


            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
