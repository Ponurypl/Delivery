using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using System.Collections;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;
public class TransportCreateWrongTransportUnitData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //Guid guidDelivererId, string number, DateTime startDate, Guid guidManagerId, DateTime creationDate, List<NewTransportUnit> transportUnitsToCreate

        //duplicate number on MultiTransportUnits
        List<Deliveries.DTO.NewTransportUnit> prepList = DomainFixture.NewTransportUnits.GetEmptyList();
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedMultiTransportUnit());
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedMultiTransportUnit());
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            new DateTime(2024, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            prepList
        };

        //duplicate barcode on UniqueTransportUnits
        prepList.Clear();
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedUniqueTransportUnit(number: "Sample"));
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedUniqueTransportUnit(number: "Sample1"));
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            new DateTime(2024, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            prepList
        };

        //duplicate barcode on UniqueTransportUnits
        //and duplicate number on MultiTransportUnits
        prepList.Clear();
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedUniqueTransportUnit(number: "Sample"));
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedUniqueTransportUnit(number: "Sample1"));
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedMultiTransportUnit());
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedMultiTransportUnit());
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            new DateTime(2024, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            prepList
        };

        //duplicate barcode on UniqueTransportUnits
        //and duplicate number on all UransportUnits
        prepList.Clear();
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedUniqueTransportUnit(number: "Sample"));
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedUniqueTransportUnit(number: "Sample"));
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedMultiTransportUnit(number: "Sample"));
        prepList.Add(DomainFixture.NewTransportUnits.GetPredefinedMultiTransportUnit(number: "Sample"));
        yield return new object[] {
            Guid.NewGuid(),
            "ABSFDD",
            new DateTime(2024, 1, 1, 12, 0, 0),
            Guid.NewGuid(),
            new DateTime(2023, 1, 1, 12, 0, 0),
            prepList
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
