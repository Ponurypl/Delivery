using System.Reflection;

namespace MultiProject.Delivery.WebApi;

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