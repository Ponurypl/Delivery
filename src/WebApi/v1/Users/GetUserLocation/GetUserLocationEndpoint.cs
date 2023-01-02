using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Users.Queries.GetUserLocation;

namespace MultiProject.Delivery.WebApi.v1.Users.GetUserLocation;

public sealed class GetUserLocationEndpoint : Endpoint<GetUserLocationRequest, GetUserLocationResponse>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public GetUserLocationEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Get("{UserId}/location");
        Group<UsersEndpointGroup>();
        Description(x =>
                    {
                        x.Produces(StatusCodes.Status404NotFound);
                    });
        Version(1);

    }

    public override async Task HandleAsync(GetUserLocationRequest req, CancellationToken ct)
    {
        ErrorOr<GetUserLocationDto> result = await _sender.Send(new GetUserLocationQuery() { UserId = req.UserId },ct);

        if (result.IsError && result.Errors.Contains(Failure.UserNotExists)) 
        {
            await SendNotFoundAsync(ct);
            return;
        }

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);

        await SendOkAsync(_mapper.Map<GetUserLocationResponse>(result.Value), ct);


    }
}
