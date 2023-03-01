using ErrorOr;
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
    public void Create_WhenValidDataProvidedWithPayload_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);
        byte[] payload = {1, 4, 6, 43 };

        UserId userId = new(Guid.Parse("0f0f1d91-37a5-4957-98fd-6e21f676be64"));
        TransportId transportId = new(2);

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
        //obj.Payload.Should().Be(payload);
        //TODO: assert dla byte[], nie da się out of box
    }

    [Fact]
    public void Create_WhenValidDataProvidedWithAditionalInformation_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);
        string aditionalinformation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

        UserId userId = new(Guid.Parse("0f0f1d91-37a5-4957-98fd-6e21f676be64"));
        TransportId transportId = new(2);

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, aditionalinformation, dateTimeProvider);

        //Assert
        result.IsError.Should().BeFalse();

        Attachment obj = result.Value;
        obj.Should().NotBeNull();
        obj.Status.Should().Be(AttachmentStatus.Valid);
        obj.CreatorId.Should().Be(userId);
        obj.LastUpdateDate.Should().Be(creationDate);
        obj.TransportId.Should().Be(transportId);
        obj.AdditionalInformation.Should().Be(aditionalinformation);
        obj.Id.Should().Be(AttachmentId.Empty);
        obj.TransportUnitId.Should().BeNull();        
        obj.ScanId.Should().BeNull();
        obj.Payload.Should().BeNull();
    }

    [Fact]
    public void Create_WhenValidDataProvidedWithAditionalInformationAndPayload_ThenNewObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 3, 1, 20, 7, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);
        string aditionalinformation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
        byte[] payload = {1, 4, 6, 43 };

        UserId userId = new(Guid.Parse("0f0f1d91-37a5-4957-98fd-6e21f676be64"));
        TransportId transportId = new(2);

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, payload, aditionalinformation, dateTimeProvider);

        //Assert
        result.IsError.Should().BeFalse();

        Attachment obj = result.Value;
        obj.Should().NotBeNull();
        obj.Status.Should().Be(AttachmentStatus.Valid);
        obj.CreatorId.Should().Be(userId);
        obj.LastUpdateDate.Should().Be(creationDate);
        obj.TransportId.Should().Be(transportId);
        obj.AdditionalInformation.Should().Be(aditionalinformation);
        obj.Id.Should().Be(AttachmentId.Empty);
        obj.TransportUnitId.Should().BeNull();
        obj.ScanId.Should().BeNull();
        //obj.Payload.Should().Be(payload);
        //TODO: assert dla byte[], nie działa out of box
    }

    [Theory]
    [InlineData("0f0f1d91-37a5-4957-98fd-6e21f676be64", 3, new byte[] { 1, 4, 6, 43 }, "")]
    [InlineData("00000000-0000-0000-0000-000000000000", 4, new byte[] { 1, 4, 6, 43 }, "Lorem ipsum dolor sit amet")]
    [InlineData("0f0f1d91-37a5-4957-98fd-6e21f676be64", 0, new byte[] { 1, 4, 6, 43 }, "Lorem ipsum dolor sit amet")]
    [InlineData("0f0f1d91-37a5-4957-98fd-6e21f676be64", 5, new byte[] { }, "Lorem ipsum dolor sit amet")]
    [InlineData("00000000-0000-0000-0000-000000000000", 0, new byte[] { }, "")]
    public void Create_WhenInvalidDataProvidedWithAditionalInformationAndPayload_ThenValidationFailureReturned(Guid guiduserId, int inttransportId, byte[] payload, string aditionalinformation)
    {
        //Arrange
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();

        UserId userId = new(guiduserId);
        TransportId transportId = new(inttransportId);

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportId, payload, aditionalinformation, dateTimeProvider);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Fact] // TODO: Czy ten test powinien przechodzić przez wszystkie rodzaje Attachment.Create? Dependency waliduje prywatny konstruktor
    public void Create_WhenDependencyNotProvided_ThenUnexpectedFailureReturned()
    {
        //Arrange
        UserId userId = DomainFixture.Users.GetRandomId;
        TransportId transportUnitId = DomainFixture.Transports.GetRandomId;
        string aditionalinformation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

        //Act
        ErrorOr<Attachment> result = Attachment.Create(userId, transportUnitId, aditionalinformation, null!);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Fact]
    public void AddScanId_WhenValidDataProvided_ThenScanIdIsUpdated()
    {
        //Arrange
        ScanId scanId = DomainFixture.Scans.GetRandomId;
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetRandomId;
        Attachment sut = DomainFixture.Attachments.GetAttachment();
        sut.AddTransportUnitId(transportUnitId);
        //Act
        ErrorOr<Updated> result = sut.AddScanId(scanId);

        //Assert
        result.IsError.Should().BeFalse();
        sut.Should().NotBeNull();
        sut.ScanId.Should().Be(scanId);
    }

    [Fact]
    public void AddScanId_WhenInvalidDataProvided_ThenValidationFailureIsReturned()
    {
        //Arrange
        ScanId scanId = DomainFixture.Scans.GetEmptyId;
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetRandomId;
        Attachment sut = DomainFixture.Attachments.GetAttachment();
        sut.AddTransportUnitId(transportUnitId);

        //Act
        ErrorOr<Updated> result = sut.AddScanId(scanId);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Fact] // TODO: Czy to kruchy test?
    public void AddScanId_WhenBussinesDependencyIsNotMeet_ThenValidationFailureIsReturned()
    {
        //Arrange
        ScanId scanId = DomainFixture.Scans.GetRandomId;
        Attachment sut = DomainFixture.Attachments.GetAttachment();

        //Act
        ErrorOr<Updated> result = sut.AddScanId(scanId);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }

    [Fact]
    public void AddTransportUnitId_WhenValidDataIsProvided_ThenTransportUnitIdIsUpdated()
    {
        //Arrange
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetRandomId;
        Attachment sut = DomainFixture.Attachments.GetAttachment();
        
        //Act
        ErrorOr<Updated> result = sut.AddTransportUnitId(transportUnitId);

        //Assert
        result.IsError.Should().BeFalse();
        sut.Should().NotBeNull();
        sut.TransportUnitId.Should().Be(transportUnitId);
    }


    [Fact]
    public void AddTransportUnitId_WhenInvalidDataIsProvided_ThenValidationFailureIsReturned()
    {
        //Arrange
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetEmptyId;
        Attachment sut = DomainFixture.Attachments.GetAttachment();

        //Act
        ErrorOr<Updated> result = sut.AddTransportUnitId(transportUnitId);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Attachments.InvalidAttachment);
    }
}
