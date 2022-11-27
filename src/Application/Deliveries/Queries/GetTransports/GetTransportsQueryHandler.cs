using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;

public sealed class GetTransportsQueryHandler : IQueryHandler<GetTransportsQuery, List<TransportDto>>
{
    private readonly ITransportRepository _transportRepository;
    private readonly IMapper _mapper;

    public GetTransportsQueryHandler(ITransportRepository transportRepository, IMapper mapper)
    {
        _transportRepository = transportRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<TransportDto>>> Handle(GetTransportsQuery request, CancellationToken cancellationToken)
    {
        List<Transport> transports = await _transportRepository.GetListByDateAsync(request.DateFrom, request.DateTo, cancellationToken);
        if(transports is null || transports.Count == 0)
        {
            return Failure.TransportNotExists;
        }

        return _mapper.Map<List<TransportDto>>(transports);
    }
}
