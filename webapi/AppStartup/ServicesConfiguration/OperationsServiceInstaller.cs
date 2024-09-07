using Application.Services;
using Application.Services.Admin;
using Application.Services.IServices;
using Application.Services.IServices.Admin;
using webapi.AppStartup.ServicesConfiguration._Interface;

namespace webapi.AppStartup.ServicesConfiguration
{
    public class OperationsServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IBannerService, BannerService>();
            services.AddTransient<IPagesService, PagesService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IPermissionService, PermissionService>();

        }
    }
}
