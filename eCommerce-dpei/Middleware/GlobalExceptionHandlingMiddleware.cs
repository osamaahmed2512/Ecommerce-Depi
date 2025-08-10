using Ecommerce.Application.Exceptions;
using FluentValidation;

namespace eCommerce_dpei.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env; 
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Not Found Exception");
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
            }



            catch (Exception ex)
            {
                var method = context.Request.Method;
                var path = context.Request.Path;
                var endpoint = context.GetEndpoint()?.DisplayName ?? "Unknown endpoint";

                _logger.LogError(ex,
                    "Unhandled exception occurred.\nMethod: {Method}\nPath: {Path}\nEndpoint: {Endpoint}\nException: {Message}",
                    method, path, endpoint, ex.Message);

                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var errorResponse = _env.IsDevelopment()
                        ? new { error = ex.Message, stackTrace = ex.StackTrace }
                        : new { error = "An unexpected error occurred.", stackTrace = (string?)null };

                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            }
        }
    }
}
