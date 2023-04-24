using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Tests.Integration.Data;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Application.Users.Queries.GetUserLocation;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Tests.Integration.Users.Queries;
public class GetUserLocationQueryTests
{
    private readonly IServiceProvider _provider;
    private readonly Mock<IUserRepository> _repoMock;
    private readonly DomainFixture _fixture = new();

    public GetUserLocationQueryTests()
    {
        _repoMock = new Mock<IUserRepository>();
        _provider = ContainerSetup.CreateNew()
                                  .AddMediatR()
                                  .AddMapper()
                                  .AddLogging()
                                  .AddScoped(_repoMock.Object)
                                  .Build();
    }

    [Fact]
    public async void GetUserLocationQuery_WhenValidDataProvided_ThenUserLocationReturned()
    {
        //Assert
        User user = _fixture.Users.GetUser();
        _repoMock.Setup(s => s.GetByIdAsync(It.Is<UserId>(id => id == user.Id), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(user);

        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<GetUserLocationDto> result = await sender.Send(new GetUserLocationQuery() { UserId = user.Id.Value });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Accuracy.Should().Be(user.Location!.Accuracy);
        result.Value.Heading.Should().Be(user.Location.Heading);
        result.Value.LastUpdateDate.Should().Be(user.Location.LastUpdateDate);
        result.Value.Latitude.Should().Be(user.Location.Latitude);
        result.Value.Longitude.Should().Be(user.Location.Longitude);
        result.Value.Speed.Should().Be(user.Location.Speed);
    }

    [Fact]
    public async void GetUserLocationQuery_WhenUserNotFound_ThenFailureReturned()
    {
        //Assert
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<GetUserLocationDto> result = await sender.Send(new GetUserLocationQuery() { UserId = _fixture.Users.GetId().Value });

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
        result.FirstError.Should().Be(Failure.UserNotExists);
    }

    [Theory]
    [ClassData(typeof(GetUserLocationQueryWrongData))]
    public async void GetUserLocationQuery_WhenInValidDataProvided_ThenFailureReturned(Guid id)
    {
        //Assert
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<GetUserLocationDto> result = await sender.Send(new GetUserLocationQuery() { UserId = id });

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(Failure.InvalidMessage);
    }
}
