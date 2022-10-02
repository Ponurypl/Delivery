namespace MultiProject.Delivery.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
