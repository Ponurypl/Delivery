using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Tests.Integration.Data;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Application.Users.Queries.GetUser;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Tests.Integration.Users.Queries;
public class GetUserQueryTests
{
    private readonly IServiceProvider _provider;
    private readonly Mock<IUserRepository> _repoMock;
    private readonly DomainFixture _fixture = new();

    public GetUserQueryTests()
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
    public async void GetUserQuery_WhenValidDataProvided_ThenUserReturned()
    {
        //Arrange
        User user = _fixture.Users.GetUser();
        _repoMock.Setup(s => s.GetByIdAsync(It.Is<UserId>(id => id == user.Id), It.IsAny<CancellationToken>())).ReturnsAsync(user);

        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<UserDto> result = await sender.Send(new GetUserQuery() { UserId = user.Id.Value });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.PhoneNumber.Should().Be(user.PhoneNumber);
        result.Value.Id.Should().Be(user.Id.Value);
        result.Value.Role.Should().Be((int)user.Role);
        result.Value.Username.Should().Be(user.Username);
    }

    [Fact]
    public async void GetUserQuery_WhenQueryUserNotExists_ThenFailureReturned()
    {
        //Arrange
        UserId id = _fixture.Users.GetId();
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<UserDto> result = await sender.Send(new GetUserQuery() { UserId = id.Value });

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
        result.FirstError.Should().Be(Failure.UserNotExists);
    }

    [Theory]
    [ClassData(typeof(GetUserQueryWrongData))]
    public async void GetUserQuery_WhenInvalidDataProvided_ThenFailureReturned(Guid id)
    {
        //Arrange
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<UserDto> result = await sender.Send(new GetUserQuery() { UserId = id });

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(Failure.InvalidMessage);
    }
}
