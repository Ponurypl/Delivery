namespace MultiProject.Delivery.WebApi.v1.Users.CreateUser;

[Flags]
public enum UserRole
{
    None = 0, 
    Deliverer = 1, 
    Manager = 2
}