using Bogus;
using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;
public static class TransportTestsData
{
    public static IEnumerable<object[]> Create_InvalidData()
    {
        // delivererId,
        // number,
        // startDate,
        // guidManagerId,
        // creationDate,
        // transportUnitsToCreate
        
        yield return new object[] //start date before creation date
                     {
                         DomainFixture.Users.GetId(), 
                         DomainFixture.Transports.Number, 
                         new DateTime(2021, 1, 1),
                         DomainFixture.Users.GetId(),
                         new DateTime(2023, 1, 1),
                         DomainFixture.Transports.GetTransportUnitsDtos()
                     };
        
        yield return new object[] //null start date
                     {
                         DomainFixture.Users.GetId(),
                         DomainFixture.Transports.Number,
                         null!,
                         DomainFixture.Users.GetId(),
                         new DateTime(2023, 1, 1),
                         DomainFixture.Transports.GetTransportUnitsDtos()
                     };
        
        yield return new object[] //empty delivererId
                     {
                         UserId.Empty,
                         DomainFixture.Transports.Number,
                         new DateTime(2023, 1, 2),
                         DomainFixture.Users.GetId(),
                         new DateTime(2023, 1, 1),
                         DomainFixture.Transports.GetTransportUnitsDtos()
                     };
        
        yield return new object[] //empty managerId
                     {
                         DomainFixture.Users.GetId(),
                         DomainFixture.Transports.Number,
                         new DateTime(2023, 1, 2),
                         UserId.Empty,
                         new DateTime(2023, 1, 1),
                         DomainFixture.Transports.GetTransportUnitsDtos()
                     };
        
        yield return new object[] //empty number
                     {
                         DomainFixture.Users.GetId(),
                         string.Empty,
                         new DateTime(2023, 1, 2),
                         DomainFixture.Users.GetId(),
                         new DateTime(2023, 1, 1),
                         DomainFixture.Transports.GetTransportUnitsDtos()
                     };
        
        yield return new object[] //null number
                     {
                         DomainFixture.Users.GetId(),
                         null!,
                         new DateTime(2023, 1, 2),
                         DomainFixture.Users.GetId(),
                         new DateTime(2023, 1, 1),
                         DomainFixture.Transports.GetTransportUnitsDtos()
                     };
        
        yield return new object[] //empty List<NewTransportUnit>
                     {
                         DomainFixture.Users.GetId(),
                         DomainFixture.Transports.Number,
                         new DateTime(2023, 1, 2),
                         DomainFixture.Users.GetId(),
                         new DateTime(2023, 1, 1),
                         new List<NewTransportUnit>()
                     };
        
        yield return new object[] //null List<NewTransportUnit>
                     {
                         DomainFixture.Users.GetId(),
                         DomainFixture.Transports.Number,
                         new DateTime(2023, 1, 2),
                         DomainFixture.Users.GetId(),
                         new DateTime(2023, 1, 1),
                         null!
                     };
        
        yield return new object[] //mix
                     {
                         UserId.Empty,
                         null!,
                         null!,
                         UserId.Empty,
                         new DateTime(2023, 1, 1),
                         null!
                     };
        
        yield return new object[] //mix
                     {
                         UserId.Empty,
                         string.Empty,
                         new DateTime(2021, 1, 1),
                         UserId.Empty,
                         new DateTime(2023, 1, 1),
                         DomainFixture.Transports.GetTransportUnitsDtos()
                     };
    }

    public static IEnumerable<object[]> Create_InvalidTransportUnitsData()
    {
        var multiDto = DomainFixture.Transports.GetMultiTransportUnitDto();

        yield return new object[] //duplicate number on MultiTransportUnits
                     {
                         new List<NewTransportUnit> { multiDto, multiDto }
                     };

        var uniqueDto1 = DomainFixture.Transports.GetUniqueTransportUnitDto();
        var uniqueDto2 = DomainFixture.Transports.GetUniqueTransportUnitDto("12345ABC/123");
        uniqueDto2.Barcode = uniqueDto1.Barcode;
        yield return new object[] //duplicate barcode on UniqueTransportUnits
                     {
                         new List<NewTransportUnit> { uniqueDto1, uniqueDto2 }
                     };

        yield return new object[] //duplicate barcode on UniqueTransportUnits
                                  //and duplicate number on MultiTransportUnits
                     {
                         new List<NewTransportUnit> { uniqueDto1, uniqueDto2, multiDto, multiDto }
                     };

        uniqueDto1.Number = multiDto.Number;
        uniqueDto2.Number = multiDto.Number;
        yield return new object[] //duplicate barcode on UniqueTransportUnits
                                  //and duplicate number on all TransportUnits
                     {
                         new List<NewTransportUnit> { uniqueDto1, uniqueDto2, multiDto, multiDto }
                     };
    }
}
