using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;

namespace MultiProject.Delivery.Application.Scans.Queries.GetTransportUnitScans;
public sealed class GetTransportUnitScansQueryHandler : IQueryHandler<GetTransportUnitScansQuery, List<GetTransportUnitScansDto>>
{
    private readonly IScanRepository _scanRepository;
    private readonly IMapper _mapper;

    public GetTransportUnitScansQueryHandler(IScanRepository scanRepository, IMapper mapper)
    {
        _scanRepository = scanRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<GetTransportUnitScansDto>>> Handle(GetTransportUnitScansQuery request, CancellationToken cancellationToken)
    {
        List<Scan> scans = await _scanRepository.GetAllByTransportUnitIdAsync(new TransportUnitId(request.Id), cancellationToken);
        if(scans.Count == 0)
        {
            return Failure.ScanNotExists;
        }

        return _mapper.Map<List<GetTransportUnitScansDto>>(scans);
    }
}
