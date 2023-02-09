namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUser;

public sealed record UpdateUserRequest
{
    public Guid UserId { get; init; }
    public bool IsActive { get; init; }
    public UserRole Role { get; init; }
    public string PhoneNumber { get; init; } = default!;
}
