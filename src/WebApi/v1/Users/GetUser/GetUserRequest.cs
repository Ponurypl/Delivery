namespace MultiProject.Delivery.WebApi.v1.Users.GetUser;

public sealed record GetUserRequest
{
    public Guid UserId { get; init; }
}
