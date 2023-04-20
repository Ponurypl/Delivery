using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public static class UnitOfMeasureTestsData
{
    public static IEnumerable<object[]> Create_InvalidData()
    {
        var fixture = new DomainFixture();
        yield return new object[] { "", "" };
        yield return new object[] { " ", " " };
        yield return new object[] { null!, null! };
        yield return new object[] { fixture.UnitOfMeasures.Name, "" };
        yield return new object[] { fixture.UnitOfMeasures.Name, " " };
        yield return new object[] { fixture.UnitOfMeasures.Name, null! };
        yield return new object[] { "", fixture.UnitOfMeasures.Symbol };
        yield return new object[] { " ", fixture.UnitOfMeasures.Symbol };
        yield return new object[] { null!, fixture.UnitOfMeasures.Symbol };
    }

}