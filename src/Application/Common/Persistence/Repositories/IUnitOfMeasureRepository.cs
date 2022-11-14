using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface IUnitOfMeasureRepository
{
    void Add(UnitOfMeasure unitOfMeasure);
    Task<UnitOfMeasure> GetByIdAsync(UnitOfMeasureId id);
    Task<List<UnitOfMeasure>> GetAllAsync();
}
