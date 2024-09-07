using Presentation.ResponseSchema;
using System.Net;

namespace web.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Global-Exception-Handling:\t" + ex.Message);

                var response = new ErrorResponse()
                {
                    Errors = new List<string> { "Internal Server Error", ex.Message },
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    SuccessMessage = null
                };

                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";
                context.Response.Headers.Add("x-web-errortype", "InternalServerError");
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
