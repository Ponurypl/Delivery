using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;
public static class TransportUnitTestData
{
    public static IEnumerable<object[]> Create_InvalidData()
    {
        var fixture = new DomainFixture();
        yield return new object[] { "", fixture.TransportUnits.Description };
        yield return new object[] { " ", fixture.TransportUnits.Description };
        yield return new object[] { null!, fixture.TransportUnits.Description };
        yield return new object[] { fixture.TransportUnits.Number, "" };
        yield return new object[] { fixture.TransportUnits.Number, " " };
        yield return new object[] { fixture.TransportUnits.Number, null! };
        yield return new object[] { "", null! };
        yield return new object[] { " ", null! };
        yield return new object[] { null!, " " };
    }

    public static IEnumerable<object[]> Create_Details_InvalidData()
    {
        var fixture = new DomainFixture();
        //mix empty
        yield return new object[] { "", null!, null! };
        yield return new object[] { " ", 0d, UnitOfMeasureId.Empty };
        yield return new object[] { null!, null!, null! };
        yield return new object[] { "  ", 0d, UnitOfMeasureId.Empty };
        yield return new object[] { null!, null!, UnitOfMeasureId.Empty };
        
        //no UnitOfMeasureId but amount given
        yield return new object[] { "", 4.3d, null! };
        yield return new object[] { "", 4.3d, UnitOfMeasureId.Empty };
        
        //no amount but UnitOfMeasureId given
        yield return new object[] { "", 0d, fixture.UnitOfMeasures.GetId() };
        yield return new object[] { "", null!, fixture.UnitOfMeasures.GetId() };
        yield return new object[] { "", -3d, fixture.UnitOfMeasures.GetId() };
        
        //everything given
        yield return new object[] { fixture.TransportUnits.Barcode, fixture.TransportUnits.Amount, fixture.UnitOfMeasures.GetId() };

    }
}
