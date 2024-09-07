using System.Reflection;
using web.AppStartup.ServicesConfiguration._Interface;

namespace web.AppStartup
{
    public static class DependencyInjection
    {
        public static IServiceCollection InstallService(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            IEnumerable<IServiceInstaller> servicesInstallers = assemblies
                .SelectMany(assembly => assembly.DefinedTypes)
                .Where(IsAssignableToType<IServiceInstaller>)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>();

            foreach (var serviceInstaller in servicesInstallers)
            {
                serviceInstaller.Install(services, configuration);
            }

            static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
                typeof(T).IsAssignableFrom(typeInfo) &&
                !typeInfo.IsInterface &&
                !typeInfo.IsAbstract;

            return services;
        }

        //public static IWebHostEnvironment InstallHostingEnvironment(this IWebHostEnvironment webHostEnvironment, params Assembly[] assemblies)
        //{
        //    IEnumerable<IHostingInstaller> hostingInstallers = assemblies
        //        .SelectMany(assembly => assembly.DefinedTypes)
        //        .Where(IsAssignableToType<IHostingInstaller>)
        //        .Select(Activator.CreateInstance)
        //        .Cast<IHostingInstaller>();

        //    foreach (var hostingInstaller in hostingInstallers)
        //    {
        //        hostingInstaller.Install(webHostEnvironment);
        //    }

        //    static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
        //         typeof(T).IsAssignableFrom(typeInfo) &&
        //         !typeInfo.IsInterface &&
        //         !typeInfo.IsAbstract;

        //    return webHostEnvironment;
        //}
    }
}
