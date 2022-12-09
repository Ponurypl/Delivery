namespace WebApi.v1.Users.CreateUser;

public sealed record CreateUserRequest
{
    public UserRole Role { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}