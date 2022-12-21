namespace MultiProject.Delivery.WebApi.v1.Users.VerifyUser;

public sealed record VerifyUserRequest
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}
