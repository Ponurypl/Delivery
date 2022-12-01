using Microsoft.EntityFrameworkCore;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Repositories;
internal sealed class TransportRepository : ITransportRepository
{
    private readonly DbSet<Transport> _transports;
    public TransportRepository(ApplicationDbContext context)
    {
        _transports = context.Set<Transport>();
    }

    public void Add(Transport transport)
    {
        _transports.Add(transport);
        //To chyba nie będzie tak proste?
    }

    public async Task<Transport?> GetByIdAsync(TransportId id, CancellationToken cancellationToken = default)
    {
        return await _transports.FirstAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Transport>> GetListByDateAsync(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default)
    {
        return await _transports.Where(t => dateFrom <= t.CreationDate && t.CreationDate <= dateTo).ToListAsync(cancellationToken);
    }
}
