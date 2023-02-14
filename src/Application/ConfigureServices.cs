using MediatR;
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

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TraceLogBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
    
}