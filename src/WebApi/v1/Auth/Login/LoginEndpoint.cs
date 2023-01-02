using MultiProject.Delivery.Application.Users.Queries.VerifyUser;
using MultiProject.Delivery.WebApi.v1.Auth.Common;
using MultiProject.Delivery.WebApi.v1.Auth.Services;

namespace MultiProject.Delivery.WebApi.v1.Auth.Login;

public sealed class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly ISender _sender;
    private readonly ITokenService _tokenService;

    public LoginEndpoint(ISender sender, ITokenService tokenService)
    {
        _sender = sender;
        _tokenService = tokenService;
    }

    public override void Configure()
    {
        Post("login");
        AllowAnonymous();
        Description(b => b.ProducesValidationProblem());
        Group<AuthEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        ErrorOr<VerifiedUserDto> result = await _sender.Send(new VerifyUserQuery() { Password = req.Password, Username = req.Username }, ct);

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);

        (TokenDetails Access, TokenDetails Refresh) tokens =
            await _tokenService.GetNewTokensAsync(result.Value.Username, result.Value.Id, ct);

        await SendOkAsync(new LoginResponse { AccessToken = tokens.Access, RefreshToken = tokens.Refresh, }, ct);

    }
}