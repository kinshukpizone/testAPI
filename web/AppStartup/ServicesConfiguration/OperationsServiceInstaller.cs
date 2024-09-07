using Application.Services;
using Application.Services.IServices;
using web.AppStartup.ServicesConfiguration._Interface;

namespace web.AppStartup.ServicesConfiguration
{
    public class OperationsServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<ICityService, CityService>();
        }
    }
}
