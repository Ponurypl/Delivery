using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface IUnitOfMeasureRepository
{
    void Add(UnitOfMeasure unitOfMeasure);
    //TODO: Done. Nullowalny obiekt z bazy
    Task<UnitOfMeasure?> GetByIdAsync(UnitOfMeasureId id, CancellationToken cancellationToken = default);
    Task<List<UnitOfMeasure>> GetAllAsync(CancellationToken cancellationToken = default);
}
