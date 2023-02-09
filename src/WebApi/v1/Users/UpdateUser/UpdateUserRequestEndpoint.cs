using MultiProject.Delivery.Application.Users.Commands.UpdateUser;

namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUser;

public class UpdateUserRequestEndpoint : Endpoint<UpdateUserRequest>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UpdateUserRequestEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Put("{UserId}");
        Group<UsersEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
    {
        ErrorOr<Success> response = await _sender.Send(_mapper.Map<UpdateUserCommand>(req), ct);
        ValidationFailures.AddErrorsAndThrowIfNeeded(response);
        await SendNoContentAsync(ct);
    }
}
