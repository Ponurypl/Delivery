using MultiProject.Delivery.WebApi.Common.Auth;
using MultiProject.Delivery.WebApi.v1.Auth.Services;

namespace MultiProject.Delivery.WebApi.v1.Auth.Refresh;

public sealed class RefreshEndpoint : EndpointWithoutRequest<RefreshResponse>
{
    private readonly ITokenService _tokenService;

    public RefreshEndpoint(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public override void Configure()
    {
        Post("refresh");
        AuthSchemes(AuthConsts.RefreshSchema);
        Group<AuthEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var refreshToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        if (!await _tokenService.ExistsAsync(refreshToken, ct))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        await _tokenService.RemoveTokenAsync(refreshToken, ct);

        var username = User.FindFirst("Username")!.Value;
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);

        var tokens = await _tokenService.GetNewTokensAsync(username, userId, ct);

        await SendOkAsync(new RefreshResponse { AccessToken = tokens.Access, RefreshToken = tokens.Refresh }, ct);

    }
}