using MediatR;
using ErrorOr;
using MultiProject.Delivery.Application.Users.Queries.GetUser;

namespace WebApi.v1.Users.GetUser;

public class GetUserEndpoint : Endpoint<GetUserRequest, GetUserResponse>
{
    private readonly ISender _sender;

    public GetUserEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("users");
        Version(1);
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        ErrorOr<UserDto> result = await _sender.Send(new GetUserQuery(){ UserId = req.UserId }, ct);

        if(result.IsError)
        {
            foreach(var error in result.Errors)
            {
                AddError(error.Description, error.Code);
            }
            ThrowError("Error List");
            return;
        }

        await SendOkAsync(new GetUserResponse()
        {
            Id = result.Value.Id,
            Role = result.Value.Role,
            PhoneNumber = result.Value.PhoneNumber,
            Username = result.Value.Username

        });
    }
}
