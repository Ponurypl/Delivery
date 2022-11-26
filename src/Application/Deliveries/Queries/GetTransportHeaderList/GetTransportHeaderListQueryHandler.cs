using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportHeaderList;

internal class GetTransportHeaderListQueryHandler : IQueryHandler<GetTransportHeaderListQuery, TransportHeaderListDto>
{
    private readonly ITransportRepository _transportRepository;
    private readonly IMapper _mapper;

    public GetTransportHeaderListQueryHandler(ITransportRepository transportRepository, IMapper mapper)
    {
        _transportRepository = transportRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TransportHeaderListDto>> Handle(GetTransportHeaderListQuery request, CancellationToken cancellationToken)
    {
        List<Transport> transports = await _transportRepository.GetListByDateAsync(request.DateFrom, request.DateTo, cancellationToken);
        if(transports is null || transports.Count == 0)
        {
            return Failure.TransportNotExists;
        }

        return new TransportHeaderListDto
        {
            Transports = _mapper.Map<List<TransportDto>>(transports)
        };
    }
}
