namespace MultiProject.Delivery.Application.Users.Queries.VerifyUser;

public sealed record VerifyUserQuery(string Username, string Password) : IQuery<VerifiedUserDto>;