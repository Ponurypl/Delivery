using MultiProject.Delivery.Domain.Dictionaries.Entities;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface IUnitOfMeasureRepository
{
    void Add(UnitOfMeasure unitOfMeasure);
    Task<UnitOfMeasure> GetByIdAsync(int id);
    Task<List<UnitOfMeasure>> GetAllAsync();
}
