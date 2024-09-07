using Application.General.MappingProfiles;
using AutoMapper;
using web.AppStartup.ServicesConfiguration._Interface;

namespace web.AppStartup.ServicesConfiguration
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
