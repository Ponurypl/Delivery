namespace MultiProject.Delivery.WebApi.v1.Users.CreateUser;

public sealed record CreateUserRequest
{
    //TODO: kurde nie mogę zepchnąć opisu enuma ( w swaggerze ) do enuma, musi chyba być na poziomie request-a
    /// <summary>
    /// [Flags] enum for roles that user fullfils
    /// ( None = 0, Deliverer = 1, Manager = 2 )
    /// </summary>
    /// <example>3</example>
    public UserRole Role { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}