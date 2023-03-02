using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.Enums;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit;
public class AttachmentsTests
{
    [Fact]
    public void Create_WithPayload_WhenValidDataProvided_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);

        byte[] payload = DomainFixture.Attachments.Payload;
        UserId userId = DomainFixture.Users.GetId();
        TransportId transportId = DomainFixture.Transports.GetId();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, payload, dateTimeProvider);

        //Assert
        result.IsError.Should().BeFalse();

        Attachment obj = result.Value;
        obj.Should().NotBeNull();
        obj.Status.Should().Be(AttachmentStatus.Valid);        
        obj.CreatorId.Should().Be(userId);
        obj.LastUpdateDate.Should().Be(creationDate);
        obj.TransportId.Should().Be(transportId);
        obj.Id.Should().Be(AttachmentId.Empty);
        obj.TransportUnitId.Should().BeNull();
        obj.AdditionalInformation.Should().BeNull();
        obj.ScanId.Should().BeNull();
        obj.Payload.Should().NotBeNull();
        obj.Payload.Should().Equal(payload);
    }

    [Fact]
    public void Create_WithAdditionalInformation_WhenValidDataProvided_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);

        string additionalInformation = DomainFixture.Attachments.AdditionalInformation;
        UserId userId = DomainFixture.Users.GetId();
        TransportId transportId = DomainFixture.Transports.GetId();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, additionalInformation, dateTimeProvider);

        //Assert
        result.IsError.Should().BeFalse();

        Attachment obj = result.Value;
        obj.Should().NotBeNull();
        obj.Status.Should().Be(AttachmentStatus.Valid);
        obj.CreatorId.Should().Be(userId);
        obj.LastUpdateDate.Should().Be(creationDate);
        obj.TransportId.Should().Be(transportId);
        obj.AdditionalInformation.Should().Be(additionalInformation);
        obj.Id.Should().Be(AttachmentId.Empty);
        obj.TransportUnitId.Should().BeNull();        
        obj.ScanId.Should().BeNull();
        obj.Payload.Should().BeNull();
    }

    [Fact]
    public void Create_WithAdditionalInformationAndPayload_WhenValidDataProvided_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);

        byte[] payload = DomainFixture.Attachments.Payload;
        string additionalInformation = DomainFixture.Attachments.AdditionalInformation;
        UserId userId = DomainFixture.Users.GetId();
        TransportId transportId = DomainFixture.Transports.GetId();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, payload, additionalInformation, dateTimeProvider);

        //Assert
        result.IsError.Should().BeFalse();

        Attachment obj = result.Value;
        obj.Should().NotBeNull();
        obj.Status.Should().Be(AttachmentStatus.Valid);
        obj.CreatorId.Should().Be(userId);
        obj.LastUpdateDate.Should().Be(creationDate);
        obj.TransportId.Should().Be(transportId);
        obj.AdditionalInformation.Should().Be(additionalInformation);
        obj.Id.Should().Be(AttachmentId.Empty);
        obj.TransportUnitId.Should().BeNull();
        obj.ScanId.Should().BeNull();
        obj.Payload.Should().NotBeNull();
        obj.Payload.Should().Equal(payload);
    }

    [Theory]
    [InlineData("0f0f1d91-37a5-4957-98fd-6e21f676be64", 3, new byte[] { 1, 4, 6, 43 }, "")]
    [InlineData("00000000-0000-0000-0000-000000000000", 4, new byte[] { 1, 4, 6, 43 }, "Lorem ipsum dolor sit amet")]
    [InlineData("0f0f1d91-37a5-4957-98fd-6e21f676be64", 0, new byte[] { 1, 4, 6, 43 }, "Lorem ipsum dolor sit amet")]
    [InlineData("0f0f1d91-37a5-4957-98fd-6e21f676be64", 5, new byte[] { }, "Lorem ipsum dolor sit amet")]
    [InlineData("00000000-0000-0000-0000-000000000000", 0, new byte[] { }, "")]
    public void Create_WithAdditionalInformationAndPayload_WhenInvalidDataProvided_ThenValidationFailureReturned(
        Guid rawUserId, int rawTransportId, byte[] payload, string additionalInformation)
    {
        //Arrange
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();

        UserId userId = new(rawUserId);
        TransportId transportId = new(rawTransportId);

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, payload, additionalInformation,
                                                       dateTimeProvider);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Fact] // TODO: Do uzupełnienia testy na unhappy path każdego z create
    public void Create_WithAdditionalInformation_WhenDependencyNotProvided_ThenUnexpectedFailureReturned()
    {
        //Arrange
        UserId userId = DomainFixture.Users.GetRandomId();
        TransportId transportUnitId = DomainFixture.Transports.GetRandomId();
        string additionalInformation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportUnitId, additionalInformation, null!);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Fact]
    public void AddScanId_WhenValidDataProvided_ThenScanIdIsUpdated()
    {
        //Arrange
        ScanId scanId = DomainFixture.Scans.GetRandomId();
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetRandomId();
        Attachment sut = DomainFixture.Attachments.GetAttachment();

        //Act
        ErrorOr<Updated> result = sut.SetScan(transportUnitId, scanId);

        //Assert
        result.IsError.Should().BeFalse();
        sut.Should().NotBeNull();
        sut.ScanId.Should().Be(scanId);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(0, 0)]
    public void SetScanId_WhenInvalidDataProvided_ThenValidationFailureIsReturned(int rawTransportUnitId, int rawScanId)
    {
        //Arrange
        ScanId scanId = new(rawScanId);
        TransportUnitId transportUnitId = new(rawTransportUnitId);
        Attachment sut = DomainFixture.Attachments.GetAttachment();

        //Act
        ErrorOr<Updated> result = sut.SetScan(transportUnitId, scanId);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Fact]
    public void SetTransportUnitId_WhenValidDataIsProvided_ThenTransportUnitIdIsUpdated()
    {
        //Arrange
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetRandomId();
        Attachment sut = DomainFixture.Attachments.GetAttachment();
        
        //Act
        ErrorOr<Updated> result = sut.SetTransportUnit(transportUnitId);

        //Assert
        result.IsError.Should().BeFalse();
        sut.Should().NotBeNull();
        sut.TransportUnitId.Should().Be(transportUnitId);
    }


    [Fact]
    public void SetTransportUnitId_WhenInvalidDataIsProvided_ThenValidationFailureIsReturned()
    {
        //Arrange
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetEmptyId();
        Attachment sut = DomainFixture.Attachments.GetAttachment();

        //Act
        ErrorOr<Updated> result = sut.SetTransportUnit(transportUnitId);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }
}
