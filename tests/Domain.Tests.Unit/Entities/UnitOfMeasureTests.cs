using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Data;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit.Entities;

public class UnitOfMeasureTests
{
    private readonly DomainFixture _domainFixture = new();
    [Theory]
    [MemberData(nameof(UnitOfMeasureTestsData.Create_InvalidData), MemberType = typeof(UnitOfMeasureTestsData))]
    public void Create_WhenRequiredParametersAreNullOrEmpty_ThenFailureIsReturned(string name, string symbol)
    {
        //Arrange

        //Act
        var result = UnitOfMeasure.Create(name, symbol, null);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Dictionaries.InvalidUnitOfMeasure);
    }

    [Fact]
    public void Create_WhenValidDataProvided_ThenNewValidObjectCreated()
    {
        //Arrange
        var name = _domainFixture.UnitOfMeasures.Name;
        var symbol = _domainFixture.UnitOfMeasures.Symbol;
        var description = _domainFixture.UnitOfMeasures.Description;

        //Act
        var result = UnitOfMeasure.Create(name, symbol, description);

        //Assert
        result.IsError.Should().BeFalse();

        var obj = result.Value;
        obj.Should().NotBeNull();
        obj.Name.Should().Be(name);
        obj.Symbol.Should().Be(symbol);
        obj.Description.Should().Be(description);
        obj.Id.Should().Be(UnitOfMeasureId.Empty);
    }
}