using Mapster;
using MapsterMapper;
using System.Reflection;

namespace WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiLayerServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}