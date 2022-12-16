namespace MultiProject.Delivery.Application.Users.Queries.GetUser;

public sealed record UserDto
{
    public Guid Id { get; init; }
    public string Role { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!; 
}