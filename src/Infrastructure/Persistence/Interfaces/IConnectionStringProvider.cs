namespace MultiProject.Delivery.Infrastructure.Persistence;

internal interface IConnectionStringProvider
{
    string GetConnectionString();
}