using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface IScanRepository
{
    void Add(Scan scan);

    //TODO: Done. z repo kiedy wyciągasz pojedynczy element on zawsze jest typem nullowalnym
    Task<Scan?> GetByIdAsync(ScanId id, CancellationToken cancellationToken = default);
    Task<List<Scan>> GetAllByTransportIdAsync(TransportId transportId, CancellationToken cancellationToken = default);
}
