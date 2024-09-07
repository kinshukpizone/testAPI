using Application.General.IPersistence;
using Infrastructure.Persistence;
using web.AppStartup.ServicesConfiguration._Interface;

namespace web.AppStartup.ServicesConfiguration
{
    public class RepositoryWrapperServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
