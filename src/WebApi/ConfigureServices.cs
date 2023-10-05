using MultiProject.Delivery.WebApi.v1.Auth.Services;
using System.Reflection;

namespace MultiProject.Delivery.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiLayerServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        TypeAdapterConfig.GlobalSettings.Compile();
        
        ValidatorOptions.Global.LanguageManager.Enabled = false;

        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddDistributedMemoryCache();
        services.AddHttpClient();
        
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}