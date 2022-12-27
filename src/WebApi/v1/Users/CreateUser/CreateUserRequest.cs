namespace MultiProject.Delivery.WebApi.v1.Users.CreateUser;

/// <example>
/// { "Username": "PanPaweł", "Password": "OJakieHaslo11!!@", "Role": 3, "PhoneNumber": "1234567890" }
/// </example>
public sealed record CreateUserRequest
{
    public UserRole Role { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}