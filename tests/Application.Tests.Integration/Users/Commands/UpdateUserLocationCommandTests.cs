using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Tests.Integration.Data;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Tests.Integration.Users.Commands;
public class UpdateUserLocationCommandTests
{
    private readonly IServiceProvider _provider;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _repositoryMock;

    public UpdateUserLocationCommandTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repositoryMock = new Mock<IUserRepository>();
        _provider = ContainerSetup.CreateNew()
                                  .AddMediatR()
                                  .AddLogging()
                                  .AddTransient(Mock.Of<IDateTime>())
                                  .AddScoped(_unitOfWorkMock.Object)
                                  .AddScoped(_repositoryMock.Object)
                                  .Build();

    }

    [Fact]
    public async void UpdateUserLocationCommand_WhenValidDataProvided_ThenSuccessReturned()
    {
        //Arrange
        var user = DomainFixture.Users.GetUser();
        _repositoryMock.Setup(s => s.GetByIdAsync(It.Is<UserId>(id => id == user.Id), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(DomainFixture.Users.GetUser());
        
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<Success> result = await sender.Send(new UpdateUserLocationCommand
                                                    {
                                                        Accuracy = 1d,
                                                        Heading = 1d,
                                                        Latitude = 1d,
                                                        Longitude = 1d,
                                                        Speed = 1d,
                                                        UserId = user.Id.Value
                                                    });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<Success>();
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.AtLeastOnce);
    }

    [Fact]
    public async void UpdateUserLocationCommand_WhenUserNotExists_ThenFailureReturned()
    {
        //Arrange
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<Success> result = await sender.Send(new UpdateUserLocationCommand()
                                                    {
                                                        Accuracy = 1d,
                                                        Heading = 1d,
                                                        Latitude = 1d,
                                                        Longitude = 1d,
                                                        Speed = 1d,
                                                        UserId = Guid.Parse("42e47ada-378d-4d82-8e67-8e198962c670")
                                                    });

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
        result.FirstError.Should().Be(Failure.UserNotExists);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [ClassData(typeof(UpdateUserLocationCommandWrongData))]
    public async void UpdateUserLocationCommand_WhenInvalidDataProvided_ThenFailureReturned(
        double accuracy, double heading, double latitude,
        double longitude, double speed, Guid userId)
    {
        //Arrange
        _repositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(DomainFixture.Users.GetUser());

        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<Success> result = await sender.Send(new UpdateUserLocationCommand()
                                                    {
                                                        Accuracy = accuracy,
                                                        Heading = heading,
                                                        Latitude = latitude,
                                                        Longitude = longitude,
                                                        Speed = speed,
                                                        UserId = userId
                                                    });

        //Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().AllSatisfy(s => s.Type.Should().Be(ErrorType.Validation));
        result.Errors.Should().AllSatisfy(s => s.Should().Be(Failure.InvalidMessage));
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
