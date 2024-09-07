using Application.General.MappingProfiles;
using AutoMapper;
using webapi.AppStartup.ServicesConfiguration._Interface;

namespace webapi.AppStartup.ServicesConfiguration
{
    public class MappingServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var MappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AccountProfile());
            });

            var Mapper = MappingConfig.CreateMapper();
            services.AddSingleton<IMapper>(Mapper);
        }
    }
}
