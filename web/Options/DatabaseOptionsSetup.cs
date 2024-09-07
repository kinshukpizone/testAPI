using Domain.General.OptionsModel;
using Microsoft.Extensions.Options;

namespace web.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {
            var ConnectionStrings = _configuration.GetConnectionString("AppDatebase");
            options.ConnectionStrings = ConnectionStrings!;
            _configuration.GetSection(nameof(DatabaseOptions)).Bind(options);
        }
    }
}
