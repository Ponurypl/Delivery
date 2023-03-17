using System.Collections;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;
public class TransportUnitCreateWrongTransportUnitDetailsData : IEnumerable<object[]>
{
    //string? barcode, double? amount, int? rawUnitOfMeasureId
    public IEnumerator<object[]> GetEnumerator()
    {
        //mix empty
        yield return new object[] { "", null!, null! };
        yield return new object[] { "", 0d, 0 };
        yield return new object[] { null!, null!, null! };
        yield return new object[] { "  ", 0d, 0 };
        yield return new object[] { null!, null!, 0 };
        //no rawUnitOfMeasureId but amount given
        yield return new object[] { "", 4.3d, null! };
        yield return new object[] { "", 4.3d, 0 };
        //no amount but rawUnitOfMeasureId given
        yield return new object[] { "", 0d, 3 };
        yield return new object[] { "", null!, 5 };
        yield return new object[] { "", -3d, 5 };
        //everything given
        yield return new object[] { "ABC", 3d, 5 };
        yield return new object[] { "sdfsdafgsdg12", 1d, 1 };

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
