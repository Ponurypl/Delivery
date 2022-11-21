using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface ITransportRepository
{
    void Add(Transport transport);
    Task<Transport?> GetByIdAsync(TransportId id, CancellationToken cancellationToken = default);
}
