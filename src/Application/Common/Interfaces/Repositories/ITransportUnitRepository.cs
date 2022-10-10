using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.Entities;

namespace MultiProject.Delivery.Application.Common.Interfaces.Repositories;

public interface ITransportUnitRepository
{
    void Add(TransportUnit transportUnit);
    Task<TransportUnit> GetByIdAsync(int id);
}
