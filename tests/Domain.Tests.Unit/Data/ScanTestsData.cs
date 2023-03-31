using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public class ScanTestsData
{
    public static IEnumerable<object[]> Create_InvalidData()
    {
        yield return new object[] { 1, UserId.Empty };
        yield return new object[] { 0, DomainFixture.Users.GetId() };
        yield return new object[] { 0, UserId.Empty };
    }
}