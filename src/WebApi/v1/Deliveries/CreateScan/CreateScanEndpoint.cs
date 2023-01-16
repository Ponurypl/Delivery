using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;
using NJsonSchema.Validation;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateScan;

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
        Post("Scan/");
        Description(d =>
                    {
                        d.Produces(StatusCodes.Status404NotFound);
                    });
        Group<DeliveriesEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateScanRequest req, CancellationToken ct)
    {
        ErrorOr<ScanCreatedDto> result = await _sender.Send(_mapper.Map<CreateScanCommand>(req), ct);
        ValidationFailures.AddErrorsAndThrowIfNeeded(result);
        await SendOkAsync(_mapper.Map<CreateScanResponse>(result.Value),ct);
    }
}
