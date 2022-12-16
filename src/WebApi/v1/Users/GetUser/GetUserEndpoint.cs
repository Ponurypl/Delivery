using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Users.Queries.GetUser;

namespace MultiProject.Delivery.WebApi.v1.Users.GetUser;

public class GetUserEndpoint : Endpoint<GetUserRequest, GetUserResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetUserEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("{UserId}");
        Group<UsersEndpointGroup>();
        Description(b =>
                    {
                        b.Produces(StatusCodes.Status404NotFound);
                    });
        Version(1);
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        ErrorOr<UserDto> result = await _sender.Send(new GetUserQuery(){ UserId = req.UserId }, ct);

        if(result.IsError && result.Errors.Contains(Failure.UserNotExists))
        {
            await SendNotFoundAsync(ct);
        }

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);

        await SendOkAsync(_mapper.Map<GetUserResponse>(result.Value), ct);
    }
}
