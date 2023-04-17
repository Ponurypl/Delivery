using System.Collections;

namespace MultiProject.Delivery.Application.Tests.Integration.Data;

public class GetUserLocationQueryWrongData : IEnumerable<object[]>
{
    //Id
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { null! };
        yield return new object[] { Guid.Empty };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}