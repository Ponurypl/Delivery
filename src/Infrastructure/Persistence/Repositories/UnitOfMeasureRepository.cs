using Microsoft.EntityFrameworkCore;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using System.Threading;

namespace MultiProject.Delivery.Infrastructure.Persistence.Repositories;
internal sealed class UnitOfMeasureRepository : IUnitOfMeasureRepository
{
    private readonly DbSet<UnitOfMeasure> _unitOfMeasures;

    public UnitOfMeasureRepository(ApplicationDbContext context)
    {
        _unitOfMeasures = context.Set<UnitOfMeasure>();
    }

    public void Add(UnitOfMeasure unitOfMeasure)
    {
        _unitOfMeasures.Add(unitOfMeasure);
    }

    public async Task<List<UnitOfMeasure>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfMeasures.ToListAsync(cancellationToken);
        // czy można wyciągnąć wszystkie w ten sposób?
    }

    public async Task<UnitOfMeasure?> GetByIdAsync(UnitOfMeasureId id, CancellationToken cancellationToken = default)
    {
        return await _unitOfMeasures.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
    }
}
