using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Users.Enums;

namespace MultiProject.Delivery.Domain.Users.Entities;

public sealed class User : IAggregateRoot
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public AdvancedGeolocalization? Geolocalization { get; set; }
}
