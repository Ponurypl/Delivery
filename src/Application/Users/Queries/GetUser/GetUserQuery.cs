namespace MultiProject.Delivery.Application.Users.Queries.GetUser;

public sealed record GetUserQuery : IQuery<UserDto>
{
    public required Guid UserId { get; init; }
}