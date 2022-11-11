namespace MultiProject.Delivery.Application.Common.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
