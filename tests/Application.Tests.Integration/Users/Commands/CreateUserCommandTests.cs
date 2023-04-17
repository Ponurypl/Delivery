using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Application.Users.Commands.CreateUser;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Application.Common.Cryptography;
using MultiProject.Delivery.Application.Tests.Integration.Data;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Application.Tests.Integration.Users.Commands;
public class CreateUserCommandTests
{
    private readonly IServiceProvider _provider;
    private readonly Mock<IUserRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IHashService> _hashService;

    public CreateUserCommandTests()
    {
        _repoMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _hashService = new Mock<IHashService>();
        _provider = ContainerSetup.CreateNew()
                                  .AddMediatR()
                                  .AddLogging()
                                  .AddTransient(_hashService.Object)
                                  .AddScoped(_unitOfWorkMock.Object)
                                  .AddScoped(_repoMock.Object)
                                  .Build();
    }
    [Fact]
    public async void CreateUserCommand_WhenValidDataProvided_ThenReturnsId()
    {
        //Arrange
        var userId = DomainFixture.Users.GetId();
        _repoMock.Setup(x => x.Add(It.IsAny<User>())).Callback<User>(u => u.SetId(userId));
        _hashService.Setup(s => s.Hash(It.IsAny<string>())).Returns("hashedPassword");
        
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<UserCreatedDto> result = await sender.Send(new CreateUserCommand()
                                                           {
                                                               Password = "password",
                                                               PhoneNumber = "123456789",
                                                               Username = "username",
                                                               Role = UserRole.Deliverer | UserRole.Manager
                                                           });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be(userId.Value);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact]
    public async void CreateUserCommand_WhenUserExists_ThenFailureReturned()
    {
        //Arrange
        _repoMock.Setup(s => s.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(DomainFixture.Users.GetUser());
        
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<UserCreatedDto> result = await sender.Send(new CreateUserCommand()
                                                           {
                                                               Password = "password",
                                                               PhoneNumber = "123456789",
                                                               Username = "username",
                                                               Role = UserRole.Deliverer | UserRole.Manager
                                                           });
        
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Conflict);
        result.FirstError.Should().Be(Failure.UserNameTaken);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [ClassData(typeof(UserCreateCommandWrongData))]
    public async void CreateUserCommand_WhenInvalidDataProvided_ThenFailureReturned(string username, string password, string phoneNumber, UserRole role)
    {
        //Arrange
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<UserCreatedDto> result = await sender.Send(new CreateUserCommand()
                                                           {
                                                               Password = password,
                                                               PhoneNumber = phoneNumber,
                                                               Username = username,
                                                               Role = role
                                                           });

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(Failure.InvalidMessage);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
