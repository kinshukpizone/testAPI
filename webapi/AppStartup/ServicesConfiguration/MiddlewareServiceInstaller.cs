using webapi.AppStartup.ServicesConfiguration._Interface;
using webapi.Middlewares;

namespace webapi.AppStartup.ServicesConfiguration
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
