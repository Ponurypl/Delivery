using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.Enums;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public static class UserTestsData
{
    public static IEnumerable<object[]> Create_InvalidData()
    {
        var fixture = new DomainFixture();
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
        yield return new object[] { UserRole.Deliverer, fixture.Users.Username, fixture.Users.Password, null! };
        yield return new object[] { UserRole.Deliverer, fixture.Users.Username, fixture.Users.Password, "" };
        yield return new object[] { UserRole.Deliverer, fixture.Users.Username, fixture.Users.Password, " " };
        yield return new object[] { UserRole.Deliverer, null!, fixture.Users.Password, fixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, "", fixture.Users.Password, fixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, " ", fixture.Users.Password, fixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, fixture.Users.Username, null!, fixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, fixture.Users.Username, "", fixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, fixture.Users.Username, " ", fixture.Users.PhoneNumber };
    }

    public static IEnumerable<object[]> Update_InvalidData()
    {
        //roleToUpdate, isActiveToUpdate, phoneNumberToUpdate
        yield return new object[] { UserRole.Manager, false, "" };
        yield return new object[] { (UserRole)342, false, "123" };
        yield return new object[] { (UserRole)342, false, "  " };
    }
    
}