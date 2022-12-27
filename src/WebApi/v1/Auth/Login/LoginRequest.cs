namespace MultiProject.Delivery.WebApi.v1.Auth.Login;

public sealed record LoginRequest
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}
