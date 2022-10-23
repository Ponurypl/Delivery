using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Users.Enums;
using MultiProject.Delivery.Domain.Users.Validators;

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

    private User(Guid id, bool isActive, UserRole role, string login, string password, string phoneNumber, AdvancedGeolocalization? geolocalization)
    {
        Id = id;
        IsActive = isActive;
        Role = role;
        Login = login;
        Password = password;
        PhoneNumber = phoneNumber;
        Geolocalization = geolocalization;
    }

    public static Result<User> Create(UserRole role, string login, string password, string phoneNumber)
    {
        UserValidator validator = new();
        Guid id = Guid.NewGuid();
        bool isActive = true;
        User newUser = new(id, isActive, role, login, password, phoneNumber, null);

        var vResults = validator.Validate(newUser);
        if(!vResults.IsValid)
        {
            throw new ValidationException(vResults.Errors);
        }
         
        return newUser;
    }
}
