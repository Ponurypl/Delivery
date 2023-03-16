using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Dictionaries.Queries.GetUnitOfMeasure;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Application.Tests.Integration.Dictionaries.Queries;

public class GetUnitOfMeasureQueryTests
{
    private readonly ContainerSetup _services;
    private readonly Mock<IUnitOfMeasureRepository> _repoMock;

    public GetUnitOfMeasureQueryTests()
    {
        _repoMock = new Mock<IUnitOfMeasureRepository>();
        _services = ContainerSetup.CreateNew()
                                  .AddDefaultValidators()
                                  .AddMediatR()
                                  .AddLogging()
                                  .AddScoped(Mock.Of<IUnitOfWork>())
                                  .AddScoped(_repoMock.Object);
    }

    [Fact]
    public async void GetUnitOfMeasureQuery_WhenValidDataProvided_ThenUnitOfMeasureReturned()
    {
        //Arrange
        const int unitId = 1;
        const string symbol = "kg";
        const string name = "kilogram";
        const string description = "SI unit of weight";
        _repoMock.Setup(s => s.GetByIdAsync(It.IsAny<UnitOfMeasureId>(), default))
                 .ReturnsAsync(UnitOfMeasure.Create(name,symbol,description).Value);

        IServiceProvider provider = _services.Build();
        ISender sender = provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<GetUnitOfMeasureDto> result = await sender.Send(new GetUnitOfMeasureQuery() { Id = unitId });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Description.Should().Be(description);
        result.Value.Name.Should().Be(name);
        result.Value.Symbol.Should().Be(symbol);
    }

    [Fact]
    public async void GetUnitOfMeasureQuery_WhenUnitOfMeasureWasNotFound_ThenFailureReturned()
    {
        //Arrange
        IServiceProvider provider = _services.Build();
        ISender sender = provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<GetUnitOfMeasureDto> result = await sender.Send(new GetUnitOfMeasureQuery() { Id = 1 });

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
        result.FirstError.Should().Be(Failure.UnitOfMeasureNotExists);
    }

}
