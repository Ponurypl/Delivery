using Microsoft.Extensions.DependencyInjection;
using MultiProject.Delivery.Application.Common.Cryptography;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Infrastructure.Cryptography;
using MultiProject.Delivery.Infrastructure.DateTimeProvider;

namespace MultiProject.Delivery.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
    {
        services.AddTransient<ICryptoService, CryptoService>();
        services.AddTransient<IHashService, HashService>();

        services.AddScoped<IDateTime, SystemDateTimeProvider>();

        return services;
    }
}