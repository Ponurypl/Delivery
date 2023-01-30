using MultiProject.Delivery.WebApi.v1.Auth.Services;
using System.Reflection;

namespace MultiProject.Delivery.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiLayerServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        TypeAdapterConfig.GlobalSettings.Compile();
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddDistributedMemoryCache();
        
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}