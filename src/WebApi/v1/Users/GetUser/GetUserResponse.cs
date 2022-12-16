namespace MultiProject.Delivery.WebApi.v1.Users.GetUser;

public class GetUserResponse
{
    public Guid Id { get; init; }
    public string Role { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
}
