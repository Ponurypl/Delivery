using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public static class AttachmentsTestsData
{
    public static IEnumerable<object[]> Create_WithAdditionalInformationAndPayload_InvalidData()
    {
        //userId, transportId, payload, additionalInformation
        yield return new object[] { UserId.Empty, DomainFixture.Transports.GetId(), 
                                      DomainFixture.Attachments.Payload, DomainFixture.Attachments.AdditionalInformation };

        yield return new object[] { DomainFixture.Users.GetId(), TransportId.Empty,
                                      DomainFixture.Attachments.Payload, DomainFixture.Attachments.AdditionalInformation };

        yield return new object[] { DomainFixture.Users.GetId(), DomainFixture.Transports.GetId(), 
                                      Array.Empty<byte>(), DomainFixture.Attachments.AdditionalInformation };

        yield return new object[] { DomainFixture.Users.GetId(), DomainFixture.Transports.GetId(), 
                                      DomainFixture.Attachments.Payload, string.Empty };

        yield return new object[] { UserId.Empty, TransportId.Empty, 
                                      Array.Empty<byte>(), string.Empty };
    }

    public static IEnumerable<object[]> Create_WithAdditionalInformation_InvalidData()
    {
        //userId, transportId, additionalInformation
        yield return new object[] { UserId.Empty, DomainFixture.Transports.GetId(),
                                     DomainFixture.Attachments.AdditionalInformation };

        yield return new object[] { DomainFixture.Users.GetId(), TransportId.Empty,
                                      DomainFixture.Attachments.AdditionalInformation };

        yield return new object[] { DomainFixture.Users.GetId(), DomainFixture.Transports.GetId(),
                                      string.Empty };

        yield return new object[] { UserId.Empty, TransportId.Empty,
                                      string.Empty };
    }

    public static IEnumerable<object[]> Create_WithPayload_InvalidData()
    {
        //userId, transportId, payload
        yield return new object[] { UserId.Empty, DomainFixture.Transports.GetId(),
                                      DomainFixture.Attachments.Payload };

        yield return new object[] { DomainFixture.Users.GetId(), TransportId.Empty,
                                      DomainFixture.Attachments.Payload };

        yield return new object[] { DomainFixture.Users.GetId(), DomainFixture.Transports.GetId(),
                                      Array.Empty<byte>() };

        yield return new object[] { UserId.Empty, TransportId.Empty,
                                      Array.Empty<byte>() };
    }

    public static IEnumerable<object[]> SetScanId_InvalidData()
    {
        //transportUnitId, scanId
        yield return new object[] {  DomainFixture.TransportUnits.GetId(), ScanId.Empty };
        yield return new object[] {  TransportUnitId.Empty, DomainFixture.Scans.GetId() };
        yield return new object[] {  TransportUnitId.Empty, ScanId.Empty };
    }
}