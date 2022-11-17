namespace MultiProject.Delivery.Application.Users.Queries.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserDto>;