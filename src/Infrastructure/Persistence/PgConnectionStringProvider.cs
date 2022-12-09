using Microsoft.Extensions.Options;
using MultiProject.Delivery.Infrastructure.Configurations;

namespace MultiProject.Delivery.Infrastructure.Persistence;

internal sealed class PgConnectionStringProvider : IConnectionStringProvider
{
    private readonly string _connectionString;

    public PgConnectionStringProvider(IOptions<ConnectionStringDetails> detailsOptions)
    {
        _connectionString =
            $"{detailsOptions.Value.ConnectionString.TrimEnd(';')};User Id={detailsOptions.Value.Username};Password={detailsOptions.Value.Password}";
    }

    public string GetConnectionString() => _connectionString;
}