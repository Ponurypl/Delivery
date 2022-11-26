using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportIdList;

internal class GetTransportListQueryHandler : IQueryHandler<GetTransportIdListQuery, TransportIdListDto>
{
    private readonly ITransportRepository _transportRepository;

    public GetTransportListQueryHandler(ITransportRepository transportRepository)
    {
        _transportRepository = transportRepository;
    }

    public async Task<ErrorOr<TransportIdListDto>> Handle(GetTransportIdListQuery request, CancellationToken cancellationToken)
    {
        List<TransportId> transportsIds = await _transportRepository.GetIdListByDateAsync(request.DateFrom, request.DateTo, cancellationToken);
        if(transportsIds is null || transportsIds.Count == 0)
        {
            return Failure.TransportNotExists;
        }

        return new TransportIdListDto
        {
             Transports = transportsIds.Select(t => t.Value).ToList()
        };
    }
}
