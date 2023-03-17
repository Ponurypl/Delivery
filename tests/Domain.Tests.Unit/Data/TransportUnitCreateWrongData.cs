using System.Collections;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;
public class TransportUnitCreateWrongData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //string number, string additionalInformation, string description, string? barcode, double? amount, int? rawunitOfMeasureId
        //empty number
        yield return new object[] {
            "",
            "additionalInformation",
            "description"
        };
        //empty number
        yield return new object[] {
            "  ",
            "additionalInformation",
            "description"
        };
        //empty number
        yield return new object[] {
            null!,
            "additionalInformation",
            "description"
        };
        //empty description
        yield return new object[] {
            "number",
            "additionalInformation",
            ""
        };
        //empty description
        yield return new object[] {
            "number",
            "additionalInformation",
            "  "
        };
        //empty description
        yield return new object[] {
            "number",
            "additionalInformation",
            null!
        };
        //mix
        yield return new object[] {
            "",
            "additionalInformation",
            ""
        };
        //mix
        yield return new object[] {
            " ",
            "additionalInformation",
            " "
        };
        //mix
        yield return new object[] {
            null!,
            "additionalInformation",
            null!
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
