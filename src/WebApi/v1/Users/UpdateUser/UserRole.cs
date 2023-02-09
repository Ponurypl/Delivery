namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUser;

/// <summary>
/// [Flags] enum for roles that user fulfills
/// ( None = 0, Deliverer = 1, Manager = 2 )
/// </summary>
[Flags]
public enum UserRole
{
    None = 0,
    Deliverer = 1,
    Manager = 2
}