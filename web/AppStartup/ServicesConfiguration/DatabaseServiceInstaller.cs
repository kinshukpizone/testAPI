using Domain.Entities.Account;
using Domain.General.OptionsModel;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using web.AppStartup.ServicesConfiguration._Interface;

namespace web.AppStartup.ServicesConfiguration
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var data = "";

            var builder = services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(ApplicationRole), services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.AddSignInManager<SignInManager<ApplicationUser>>();

            services.AddDbContext<ApplicationDbContext>(
                (serviceProvider, dbContextOptionsBuilder) =>
                {
                    var DatabaseOption = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
                    data = DatabaseOption.ConnectionStrings;
                    dbContextOptionsBuilder.UseSqlServer(DatabaseOption.ConnectionStrings, sqlServerOptionsAction =>
                    {
                        sqlServerOptionsAction.EnableRetryOnFailure(DatabaseOption.MaxRetryCount);
                        sqlServerOptionsAction.CommandTimeout(DatabaseOption.CommandTimeout);
                    });

                    dbContextOptionsBuilder.EnableDetailedErrors(DatabaseOption.EnableDetailedErrors);
                    dbContextOptionsBuilder.EnableSensitiveDataLogging(DatabaseOption.EnableSensitiveDataLoggin);
                });
        }
    }
}
