﻿using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Common.Extensions;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Users.Enums;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Users.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public bool IsActive { get; private set; }
    public UserRole Role { get; private set; }
    public string Username { get; private set; } 
    public string Password { get; private set; }
    public string PhoneNumber { get; private set; } 
    public AdvancedGeolocation? Location { get; private set; }

#pragma warning disable CS8618, IDE0051
    private User(UserId id) : base(id)
    {
        //EF Core ctor
    }
#pragma warning restore

    private User(UserId id, bool isActive, UserRole role, string username, string password, string phoneNumber)
        : base(id)
    {
        IsActive = isActive;
        Role = role;
        Username = username;
        Password = password;
        PhoneNumber = phoneNumber;
    }

    public static ErrorOr<User> Create(UserRole role, string username, string password, string phoneNumber)
    {
        //TODO: Regex na numer telefonu
        if (string.IsNullOrWhiteSpace(username)) return DomainFailures.Users.InvalidUser;
        if (string.IsNullOrWhiteSpace(password)) return DomainFailures.Users.InvalidUser;
        if (string.IsNullOrWhiteSpace(phoneNumber)) return DomainFailures.Users.InvalidUser;
        if (!role.IsValidForEnum()) return DomainFailures.Users.InvalidUser;
        
        return new User(UserId.New(), true, role, username, password, phoneNumber);
    }

    public ErrorOr<Updated> UpdateGeolocation(double latitude, double longitude, double accuracy, double heading,
                                              double speed, DateTime readDateTime)
    {
        ErrorOr<AdvancedGeolocation> geolocation = AdvancedGeolocation.Create(latitude, longitude, accuracy, readDateTime,
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
        return (Role & UserRole.Deliverer) == UserRole.Deliverer ? Result.Success : DomainFailures.Users.UserDoesNotMeetRole;
    }


    public ErrorOr<Success> CheckIfUserIsManager()
    {
        return (Role & UserRole.Manager) == UserRole.Manager ? Result.Success : DomainFailures.Users.UserDoesNotMeetRole;
    }

    public ErrorOr<Updated> UpdateUser(UserRole newRole, bool newIsActive, string newPhoneNumber)
    {
        if (string.IsNullOrWhiteSpace(newPhoneNumber)) return DomainFailures.Users.InvalidUser;
        if (!newRole.IsValidForEnum()) return DomainFailures.Users.InvalidUser;

        Role = newRole;
        IsActive = newIsActive;
        PhoneNumber = newPhoneNumber;
        return Result.Updated;
    }

    public ErrorOr<Updated> ChangePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
        {
            return DomainFailures.Users.InvalidUser;
        }

        Password = newPassword;
        return Result.Updated;
    }
}
