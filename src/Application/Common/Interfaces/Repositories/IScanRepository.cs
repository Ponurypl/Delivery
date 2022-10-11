using MultiProject.Delivery.Domain.Scans.Entities;

namespace MultiProject.Delivery.Application.Common.Interfaces.Repositories;

public interface IScanRepository
{
    void Add(Scan scan);
}
