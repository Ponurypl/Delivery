using Delivery.Domain.Common.Entities;
using Delivery.Domain.Users.Enums;

namespace Delivery.Domain.Users.Entities;

public sealed class User
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public AdvancedGeolocalization? Geolocalization { get; set; }
}
