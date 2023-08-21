using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MultiProject.Delivery.Infrastructure.Persistence;
using Npgsql;
using Respawn;
using System.Data.Common;
using Testcontainers.PostgreSql;

namespace MultiProject.Delivery.WebApi.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;
    
    private Respawner _respawner = null!;
    private DbConnection _dbConnection = null!;

    public HttpClient HttpClient { get; private set; } = null!;


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
        HttpClient = CreateClient();

        _dbConnection = new NpgsqlConnection(_container.GetConnectionString());
        await _dbConnection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_dbConnection,
                                                 new RespawnerOptions
                                                 {
                                                     DbAdapter = DbAdapter.Postgres,
                                                     SchemasToInclude = new[] { "public" }
                                                 });
    }

    public new async Task DisposeAsync()
    {
        await _container.StopAsync();
    }

    public async Task SeedDatabaseAsync()
    {
        var script = await File.ReadAllTextAsync("../../../Persistence/Users.sql");
        
        await _dbConnection.ExecuteAsync(script);
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }
}