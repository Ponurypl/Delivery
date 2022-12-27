namespace MultiProject.Delivery.WebApi.v1.Auth.Login;

public sealed record LoginResponse
{
    public TokenDetails AccessToken { get; init; } = null!;
    public TokenDetails RefreshToken { get; init; } = null!;
}