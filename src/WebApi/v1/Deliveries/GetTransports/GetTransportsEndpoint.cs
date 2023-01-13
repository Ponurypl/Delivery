using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransports;

public sealed class GetTransportsEndpoint : Endpoint<GetTransportsRequest, List<GetTransportsResponse>>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public GetTransportsEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Get("");
        Group<DeliveriesEndpointGroup>();
        Description(d =>
                    {
                        d.Produces(StatusCodes.Status404NotFound);

                    });
        Version(1);
    }

    public override async Task HandleAsync(GetTransportsRequest req, CancellationToken ct)
    {
        ErrorOr<List<TransportDto>> result = await _sender.Send(_mapper.Map<GetTransportsQuery>(req), ct);

        if (result.IsError && result.Errors.Contains(Failure.TransportNotExists))
        {
            await SendNotFoundAsync(ct);
            return;
        }

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);

        await SendOkAsync(_mapper.Map<List<GetTransportsResponse>>(result.Value), ct);
    }
}
