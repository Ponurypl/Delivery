using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Data;

public static class AttachmentsTestsData
{
    public static IEnumerable<object[]> Create_WithAdditionalInformationAndPayload_InvalidData()
    {
        var fixture = new DomainFixture();
        //userId, transportId, payload, additionalInformation
        yield return new object[] { UserId.Empty, fixture.Transports.GetId(),
                                      fixture.Attachments.Payload, fixture.Attachments.AdditionalInformation };

        yield return new object[] { fixture.Users.GetId(), TransportId.Empty,
                                      fixture.Attachments.Payload, fixture.Attachments.AdditionalInformation };

        yield return new object[] { fixture.Users.GetId(), fixture.Transports.GetId(),
                                      Array.Empty<byte>(), fixture.Attachments.AdditionalInformation };

        yield return new object[] { fixture.Users.GetId(), fixture.Transports.GetId(),
                                      fixture.Attachments.Payload, string.Empty };

        yield return new object[] { UserId.Empty, TransportId.Empty,
                                      Array.Empty<byte>(), string.Empty };
    }

    public static IEnumerable<object[]> Create_WithAdditionalInformation_InvalidData()
    {
        var fixture = new DomainFixture();
        //userId, transportId, additionalInformation
        yield return new object[] { UserId.Empty, fixture.Transports.GetId(),
                                     fixture.Attachments.AdditionalInformation };

        yield return new object[] { fixture.Users.GetId(), TransportId.Empty,
                                      fixture.Attachments.AdditionalInformation };

        yield return new object[] { fixture.Users.GetId(), fixture.Transports.GetId(),
                                      string.Empty };

        yield return new object[] { UserId.Empty, TransportId.Empty,
                                      string.Empty };
    }

    public static IEnumerable<object[]> Create_WithPayload_InvalidData()
    {
        var fixture = new DomainFixture();
        //userId, transportId, payload
        yield return new object[] { UserId.Empty, fixture.Transports.GetId(),
                                      fixture.Attachments.Payload };

        yield return new object[] { fixture.Users.GetId(), TransportId.Empty,
                                      fixture.Attachments.Payload };

        yield return new object[] { fixture.Users.GetId(), fixture.Transports.GetId(),
                                      Array.Empty<byte>() };

        yield return new object[] { UserId.Empty, TransportId.Empty,
                                      Array.Empty<byte>() };
    }

    public static IEnumerable<object[]> SetScanId_InvalidData()
    {
        var fixture = new DomainFixture();
        //transportUnitId, scanId
        yield return new object[] { fixture.TransportUnits.GetId(), ScanId.Empty };
        yield return new object[] { TransportUnitId.Empty, fixture.Scans.GetId() };
        yield return new object[] { TransportUnitId.Empty, ScanId.Empty };
    }
}