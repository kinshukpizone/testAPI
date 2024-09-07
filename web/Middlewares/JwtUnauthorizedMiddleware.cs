using Presentation.ResponseSchema;
using System.Net;

namespace web.Middlewares
{
    public class JwtUnauthorizedMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // TODO: Modify the unauthorized response as needed
            await next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                // Unauthorized: Access is denied due to invalid credential!, Authentication Required
                var response = new FailedAuthResponse()
                {
                    Errors = new List<string> { "Unauthorized: Access is denied!, Authentication Required" },
                    IsAuthenticate = false,
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    SuccessMessage = null
                };

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                context.Response.Headers.Add("x-web-errortype", "AccessDeniedException");
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
