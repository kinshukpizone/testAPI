using Domain.General.OptionsModel;
using Microsoft.Extensions.Options;

namespace web.Options
{
    public class JwtConfigsOptionsSetup : IConfigureOptions<JwtConfigsOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtConfigsOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtConfigsOptions options)
        {
            _configuration.GetSection(nameof(JwtConfigsOptions)).Bind(options);
        }
    }
}
