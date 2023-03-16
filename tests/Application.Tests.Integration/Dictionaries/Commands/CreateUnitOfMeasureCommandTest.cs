using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Application.Tests.Integration.Dictionaries.Commands;

public class CreateUnitOfMeasureCommandTest
{
    private readonly ContainerSetup _services;
    private readonly Mock<IUnitOfMeasureRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateUnitOfMeasureCommandTest()
    {
        _repoMock = new Mock<IUnitOfMeasureRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _services = ContainerSetup.CreateNew()
                                  .AddDefaultValidators()
                                  .AddMediatR()
                                  .AddLogging()
                                  .AddScoped(_unitOfWorkMock.Object)
                                  .AddScoped(_repoMock.Object);
    }

    [Fact]
    public async void CreateUnitOfMeasureCommand_WhenValidDataProvided_ThenReturnsId()
    {
        //Arrange
        const int unitId = 1;
        _repoMock.Setup(s => s.Add(It.IsAny<UnitOfMeasure>()))
                .Callback<UnitOfMeasure>(unit => unit.SetId(new UnitOfMeasureId(unitId)));

        var provider = _services.Build();
        var sender = provider.GetRequiredService<ISender>();

        //Act
        var result = await sender.Send(new CreateUnitOfMeasureCommand() { Name = "abc", Symbol = "abc" });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be(unitId);
        _repoMock.Verify(v => v.Add(It.IsAny<UnitOfMeasure>()), Times.Once);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.AtLeastOnce);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null!, null!)]
    [InlineData(" ", " ")]
    [InlineData("", "ABC")]
    [InlineData("  ", "ABC")]
    [InlineData(null!, "ABC")]
    [InlineData("DEF", "")]
    [InlineData("DEF", "  ")]
    [InlineData("DEF", null!)]
    public async void CreateUnitOfMeasureCommand_WhenInvalidDataProvided_ThenFailureReturned(string name, string symbol)
    {
        //Arrange
        IServiceProvider provider = _services.Build();
        ISender sender = provider.GetService<ISender>()!;

        //Act
        ErrorOr<UnitOfMeasureCreatedDto> result = await sender.Send(new CreateUnitOfMeasureCommand() { Name = name, Symbol = symbol });

        //Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().AllSatisfy(x =>
                                          {
                                              x.Type.Should().Be(ErrorType.Validation);
                                              x.Should().Be(Failure.InvalidMessage);
                                          });
        _repoMock.Verify(v => v.Add(It.IsAny<UnitOfMeasure>()), Times.Never);
        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}