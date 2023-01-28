using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;

namespace MultiProject.Delivery.Application.Scans.Queries.GetScan;
public sealed class GetScanQueryHandler : IQueryHandler<GetScanQuery, GetScanDto>
{
    private readonly IScanRepository _scanRepository;
    private readonly IMapper _mapper;

    public GetScanQueryHandler(IScanRepository scanRepository, IMapper mapper)
    {
        _scanRepository = scanRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<GetScanDto>> Handle(GetScanQuery request, CancellationToken cancellationToken)
    {
        Scan? scan = await _scanRepository.GetByIdAsync(new ScanId(request.Id), cancellationToken);

        if (scan is null)
        {
            return Failure.ScanNotExists;
        }
         
        return _mapper.Map<GetScanDto>(scan);
    }
}
