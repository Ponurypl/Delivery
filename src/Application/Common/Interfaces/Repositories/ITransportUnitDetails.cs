using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Application.Common.Interfaces.Repositories;

public interface ITransportUnitDetailsRepository
{
    void Add(UnitDetails unitDetails);
    Task<UnitDetails> GetByIdAsync(int id);
}
