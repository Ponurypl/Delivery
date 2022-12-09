using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MultiProject.Delivery.Infrastructure.Persistence;

internal sealed class ApplicationDbContext : DbContext
{
    private readonly IConnectionStringProvider _connectionStringProvider;
    
    public ApplicationDbContext(DbContextOptions options, IConnectionStringProvider connectionStringProvider)
        : base(options)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionStringProvider.GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}