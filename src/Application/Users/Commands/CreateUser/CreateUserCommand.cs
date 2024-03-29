﻿namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand : ICommand<UserCreatedDto>
{
    public required UserRole Role { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string PhoneNumber { get; init; }
}