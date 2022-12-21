namespace MultiProject.Delivery.WebApi.v1.Users.VerifyUser;

public sealed record VerifyUserResponse
{
    public Guid Id { get; init; }
    public string Username { get; init; } = default!;
}
