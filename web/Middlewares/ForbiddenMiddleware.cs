using Presentation.ResponseSchema;
using System.Net;

namespace web.Middlewares
{
    public class ForbiddenMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // TODO: Modify the forbidden response as needed
            await next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                // Unauthorized: Access is denied due to invalid credential!, Authentication Required
                var response = new FailedAuthResponse()
                {
                    Errors = new List<string> { "Unauthorized: User is not authorized to access this resource with an explicit deny" },
                    IsAuthenticate = false,
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    SuccessMessage = null
                };

                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";
                context.Response.Headers.Add("x-web-errortype", "AccessDeniedException");
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
