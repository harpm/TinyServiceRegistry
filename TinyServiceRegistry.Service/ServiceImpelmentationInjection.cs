
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TinyServiceRegistry.Service.PanelService.AccountServices;
using TinyServiceRegistry.Service.PanelService.Instance;
using TinyServiceRegistry.Service.PanelService.Service;

namespace TinyServiceRegistry.Service;

public static class ServiceImpelmentationInjection
{
    public static IServiceCollection InjectTSRServices(this IServiceCollection services)
    {
        services.AddScoped<IConfigurationProvider>(sp =>
        {
            return new MapperConfiguration(config =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                config.AddProfiles(assemblies
                    .SelectMany(x => x.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(Profile)) && !t.IsAbstract)
                        .Select(t => t.GetConstructors().First().Invoke(null) as Profile)
                        .ToArray()));
            });
        });

        services.AddScoped(sp =>
        {
            return new Mapper(sp.GetService<AutoMapper.MapperConfiguration>(), sp.GetService);
        });

        services.AddScoped<IMapper>(sp =>
        {
            return new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService);
        });

        services.AddScoped<TSRAdminAccountService>();
        services.AddScoped<TSRServiceAccountService>();
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IServiceInstanceService, ServiceInstanceService>();
        return services;
    }
}
