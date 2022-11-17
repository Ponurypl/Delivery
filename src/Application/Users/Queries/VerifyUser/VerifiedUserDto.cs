namespace MultiProject.Delivery.Application.Users.Queries.VerifyUser;

public sealed record VerifiedUserDto
{
    public Guid Id { get; init; }
    public string Username { get; init; } = default!;
}