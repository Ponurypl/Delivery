using MultiProject.Delivery.Application.Users.Commands.CreateUser;
using System.Collections;

namespace MultiProject.Delivery.Application.Tests.Integration.Helpers;
internal class UserCreateCommandWrongData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //string username, string password, string phoneNumber, UserRole role
        //missing username
        yield return new object[] { "", "password", "123456789", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "  ", "password", "123456789", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { null!, "password", "123456789", UserRole.Deliverer | UserRole.Manager };
        //missing password
        yield return new object[] { "username", "", "123456789", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "username", "  ", "123456789", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "username", null!, "123456789", UserRole.Deliverer | UserRole.Manager };
        //missing phoneNumber
        yield return new object[] { "username", "password", "", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "username", "password", "  ", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "username", "password", null!, UserRole.Deliverer | UserRole.Manager };
        //missing role
        yield return new object[] { "username", "password", "123456789", (UserRole)(-2) };
        yield return new object[] { "username", "password", "123456789", null! };
        //password same as username
        yield return new object[] { "password", "password", "123456789", UserRole.Deliverer | UserRole.Manager };
        //phoneNumber that for sure is not valid
        yield return new object[] { "username", "password", "ABCDEFGHI", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "username", "password", "45243t6ytvzsdg32w", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "username", "password", "2", UserRole.Deliverer | UserRole.Manager };
        //mix
        yield return new object[] { "", "", "", (UserRole)(-2) };
        yield return new object[] { "  ", " ", "   ", (UserRole)(-2) };
        yield return new object[] { null!, null!, null!, null! };
        yield return new object[] { "password", "password", "ABCDEFGHI", (UserRole)(-2) };
        //too long
        yield return new object[] { "usernameusernameusernameusernameusernameusernameusernameusernameusername", "password", "123456789", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "username", "passwordpasswordpasswordpasswordpasswordpasswordpasswordpasswordpassword", "123456789", UserRole.Deliverer | UserRole.Manager };
        yield return new object[] { "username", "password", "123456789123456789", UserRole.Deliverer | UserRole.Manager };

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
