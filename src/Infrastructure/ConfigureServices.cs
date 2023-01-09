using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MultiProject.Delivery.Application.Common.Cryptography;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Infrastructure.Configurations;
using MultiProject.Delivery.Infrastructure.Cryptography;
using MultiProject.Delivery.Infrastructure.DateTimeProvider;
using MultiProject.Delivery.Infrastructure.Persistence;
using MultiProject.Delivery.Infrastructure.Persistence.Repositories;

namespace MultiProject.Delivery.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptionsWithCryptoService<ConnectionStringDetails>(configuration.GetSection("Database:Postgres"));
        services.AddSingleton<IConnectionStringProvider, PgConnectionStringProvider>();

        services.AddTransient<ICryptoService, CryptoService>();
        services.AddTransient<IHashService, HashService>();

        services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IScanRepository, ScanRepository>();
        services.AddScoped<ITransportRepository, TransportRepository>();
        services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IDateTime, SystemDateTimeProvider>();

        services.AddDbContext<ApplicationDbContext>();
        
        return services;
    }

    internal static IServiceCollection ConfigureOptionsWithCryptoService<TOptions>(
        this IServiceCollection services, IConfigurationSection configurationSection)
        where TOptions : class, IOptionsWithCryptoService
    {
        services.Configure<TOptions>(configurationSection);
        services.TryAddSingleton<IConfigureOptions<TOptions>, ConfigureOptionsWithCryptoService>();

        return services;
    }
}