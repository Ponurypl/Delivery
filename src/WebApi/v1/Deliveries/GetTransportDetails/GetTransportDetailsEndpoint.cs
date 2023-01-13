using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransportDetails;

public sealed class GetTransportDetailsEndpoint : Endpoint<GetTransportDetailsRequest, GetTransportDetailsResponse>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public GetTransportDetailsEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Get("{TransportId}");
        Group<DeliveriesEndpointGroup>();
        Description(d =>
                    {
                        d.Produces(StatusCodes.Status404NotFound);
                    });
        Version(1);
    }

    public override async Task HandleAsync(GetTransportDetailsRequest req, CancellationToken ct)
    {
        ErrorOr<TransportDetailsDto> result = await _sender.Send(new GetTransportDetailsQuery(){ Id = req.TransportId }, ct);

        if (result.IsError && result.Errors.Contains(Failure.TransportNotExists))
        {
            await SendNotFoundAsync(ct);
            return;
        }

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);

        await SendOkAsync(_mapper.Map<GetTransportDetailsResponse>(result.Value), ct);
    }

}
