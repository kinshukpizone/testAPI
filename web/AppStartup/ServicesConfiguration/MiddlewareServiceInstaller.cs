using web.AppStartup.ServicesConfiguration._Interface;
using web.Middlewares;

namespace web.AppStartup.ServicesConfiguration
{
    public class MiddlewareServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<JwtUnauthorizedMiddleware>();
            services.AddTransient<ForbiddenMiddleware>();
            services.AddTransient<GlobalExceptionHandlingMiddleware>();
        }
    }
}
