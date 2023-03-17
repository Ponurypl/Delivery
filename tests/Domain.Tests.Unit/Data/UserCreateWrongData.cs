using MultiProject.Delivery.Domain.Users.Enums;
using System.Collections;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public class UserCreateWrongData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //// Role, Username, Password, PhoneNumber
        yield return new object[] { UserRole.Deliverer, "", "", "" };
        yield return new object[] { UserRole.Deliverer, " ", " ", " " };
        yield return new object[] { UserRole.Deliverer, null!, null!, null! };
        yield return new object[] { UserRole.Manager, "", "", "" };
        yield return new object[] { UserRole.Manager, " ", " ", " " };
        yield return new object[] { UserRole.Manager, null!, null!, null! };
        yield return new object[] { (UserRole)(-5), "", "", "" };
        yield return new object[] { (UserRole)(-5), " ", " ", " " };
        yield return new object[] { (UserRole)(-5), null!, null!, null! };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}