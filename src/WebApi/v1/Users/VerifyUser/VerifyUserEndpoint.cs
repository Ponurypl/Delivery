using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Users.Queries.VerifyUser;

namespace MultiProject.Delivery.WebApi.v1.Users.VerifyUser;

public sealed class VerifyUserEndpoint : Endpoint<VerifyUserRequest, VerifyUserResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public VerifyUserEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Post("Verify"); // daje post bo mowiliśmy że adres nie może być szyfrowany przez SSL i podanie tam loginu i hasła nie wydaje się rozsądne
        Group<UsersEndpointGroup>();
        Description(x =>
                    {
                        x.Produces(StatusCodes.Status404NotFound);
                    });
    }

    public override async Task HandleAsync(VerifyUserRequest req, CancellationToken ct)
    {
        ErrorOr<VerifiedUserDto> result = await _sender.Send(new VerifyUserQuery() { Password = req.Password, Username = req.Username }, ct);

        if(result.IsError && result.Errors.Contains(Failure.UserNotExists))
        {
            await SendNotFoundAsync(ct);
        }

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);

        await SendOkAsync(_mapper.Map<VerifyUserResponse>(result.Value), ct);

    }
}

