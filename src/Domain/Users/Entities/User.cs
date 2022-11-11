using MultiProject.Delivery.Domain.Common.Interfaces;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Users.Enums;

namespace MultiProject.Delivery.Domain.Users.Entities;

public sealed class User : IAggregateRoot
{
    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }
    public UserRole Role { get; private set; }
    public string Login { get; private set; } 
    public string Password { get; private set; }
    public string PhoneNumber { get; private set; } 
    public AdvancedGeolocation? Location { get; private set; }

    private User(Guid id, bool isActive, UserRole role, string login, string password, string phoneNumber)
    {
        Id = id;
        IsActive = isActive;
        Role = role;
        Login = login;
        Password = password;
        PhoneNumber = phoneNumber;
    }

    public static ErrorOr<User> Create(UserRole role, string login, string password, string phoneNumber)
    {
        if (login == password)
        {
            return Failures.LoginSameAsPassword;
        }

        //TODO: Regex na numer telefonu

        return new User(Guid.NewGuid(), true, role, login, password, phoneNumber);
    }

    public ErrorOr<Updated> UpdateGeolocation(double latitude, double longitude, double accuracy, double heading,
                                              double speed, DateTime readDateTime)
    {
        var geolocation = AdvancedGeolocation.Create(latitude, longitude, accuracy, readDateTime,
                                                     heading, speed);

        if (geolocation.IsError)
        {
            return geolocation.Errors;
        }

        Location = geolocation.Value;
        return Result.Updated;
    }
}
