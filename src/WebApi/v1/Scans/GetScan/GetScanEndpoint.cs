using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Scans.Queries.GetScan;

namespace MultiProject.Delivery.WebApi.v1.Scans.GetScan;

public sealed class GetScanEndpoint : Endpoint<GetScanRequest,GetScanResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetScanEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("{ScanId}");
        Group<ScansEndpointGroup>();
        Description(builder =>
        {
            builder.Produces(StatusCodes.Status404NotFound);
        });
        Version(1);
    }

    public override async Task HandleAsync(GetScanRequest request, CancellationToken cancellationToken)
    {
        ErrorOr<GetScanDto> result = await _sender.Send(new GetScanQuery { Id = request.ScanId }, cancellationToken);

        if (result.IsError && result.Errors.Contains(Failure.ScanNotExists))
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);
        await SendOkAsync(_mapper.Map<GetScanResponse>(result.Value), cancellationToken);

    }
}
