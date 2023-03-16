using FluentValidation;
using Microsoft.Extensions.Logging;
using MultiProject.Delivery.Application.Common.Behaviors;

namespace MultiProject.Delivery.Application.Tests.Integration.Helpers;

public class ContainerSetup
{
    private readonly IServiceCollection _services;

    private ContainerSetup()
    {
        _services = new ServiceCollection();
    }

    public static ContainerSetup CreateNew()
    {
        return new ContainerSetup();
    }

    public ContainerSetup AddDefaultValidators()
    {
        _services.AddValidatorsFromAssemblyContaining(typeof(ConfigureServices));
        return this;
    }

    public ContainerSetup AddMediatR()
    {
        _services.AddMediatR(c =>
                            {
                                c.RegisterServicesFromAssemblyContaining(typeof(ConfigureServices));
                                c.AddOpenBehavior(typeof(ValidationBehavior<,>));
                            });
        return this;
    }

    public ContainerSetup AddScoped<T>(T instance) where T : class
    {
        _services.AddScoped(_ => instance);
        return this;
    }

    public ContainerSetup AddScoped<T>(Func<IServiceProvider, T> func) where T : class
    {
        _services.AddScoped(func);
        return this;
    }

    public ContainerSetup AddTransient<T>(T instance) where T : class
    {
        _services.AddTransient(_ => instance);
        return this;
    }

    public ContainerSetup AddTransient<T>(Func<IServiceProvider, T> func) where T : class
    {
        _services.AddTransient(func);
        return this;
    }

    public ContainerSetup AddLogging()
    {
        _services.AddScoped(_ => Mock.Of<ILogger>());
        _services.AddScoped(typeof(ILogger<>), typeof(LoggerMockWrapper<>));
        return this;
    }

    public ContainerSetup AddLogging(ILogger logger)
    {
        _services.AddScoped(_ => logger);
        _services.AddScoped(typeof(ILogger<>), typeof(LoggerMockWrapper<>));
        return this;
    }

    public IServiceProvider Build(bool startScope = true)
    {
        IServiceProvider provider = _services.BuildServiceProvider();
        return startScope ? provider.CreateScope().ServiceProvider : provider;
    }

}