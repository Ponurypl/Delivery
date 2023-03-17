using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Application.Tests.Integration.Dictionaries.Commands;

public class CreateUnitOfMeasureCommandTest
{
    private readonly IServiceProvider _provider;
    private readonly Mock<IUnitOfMeasureRepository> _repoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateUnitOfMeasureCommandTest()
    {
        _repoMock = new Mock<IUnitOfMeasureRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _provider = ContainerSetup.CreateNew()
                                  .AddMediatR()
                                  .AddLogging()
                                  .AddScoped(_unitOfWorkMock.Object)
                                  .AddScoped(_repoMock.Object)
                                  .Build();
    }

    [Fact]
    public async void CreateUnitOfMeasureCommand_WhenValidDataProvided_ThenReturnsId()
    {
        //Arrange
        const int unitId = 1;
        _repoMock.Setup(s => s.Add(It.IsAny<UnitOfMeasure>()))
                .Callback<UnitOfMeasure>(unit => unit.SetId(new UnitOfMeasureId(unitId)));
        
        var sender = _provider.GetRequiredService<ISender>();

        //Act
        var result = await sender.Send(new CreateUnitOfMeasureCommand() { Name = "abc", Symbol = "abc" });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be(unitId);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("", "ABC")]
    [InlineData("DEF", "")]
    public async void CreateUnitOfMeasureCommand_WhenInvalidDataProvided_ThenFailureReturned(string name, string symbol)
    {
        //Arrange
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<UnitOfMeasureCreatedDto> result = await sender.Send(new CreateUnitOfMeasureCommand() { Name = name, Symbol = symbol });

        //Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().AllSatisfy(x =>
                                          {
                                              x.Type.Should().Be(ErrorType.Validation);
                                              x.Should().Be(Failure.InvalidMessage);
                                          });

        _unitOfWorkMock.Verify(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}