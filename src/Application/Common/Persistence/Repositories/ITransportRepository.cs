using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface ITransportRepository
{
    void Add(Transport transport);
    Task<List<Transport>> GetListByDateAsync(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default);
    Task<Transport?> GetByIdAsync(TransportId id, CancellationToken cancellationToken = default);
}
