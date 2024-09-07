using Application.General.IPersistence;
using Infrastructure.Persistence;
using webapi.AppStartup.ServicesConfiguration._Interface;

namespace webapi.AppStartup.ServicesConfiguration
{
    public class RepositoryWrapperServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
