using Microsoft.EntityFrameworkCore;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Repositories;
internal sealed class ScanRepository : IScanRepository
{
    private readonly DbSet<Scan> _scans;

    public ScanRepository(ApplicationDbContext context)
    {
        _scans = context.Set<Scan>();
    }

    public void Add(Scan scan)
    {
        _scans.Add(scan);
    }

    public async Task<List<Scan>> GetAllByTransportUnitIdAsync(TransportUnitId transportUnitId, CancellationToken cancellationToken = default)
    {
        return await _scans.AsNoTracking()
                           .Where(s => s.TransportUnitId == transportUnitId)
                           .ToListAsync(cancellationToken);
    }

    public async Task<Scan?> GetByIdAsync(ScanId id, CancellationToken cancellationToken = default)
    {
        return await _scans.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}
