using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Models;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;

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
        List<TransportDbModel> transports = await _transportRepository.GetTransportListAsync(request.DateFrom, request.DateTo);
        if (transports.Count == 0)
        {
            return Failure.TransportNotExists;
        }

        return _mapper.Map<List<TransportDto>>(transports);
    }
}
