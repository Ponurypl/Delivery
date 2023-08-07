using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MultiProject.Delivery.Infrastructure.Persistence;
using Npgsql;
using System.Data;
using Testcontainers.PostgreSql;

namespace MultiProject.Delivery.WebApi.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    public CustomWebApplicationFactory()
    {
        _container = new PostgreSqlBuilder()
                     .WithImage("postgres")
                     .WithDatabase("deliveries-db")
                     .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
                                  {
                                      services.RemoveAll<IConnectionStringProvider>();
                                      services.AddSingleton<IConnectionStringProvider>(_ =>
                                          new TestConnectionStringProvider(_container.GetConnectionString()));
                                  });
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _container.StopAsync();
    }

    public async Task SeedDatabaseAsync()
    {
        IDbConnection conn = new NpgsqlConnection(_container.GetConnectionString());

        var script = await File.ReadAllTextAsync("../../../Persistence/Users.sql");

        await conn.ExecuteAsync(script);
    }
}