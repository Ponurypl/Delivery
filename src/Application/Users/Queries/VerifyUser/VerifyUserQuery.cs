namespace MultiProject.Delivery.Application.Users.Queries.VerifyUser;

public sealed record VerifyUserQuery : IQuery<VerifiedUserDto>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}