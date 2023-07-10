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
        //userId, transportId, fileExtension, additionalInformation
        yield return new object[] { UserId.Empty, fixture.Transports.GetId(),
                                      "jpg", fixture.Attachments.AdditionalInformation };

        yield return new object[] { fixture.Users.GetId(), TransportId.Empty,
                                      "jpg", fixture.Attachments.AdditionalInformation };

        yield return new object[] { UserId.Empty, TransportId.Empty,
                                      string.Empty, string.Empty };
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