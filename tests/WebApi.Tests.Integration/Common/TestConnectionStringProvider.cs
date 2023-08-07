using MultiProject.Delivery.Infrastructure.Persistence;

namespace MultiProject.Delivery.WebApi.Tests.Integration.Common;

public class TestConnectionStringProvider : IConnectionStringProvider
{
    private readonly string _connectionString;

    public TestConnectionStringProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public string GetConnectionString() => _connectionString;
}