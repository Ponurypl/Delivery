using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.Enums;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Data;
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
    [MemberData(nameof(AttachmentsTestsData.Create_WithAdditionalInformationAndPayload_InvalidData), MemberType = typeof(AttachmentsTestsData))]
    public void Create_WithAdditionalInformationAndPayload_WhenInvalidDataProvided_ThenValidationFailureReturned(
        UserId userId, TransportId transportId, byte[] payload, string additionalInformation)
    {
        //Arrange
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, payload, additionalInformation,
                                                       dateTimeProvider);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Theory]
    [MemberData(nameof(AttachmentsTestsData.Create_WithAdditionalInformation_InvalidData), MemberType = typeof(AttachmentsTestsData))]
    public void Create_WithAdditionalInformation_WhenInvalidDataProvided_ThenValidationFailureReturned(
        UserId userId, TransportId transportId, string additionalInformation)
    {
        //Arrange
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, additionalInformation,
                                                       dateTimeProvider);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Theory]
    [MemberData(nameof(AttachmentsTestsData.Create_WithPayload_InvalidData), MemberType = typeof(AttachmentsTestsData))]
    public void Create_WithPayload_WhenInvalidDataProvided_ThenValidationFailureReturned(
        UserId userId, TransportId transportId, byte[] payload)
    {
        //Arrange
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, payload,
                                                       dateTimeProvider);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Fact] 
    public void Create_WithAdditionalInformation_WhenDependencyNotProvided_ThenUnexpectedFailureReturned()
    {
        //Arrange
        UserId userId = DomainFixture.Users.GetId();
        TransportId transportUnitId = DomainFixture.Transports.GetId();
        string additionalInformation = DomainFixture.Attachments.AdditionalInformation;

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportUnitId, additionalInformation, null!);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Fact] 
    public void Create_WithAdditionalInformationAndPayload_WhenDependencyNotProvided_ThenUnexpectedFailureReturned()
    {
        //Arrange
        UserId userId = DomainFixture.Users.GetId();
        TransportId transportUnitId = DomainFixture.Transports.GetId();
        string additionalInformation = DomainFixture.Attachments.AdditionalInformation;
        byte[] payload = DomainFixture.Attachments.Payload;

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportUnitId, payload, additionalInformation, null!);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Fact]
    public void Create_WithPayload_WhenDependencyNotProvided_ThenUnexpectedFailureReturned()
    {
        //Arrange
        UserId userId = DomainFixture.Users.GetId();
        TransportId transportUnitId = DomainFixture.Transports.GetId();
        byte[] payload = DomainFixture.Attachments.Payload;

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportUnitId, payload, null!);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Fact]
    public void AddScanId_WhenValidDataProvided_ThenScanIdIsUpdated()
    {
        //Arrange
        ScanId scanId = DomainFixture.Scans.GetId();
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetId();

        Attachment sut = DomainFixture.Attachments.GetAttachment();

        //Act
        ErrorOr<Updated> result = sut.SetScan(transportUnitId, scanId);

        //Assert
        result.IsError.Should().BeFalse();
        sut.Should().NotBeNull();
        sut.ScanId.Should().Be(scanId);
    }

    [Theory]
    [MemberData(nameof(AttachmentsTestsData.SetScanId_InvalidData), MemberType = typeof(AttachmentsTestsData))]
    public void SetScanId_WhenInvalidDataProvided_ThenValidationFailureIsReturned(TransportUnitId transportUnitId, ScanId scanId)
    {
        //Arrange
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
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetId();
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
        TransportUnitId transportUnitId = TransportUnitId.Empty;
        Attachment sut = DomainFixture.Attachments.GetAttachment();

        //Act
        ErrorOr<Updated> result = sut.SetTransportUnit(transportUnitId);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }
}
