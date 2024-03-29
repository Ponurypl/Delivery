﻿using MultiProject.Delivery.Application.Common.Persistence.Models;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface ITransportRepository
{
    //EntityFramework
    void Add(Transport transport);
    Task<List<Transport>> GetListByDateAsync(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default);
    Task<Transport?> GetByIdAsync(TransportId id, CancellationToken cancellationToken = default);

    //Dapper
    Task<TransportDbModel?> GetTransportAsync(TransportId id);
    Task<List<TransportDbModel>> GetTransportListAsync(DateTime dateFrom, DateTime dateTo);
    Task<List<int>> GetAttachmentsAsync(TransportId id, TransportUnitId? truId = null);
    Task<List<int>> GetScansAsync(TransportUnitId truId);
    Task<List<TransportUnitDbModel>> GetTransportUnitsAsync(TransportId id);
    
    
}
