using MultiProject.Delivery.Application.Users.Commands.CreateUser;
using MultiProject.Delivery.WebApi.v1.Users.GetUser;

namespace MultiProject.Delivery.WebApi.v1.Users.CreateUser;

public sealed class CreateUserEndpoint : Endpoint<CreateUserRequest, UserCreatedResponse>
{
    private readonly ISender _sender;

    public CreateUserEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("");
        Group<UsersEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        ErrorOr<UserCreatedDto> response = await _sender.Send(new CreateUserCommand()
                                          {
                                              Password = req.Password,
                                              Username = req.Username,
                                              PhoneNumber = req.PhoneNumber,
                                              Role = (Application.Users.Commands.CreateUser.UserRole) req.Role
                                          }, ct);

        ValidationFailures.AddErrorsAndThrowIfNeeded(response);

        await SendCreatedAtAsync<GetUserEndpoint>(
            new { UserId = response.Value.Id },
            new UserCreatedResponse() { Id = response.Value.Id }, 
            generateAbsoluteUrl: true,
            cancellation: ct);
    }
}