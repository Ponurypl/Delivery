using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Application.Users.Commands.CreateUser;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;
using MultiProject.Delivery.Application.Common.Cryptography;

namespace MultiProject.Delivery.Application.Tests.Integration.Users.Commands;
public class CreateUserCommandTests
{
    private readonly ContainerSetup _services;
    private readonly Mock<IUserRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IHashService> _hashService;

    public CreateUserCommandTests()
    {
        _repoMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _hashService = new Mock<IHashService>();
        _services = ContainerSetup.CreateNew()
                                  .AddDefaultValidators()
                                  .AddMediatR()
                                  .AddLogging()
                                  .AddTransient(_hashService.Object)
                                  .AddScoped(_unitOfWorkMock.Object)
                                  .AddScoped(_repoMock.Object);
    }
    [Fact]
    public async void CreateUserCommand_WhenValidDataProvided_ThenReturnsId()
    {
        //Arrange
        Guid userId = new("42e47ada-378d-4d82-8e67-8e198962c670");
        _repoMock.Setup(x => x.Add(It.IsAny<User>())).Callback<User>(u => u.SetId(new UserId(userId)));
        _hashService.Setup(s => s.Hash(It.IsAny<string>())).Returns("hashedPassword");

        IServiceProvider provider = _services.Build();
        ISender sender = provider.GetRequiredService<ISender>();

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
        result.Value.Id.Should().Be(userId);
        _repoMock.Verify(v => v.Add(It.IsAny<User>()),Times.Once);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        _hashService.Verify(v => v.Hash(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async void CreateUserCommand_WhenUSerExists_ThenFailureReturned()
    {
        //Arrange
        _repoMock.Setup(s => s.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(User.Create(Domain.Users.Enums.UserRole.Manager, "username", "password", "123456789").Value);

        IServiceProvider provider = _services.Build();
        ISender sender = provider.GetRequiredService<ISender>();

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
        _repoMock.Verify(v => v.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        _repoMock.Verify(v => v.Add(It.IsAny<User>()), Times.Never);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [ClassData(typeof(UserCreateCommandWrongData))]
    public async void CreateUserCommand_WhenInvalidDataProvided_ThenFailureReturned(string username, string password, string phoneNumber, UserRole role)
    {
        //Arrange
        IServiceProvider provider = _services.Build();
        ISender sender = provider.GetRequiredService<ISender>();

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
        _repoMock.Verify(v => v.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        _repoMock.Verify(v => v.Add(It.IsAny<User>()), Times.Never);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
