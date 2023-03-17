using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using System.Collections;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;
public class TransportCreateWrongData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //Guid guidDelivererId, string number, DateTime startDate, Guid guidManagerId, DateTime creationDate, List<NewTransportUnit> transportUnitsToCreate
        //start date before creation date
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            new DateTime(2021, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            DomainFixture.NewTransportUnits.GetFilledList()
        };
        //null start date
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            null!,
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            DomainFixture.NewTransportUnits.GetFilledList()
        };
        //empty guidDelivererId
        yield return new object[] {
            Guid.Empty,
            "ABSFDD",
            new DateTime(2023, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            DomainFixture.NewTransportUnits.GetFilledList()
        };
        //empty guidManagerId
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            new DateTime(2023, 1, 1, 12, 0, 0),
            Guid.Empty,
            new DateTime(2023, 1, 1, 12, 0, 0),
            DomainFixture.NewTransportUnits.GetFilledList()
        };
        //empty number
        yield return new object[] {
            Guid.NewGuid(),
            "",
            new DateTime(2023, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            DomainFixture.NewTransportUnits.GetFilledList()
        };
        //null number
        yield return new object[] {
            Guid.NewGuid(),
            null!,
            new DateTime(2023, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            DomainFixture.NewTransportUnits.GetFilledList()
        };
        //empty List<NewTransportUnit>
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            new DateTime(2023, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            DomainFixture.NewTransportUnits.GetEmptyList()
        };
        //null List<NewTransportUnit>
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            new DateTime(2023, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            null!
        };
        //mix
        yield return new object[] {
            Guid.Empty,
            null!,
            null!,
            Guid.Empty,
            new DateTime(2023, 1, 1, 12, 0, 0),
            null!
        };
        //mix
        yield return new object[] {
            Guid.Empty,
            "",
            new DateTime(2021, 1, 1, 12, 0, 0),
            Guid.Empty,
            new DateTime(2023, 1, 1, 12, 0, 0),
            DomainFixture.NewTransportUnits.GetEmptyList()
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
