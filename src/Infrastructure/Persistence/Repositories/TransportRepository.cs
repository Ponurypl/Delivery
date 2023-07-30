using Dapper;
using Microsoft.EntityFrameworkCore;
using MultiProject.Delivery.Application.Common.Persistence.Models;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using System.Data;

namespace MultiProject.Delivery.Infrastructure.Persistence.Repositories;


internal sealed class TransportRepository : ITransportRepository
{
    private readonly DbSet<Transport> _transports;
    private readonly IDbConnection _connection;
    public TransportRepository(ApplicationDbContext context)
    {
        _transports = context.Set<Transport>();
        _connection = context.Connection;
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

    public async Task<TransportDbModel?> GetTransportAsync(TransportId id)
    {
        const string query = """
                    select 
                        t.transport_id as Id, 
                        t.deliverer_id as DelivererId, 
                        t.status, 
                        t."number" ,
                        t.additional_information as AdditionalInformation, 
                        t.total_weight as TotalWeight,
                        t.creation_date as CreationDate, 
                        t.start_date as StartDate, 
                        t.manager_id as ManagerId
                    from 
                        transports t 
                    where 
                        t.transport_id = :id
                    """;

        TransportDbModel? result = await _connection.QueryFirstOrDefaultAsync<TransportDbModel>(query, new { id = id.Value });
        
        return result;
    }

    public async Task<List<int>> GetAttachmentsAsync(TransportId id, TransportUnitId? truId = null)
    {
        string query = """
                    select 
                        a.attachment_id
                    from 
                        attachments a
                    where 
                        a.transport_id = :id
                    """;
        
        DynamicParameters parameters = new();
        parameters.Add("id", id.Value);

        if (truId.HasValue)
        {
            query += """

                        and a.transport_unit_id = :truId
                     """;
            parameters.Add("truId", truId.Value.Value);
        }

        IEnumerable<int>? result = await _connection.QueryAsync<int>(query, parameters);
        return result.AsList();
    }

    public async Task<List<int>> GetScansAsync(TransportUnitId truId)
    {
                const string query = """
                    select 
                        s.scan_id
                    from 
                         scans s 
                    where 
                        s.transport_unit_id = :id
                    """;
        IEnumerable<int>? result = await _connection.QueryAsync<int>(query, new { id = truId.Value });
        return result.AsList();
    }

    public async Task<List<TransportUnitDbModel>> GetTransportUnitsAsync(TransportId id)
    {
        const string query = """
                    select 
                        tu.transport_unit_id as Id, 
                        tu."number", 
                        tu.description, 
                        tu.status, 
                        tu.additional_information as AdditionalInformation, 
                        uud.barcode, 
                        mud.amount, 
                        mud.unit_of_measure_id as UnitOfMeasureId,
                        '' as SPLIT_RE, -- recipient
                        tu.recipient_company_name as CompanyName, 
                        tu.recipient_name as "Name",
                        tu.recipient_last_name as LastName, 
                        tu.recipient_phone_number as PhoneNumber,
                        tu.recipient_flat_number as FlatNumber, 
                        tu.recipient_street_number as StreetNumber,
                        tu.recipient_street as Street, 
                        tu.recipient_town as Town, 
                        tu.recipient_country as Country, 
                        tu.recipient_post_code as PostCode
                    from 
                        transports t 
                        join transport_units tu on 
                            t.transport_id = tu.transport_id
                        left join unique_units_details uud on 
                            tu.unique_unit_id = uud.unique_unit_id
                        left join multi_units_details mud on 
                            tu.multi_unit_id = mud.multi_unit_id
                    where
                        t.transport_id = :id
                    """;
        IEnumerable<TransportUnitDbModel>? result =
            await _connection.QueryAsync<TransportUnitDbModel, RecipientDbModel, TransportUnitDbModel>(query,
                (tu, r) =>
                {
                    tu.Recipient = r;
                    return tu;
                }, new { id = id.Value }, splitOn: "SPLIT_RE");

        return result.AsList();
    }

    public async Task<List<Transport>> GetListByDateAsync(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default)
    {
        return await _transports.AsNoTracking()
                                .Where(t => dateFrom.ToUniversalTime() <= t.CreationDate && t.CreationDate <= dateTo.ToUniversalTime())
                                .ToListAsync(cancellationToken);
    }
}
