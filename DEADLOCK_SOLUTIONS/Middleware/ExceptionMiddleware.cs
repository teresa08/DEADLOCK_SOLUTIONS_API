using DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

namespace DEADLOCK_SOLUTIONS.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error no controlado en {Path}", context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(Response<object>.Error("Ocurrio un error inesperado."));
        }
    }
}
