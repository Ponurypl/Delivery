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

namespace MultiProject.Delivery.Domain.Tests.Unit.Entities;
public class AttachmentsTests
{
    private readonly DomainFixture _domainFixture = new();
    
    [Fact]
    public void Create_WithFileExtension_WhenValidDataProvided_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);
        const string fileExtension = "jpg";
        
        UserId userId = _domainFixture.Users.GetId();
        TransportId transportId = _domainFixture.Transports.GetId();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, dateTimeProvider, fileExtension: fileExtension);

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
        obj.AdditionalInformation.Should().BeNullOrEmpty();
        obj.ScanId.Should().BeNull();
        obj.FileExtension.Should().Be(fileExtension);
    }

    [Fact]
    public void Create_WithAdditionalInformation_WhenValidDataProvided_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);

        string additionalInformation = _domainFixture.Attachments.AdditionalInformation;
        UserId userId = _domainFixture.Users.GetId();
        TransportId transportId = _domainFixture.Transports.GetId();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, dateTimeProvider, additionalInformation: additionalInformation);

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
        obj.FileExtension.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Create_WithAdditionalInformationAndFileExtension_WhenValidDataProvided_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);
        const string fileExtension = "jpg";


        string additionalInformation = _domainFixture.Attachments.AdditionalInformation;
        UserId userId = _domainFixture.Users.GetId();
        TransportId transportId = _domainFixture.Transports.GetId();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, dateTimeProvider,
                                                       fileExtension: fileExtension,
                                                       additionalInformation: additionalInformation);

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
        obj.FileExtension.Should().Be(fileExtension);
    }

    [Theory]
    [MemberData(nameof(AttachmentsTestsData.Create_WithAdditionalInformationAndPayload_InvalidData), MemberType = typeof(AttachmentsTestsData))]
    public void Create_WithAdditionalInformationAndFile_WhenInvalidDataProvided_ThenValidationFailureReturned(
        UserId userId, TransportId transportId, string fileExtension, string additionalInformation)
    {
        //Arrange
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, dateTimeProvider, 
                                                       fileExtension: fileExtension,
                                                       additionalInformation: additionalInformation);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Fact]
    public void Create_WithAdditionalInformation_WhenDependencyNotProvided_ThenUnexpectedFailureReturned()
    {
        //Arrange
        UserId userId = _domainFixture.Users.GetId();
        TransportId transportUnitId = _domainFixture.Transports.GetId();
        string additionalInformation = _domainFixture.Attachments.AdditionalInformation;

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportUnitId, null!, "jpg", additionalInformation);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Fact]
    public void AddScanId_WhenValidDataProvided_ThenScanIdIsUpdated()
    {
        //Arrange
        ScanId scanId = _domainFixture.Scans.GetId();
        TransportUnitId transportUnitId = _domainFixture.TransportUnits.GetId();

        Attachment sut = _domainFixture.Attachments.GetAttachment();

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
        Attachment sut = _domainFixture.Attachments.GetAttachment();

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
        TransportUnitId transportUnitId = _domainFixture.TransportUnits.GetId();
        Attachment sut = _domainFixture.Attachments.GetAttachment();

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
        Attachment sut = _domainFixture.Attachments.GetAttachment();

        //Act
        ErrorOr<Updated> result = sut.SetTransportUnit(transportUnitId);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }
}
