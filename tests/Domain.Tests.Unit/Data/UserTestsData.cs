using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.Enums;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public static class UserTestsData
{
    public static IEnumerable<object[]> Create_InvalidData()
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
        yield return new object[] { UserRole.Deliverer, DomainFixture.Users.Username, DomainFixture.Users.Password, null! };
        yield return new object[] { UserRole.Deliverer, DomainFixture.Users.Username, DomainFixture.Users.Password, "" };
        yield return new object[] { UserRole.Deliverer, DomainFixture.Users.Username, DomainFixture.Users.Password, " " };
        yield return new object[] { UserRole.Deliverer, null!, DomainFixture.Users.Password, DomainFixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, "", DomainFixture.Users.Password, DomainFixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, " ", DomainFixture.Users.Password, DomainFixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, DomainFixture.Users.Username, null!, DomainFixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, DomainFixture.Users.Username, "", DomainFixture.Users.PhoneNumber };
        yield return new object[] { UserRole.Deliverer, DomainFixture.Users.Username, " ", DomainFixture.Users.PhoneNumber };
    }
}