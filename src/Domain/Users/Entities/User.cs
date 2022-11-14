using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Users.Enums;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Users.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public bool IsActive { get; private set; }
    public UserRole Role { get; private set; }
    public string Login { get; private set; } 
    public string Password { get; private set; }
    public string PhoneNumber { get; private set; } 
    public AdvancedGeolocation? Location { get; private set; }

    private User(UserId id, bool isActive, UserRole role, string login, string password, string phoneNumber)
        : base(id)
    {
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

        return new User(UserId.New(), true, role, login, password, phoneNumber);
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

    public ErrorOr<Success> CheckIfUserIsDeliverer()
    {
        return Role is UserRole.Deliverer ? Result.Success : Failures.UserDoesNotMeetRole;
    }


    public ErrorOr<Success> CheckIfUserIsManager()
    {
        return Role is UserRole.Manager ? Result.Success : Failures.UserDoesNotMeetRole;
    }
}
