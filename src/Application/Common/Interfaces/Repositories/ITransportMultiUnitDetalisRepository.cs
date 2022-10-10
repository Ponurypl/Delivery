using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Application.Common.Interfaces.Repositories;

public interface ITransportMultiUnitDetalisRepository
{
    void Add(MultiUnitDetails multiUnitDetails);
    Task<MultiUnitDetails> GetByIdAsync(int id);
}
