namespace MultiProject.Delivery.WebApi.v1.Users.CreateUser;

/// <example>
/// { "Username": "PanPaweł", "Password": "OJakieHaslo11!!@", "Role": 3, "PhoneNumber": "1234567890" }
/// </example>
public sealed record CreateUserRequest
{
    public UserRole Role { get; init; }
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
}