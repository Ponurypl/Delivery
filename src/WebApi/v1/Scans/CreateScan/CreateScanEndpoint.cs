using MultiProject.Delivery.Application.Scans.Commands.CreateScan;

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
        Post("scan");
        Group<ScansEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateScanRequest req, CancellationToken ct)
    {
        ErrorOr<ScanCreatedDto> result = await _sender.Send(_mapper.Map<CreateScanCommand>(req), ct);
        ValidationFailures.AddErrorsAndThrowIfNeeded(result);
        await SendOkAsync(_mapper.Map<CreateScanResponse>(result.Value), ct);
    }
}
