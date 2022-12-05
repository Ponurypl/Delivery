using Microsoft.Extensions.DependencyInjection;
using MultiProject.Delivery.Application.Common.Cryptography;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Infrastructure.Cryptography;
using MultiProject.Delivery.Infrastructure.DateTimeProvider;
using MultiProject.Delivery.Infrastructure.Persistence;
using MultiProject.Delivery.Infrastructure.Persistence.Repositories;

namespace MultiProject.Delivery.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
    {
        services.AddTransient<ICryptoService, CryptoService>();
        services.AddTransient<IHashService, HashService>();

        services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IScanRepository, ScanRepository>();
        services.AddScoped<ITransportRepository, TransportRepository>();
        services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDateTime, SystemDateTimeProvider>();

        return services;
    }
}