using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Application.Common.Interfaces.Repositories;

public interface ITransportRepository
{
    void Add(Transport transport);
    Task<Transport> GetByIdAsync(int id);
}
