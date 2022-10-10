using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Application.Common.Interfaces.Repositories;

public interface ITransportUniqueUnitDetalisRepository
{
    void Add(UniqueUnitDetails uniqueUnitDetails);
    Task<UniqueUnitDetails> GetByIdAsync(int id);
}
