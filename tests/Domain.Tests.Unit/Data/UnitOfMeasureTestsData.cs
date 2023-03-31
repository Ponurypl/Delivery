using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public static class UnitOfMeasureTestsData
{
    public static IEnumerable<object[]> Create_InvalidData()
    {
        yield return new object[] { "", "" };
        yield return new object[] { " ", " " };
        yield return new object[] { null!, null! };
        yield return new object[] { DomainFixture.UnitOfMeasures.Name, "" };
        yield return new object[] { DomainFixture.UnitOfMeasures.Name, " " };
        yield return new object[] { DomainFixture.UnitOfMeasures.Name, null! };
        yield return new object[] { "", DomainFixture.UnitOfMeasures.Symbol };
        yield return new object[] { " ", DomainFixture.UnitOfMeasures.Symbol };
        yield return new object[] { null!, DomainFixture.UnitOfMeasures.Symbol };
    }

}