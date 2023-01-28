using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Scans.Queries.GetTransportUnitScans;
using MultiProject.Delivery.WebApi.v1.Scans.GetScan;

namespace MultiProject.Delivery.WebApi.v1.Scans.GetTransportUnitScans;

public sealed class GetTransportUnitScansEndpoint : Endpoint<GetTransportUnitScansRequest, List<GetTransportUnitScansResponse>>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public GetTransportUnitScansEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Get("TransportUnit/{TransportUnitId}");
        Group<ScansEndpointGroup>();
        Description(builder =>
        {
            builder.Produces(StatusCodes.Status404NotFound);
        });
        Version(1);
    }

    public override async Task HandleAsync(GetTransportUnitScansRequest request, CancellationToken cancellationToken)
    {
        ErrorOr<List<GetTransportUnitScansDto>> result = await _sender.Send( 
            new GetTransportUnitScansQuery { Id = request.TransportUnitId }, cancellationToken);

        if (result.IsError && result.Errors.Contains(Failure.ScanNotExists))
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);
        await SendOkAsync(_mapper.Map<List<GetTransportUnitScansResponse>>(result.Value), cancellationToken);



    }
}
