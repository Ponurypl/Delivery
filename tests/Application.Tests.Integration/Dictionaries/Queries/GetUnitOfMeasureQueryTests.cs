using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Dictionaries.Queries.GetUnitOfMeasure;
using MultiProject.Delivery.Application.Tests.Integration.Helpers;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Application.Tests.Integration.Dictionaries.Queries;

public class GetUnitOfMeasureQueryTests
{
    private readonly IServiceProvider _provider;
    private readonly Mock<IUnitOfMeasureRepository> _repoMock;

    public GetUnitOfMeasureQueryTests()
    {
        _repoMock = new Mock<IUnitOfMeasureRepository>();
        _provider = ContainerSetup.CreateNew()
                                  .AddMediatR()
                                  .AddLogging()
                                  .AddScoped(Mock.Of<IUnitOfWork>())
                                  .AddScoped(_repoMock.Object)
                                  .Build();
    }

    [Fact]
    public async void GetUnitOfMeasureQuery_WhenValidDataProvided_ThenUnitOfMeasureReturned()
    {
        //Arrange
        var unitOfMeasure = DomainFixture.UnitOfMeasures.GetUnitOfMeasure();
        _repoMock.Setup(s => s.GetByIdAsync(It.IsAny<UnitOfMeasureId>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(unitOfMeasure);
        
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<GetUnitOfMeasureDto> result = await sender.Send(new GetUnitOfMeasureQuery() { Id = unitOfMeasure.Id.Value });

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Description.Should().Be(unitOfMeasure.Description);
        result.Value.Name.Should().Be(unitOfMeasure.Name);
        result.Value.Symbol.Should().Be(unitOfMeasure.Symbol);
    }

    [Fact]
    public async void GetUnitOfMeasureQuery_WhenUnitOfMeasureWasNotFound_ThenFailureReturned()
    {
        //Arrange
        ISender sender = _provider.GetRequiredService<ISender>();

        //Act
        ErrorOr<GetUnitOfMeasureDto> result = await sender.Send(new GetUnitOfMeasureQuery { Id = 1 });

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
        result.FirstError.Should().Be(Failure.UnitOfMeasureNotExists);
    }

}
