using Domain.General.OptionsModel;
using Domain.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using web.AppStartup.ServicesConfiguration._Interface;

namespace web.AppStartup.ServicesConfiguration
{
    public class TokenServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
                {
                    var options = configuration.GetSection(nameof(JwtConfigsOptions)).Get<JwtConfigsOptions>();
                    var key = Encoding.ASCII.GetBytes(options!.Secret);
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false, // In Dev Enviorment
                        ValidateAudience = false, // In Dev Enviorment
                        RequireExpirationTime = false, // In Dev Enviorment
                        ValidateLifetime = true,
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AppRoles.SUPERADMIN, policy => policy.RequireRole(AppRoles.SUPERADMIN));
            });
        }
    }
}
