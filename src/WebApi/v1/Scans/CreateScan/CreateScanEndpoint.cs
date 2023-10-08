using MultiProject.Delivery.Application.Scans.Commands.CreateScan;
using MultiProject.Delivery.WebApi.v1.Scans.GetScan;

namespace MultiProject.Delivery.WebApi.v1.Scans.CreateScan;

public sealed class CreateScanEndpoint : Endpoint<CreateScanRequest, CreateScanResponse>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public CreateScanEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Post("");
        Group<ScansEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateScanRequest req, CancellationToken ct)
    {
        ErrorOr<ScanCreatedDto> result = await _sender.Send(_mapper.Map<CreateScanCommand>(req), ct);
        ValidationFailures.AddErrorsAndThrowIfNeeded(result);
        await SendCreatedAtAsync<GetScanEndpoint>(
            new { ScanId = result.Value.Id },
            _mapper.Map<CreateScanResponse>(result.Value),
            generateAbsoluteUrl: true,
            cancellation: ct);
    }
}
