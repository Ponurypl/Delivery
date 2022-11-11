using MultiProject.Delivery.Domain.Scans.Entities;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface IScanRepository
{
    void Add(Scan scan);
}
