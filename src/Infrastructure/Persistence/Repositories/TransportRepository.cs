﻿using Microsoft.EntityFrameworkCore;
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
    }

    public async Task<Transport?> GetByIdAsync(TransportId id, CancellationToken cancellationToken = default)
    {
        return await _transports.Where(t => t.Id == id)
                                .Include(x => x.TransportUnits)
                                .ThenInclude(x => x.UniqueUnitDetails)
                                .Include(x => x.TransportUnits)
                                .ThenInclude(x => x.MultiUnitDetails)  
                                .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Transport>> GetListByDateAsync(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default)
    {
        return await _transports.AsNoTracking()
                                .Where(t => dateFrom.ToUniversalTime() <= t.CreationDate && t.CreationDate <= dateTo.ToUniversalTime())
                                .ToListAsync(cancellationToken);
    }
}
