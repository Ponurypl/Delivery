using MultiProject.Delivery.Domain.Users.Enums;
using System.Collections;

namespace MultiProject.Delivery.Domain.Tests.Unit.DataProviders;

public class UserCreateWrongData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        List<object[]> data = new();
        // Role, Username, Password, PhoneNumber
        data.Add(new object[] { UserRole.Deliverer, "", "", "" });
        data.Add(new object[] { UserRole.Deliverer, " ", " ", " " });
        data.Add(new object[] { UserRole.Deliverer, null, null, null });
        data.Add(new object[] { UserRole.Manager, "", "", "" });
        data.Add(new object[] { UserRole.Manager, " ", " ", " " });
        data.Add(new object[] { UserRole.Manager, null, null, null });
        data.Add(new object[] { (UserRole)(-5), "", "", "" });
        data.Add(new object[] { (UserRole)(-5), " ", " ", " " });
        data.Add(new object[] { (UserRole)(-5), null, null, null });
        
        return data.GetEnumerator();

        //TODO: Pokazać yieldy
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}