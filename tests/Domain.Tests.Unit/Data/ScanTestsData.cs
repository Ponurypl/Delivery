using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public static class ScanTestsData
{
    public static IEnumerable<object[]> Create_InvalidData()
    {
        var fixture = new DomainFixture();
        yield return new object[] { 1, UserId.Empty };
        yield return new object[] { 0, fixture.Users.GetId() };
        yield return new object[] { 0, UserId.Empty };
    }
}