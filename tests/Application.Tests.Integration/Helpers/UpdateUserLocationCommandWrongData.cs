using System.Collections;

namespace MultiProject.Delivery.Application.Tests.Integration.Helpers;
public class UpdateUserLocationCommandWrongData : IEnumerable<object[]>
{
    //double accuracy, double heading, double latitude, double longitude, double speed, Guid userId
    public IEnumerator<object[]> GetEnumerator()
    {
        //bad accuracy
        yield return new object[] { 43431d, 1d, 1d, 1d, 1d, Guid.NewGuid() };
        yield return new object[] { 0d, 1d, 1d, 1d, 1d, Guid.NewGuid() };
        yield return new object[] { 1.1d, 1d, 1d, 1d, 1d, Guid.NewGuid() };
        //bad heading
        yield return new object[] { 1d, -1d, 1d, 1d, 1d, Guid.NewGuid() };
        yield return new object[] { 1d, 500d, 1d, 1d, 1d, Guid.NewGuid() };
        yield return new object[] { 1d, 1.123d, 1d, 1d, 1d, Guid.NewGuid() };
        //bad latitude
        yield return new object[] { 1d, 1d, -1234567890d, 1d, 1d, Guid.NewGuid() };
        yield return new object[] { 1d, 1d, 1234d, 1d, 1d, Guid.NewGuid() };
        yield return new object[] { 1d, 1d, 1.123456d, 1d, 1d, Guid.NewGuid() };
        //bad longitude
        yield return new object[] { 1d, 1d, 1d, -1234567890d, 1d, Guid.NewGuid() };
        yield return new object[] { 1d, 1d, 1d, 1234d, 1d, Guid.NewGuid() };
        yield return new object[] { 1d, 1d, 1d, 1.123456d, 1d, Guid.NewGuid() };
        //bad speed
        yield return new object[] { 1d, 1d, 1d, 1d, -1d, Guid.NewGuid() };
        yield return new object[] { 1d, 1d, 1d, 1d, 12345d, Guid.NewGuid() };
        yield return new object[] { 1d, 1d, 1d, 1d, 1.123d, Guid.NewGuid() };
        //empty userId
        yield return new object[] { 1d, 1d, 1d, 1d, 1d, Guid.Empty };
        yield return new object[] { 1d, 1d, 1d, 1d, 1d, null! };
        //mix
        yield return new object[] { 0d, -1d, -1234d, -1234d, -1d, null! };
        yield return new object[] { 43431d, 500d, 1234d, 1234d, 12345d, Guid.Empty };
        yield return new object[] { 1.1d, 1.123d, 1.123456d, 1.123456d, 1.123d, Guid.Empty };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
