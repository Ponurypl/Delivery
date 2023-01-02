using MultiProject.Delivery.WebApi.v1.Auth.Common;

namespace MultiProject.Delivery.WebApi.v1.Auth.Refresh;

public sealed record RefreshResponse
{
    public TokenDetails AccessToken { get; init; } = null!;
    public TokenDetails RefreshToken { get; init; } = null!;
}