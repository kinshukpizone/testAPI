namespace web.AppStartup.ServicesConfiguration._Interface
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
