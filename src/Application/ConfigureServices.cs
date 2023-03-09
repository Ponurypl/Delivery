using Microsoft.Extensions.DependencyInjection;
using MultiProject.Delivery.Application.Common.Behaviors;
using System.Reflection;

namespace MultiProject.Delivery.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
                            {
                                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
                                cfg.AddOpenBehavior(typeof(TraceLogBehavior<,>));
                                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                            });

        return services;
    }
    
}