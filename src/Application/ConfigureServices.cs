using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MultiProject.Delivery.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        return services;
    }
    
}