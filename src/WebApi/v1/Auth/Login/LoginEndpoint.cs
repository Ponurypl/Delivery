using FastEndpoints.Security;
using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Users.Queries.VerifyUser;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.WebApi.Common.Auth;
using System.Security.Claims;

namespace MultiProject.Delivery.WebApi.v1.Auth.Login;

public sealed class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly ISender _sender;
    private readonly IDateTime _dateTime;

    public LoginEndpoint(ISender sender, IDateTime dateTime)
    {
        _sender = sender;
        _dateTime = dateTime;
    }

    public override void Configure()
    {
        Post("login");
        AllowAnonymous();
        Group<AuthEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        ErrorOr<VerifiedUserDto> result = await _sender.Send(new VerifyUserQuery() { Password = req.Password, Username = req.Username }, ct);

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);

        var accessExpire = _dateTime.UtcNow.AddMinutes(10);
        var accessToken = JWTBearer.CreateToken(signingKey: AuthConsts.JwtSigningKey,
                                             expireAt: accessExpire,
                                             audience: AuthConsts.AccessSchema,
                                             claims: new Claim[] { new("Username", result.Value.Username), 
                                                                   new("UserID", result.Value.Id.ToString()) });

        var refreshExpire = _dateTime.UtcNow.AddHours(2);
        var refreshToken = JWTBearer.CreateToken(signingKey: AuthConsts.JwtSigningKey,
                                                expireAt: refreshExpire,
                                                audience: AuthConsts.RefreshSchema,
                                                claims: new Claim[] { new("Username", result.Value.Username),
                                                                      new("UserID", result.Value.Id.ToString()) });

        //TODO: Zapis tokena w bazie
        await SendOkAsync(new LoginResponse
                          {
                              AccessToken = new TokenDetails
                                            {
                                                Token = accessToken, 
                                                ExpireAt = accessExpire
                                            },
                              RefreshToken = new TokenDetails()
                                            {
                                                 Token = refreshToken,
                                                 ExpireAt = refreshExpire
                                            }
                          }, ct);

    }
}