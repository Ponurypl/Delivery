using MultiProject.Delivery.Domain.Common.Extensions;
using MultiProject.Delivery.Domain.Tests.Unit.Data;

namespace MultiProject.Delivery.Domain.Tests.Unit.Extensions;

public class EnumExtensionsTests
{
    [Theory]
    [InlineData(1 | 4, true)]
    [InlineData(1 | 4 | 2, true)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(1 + 2 + 4 + 8, true)]
    [InlineData(12, true)]
    [InlineData(16, false)]
    [InlineData(17, false)]
    [InlineData(18, false)]
    [InlineData(0, false)]
    public void IsValidEnum_WhenEnumHasFlagsAttribute(int value, bool expectedResult)
    {
        //Arrange

        //Act
        var result = ((WithFlags)(value)).IsValidForEnum();

        //Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(22, true)]
    [InlineData(53 | 6, true)]
    [InlineData(22 | 25 | 99, true)]
    [InlineData(48, false)]
    [InlineData(50, false)]
    [InlineData(1 | 22, false)]
    [InlineData(9 | 27 | 4, false)]
    public void IsValidEnum_WhenEnumHasNoFlagsAttribute(int value, bool expectedResult)
    {
        //Arrange

        //Act
        var result = ((WithoutFlags)(value)).IsValidForEnum();

        //Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, false)]
    [InlineData(25, false)]
    [InlineData(1 + 2 + 3, false)]
    public void IsValidEnum_WhenEnumHasNoFlagsAttributeAndNoNumberValuesDeclared(int value, bool expectedResult)
    {
        //Arrange

        //Act
        var result = ((WithoutNumbers)(value)).IsValidForEnum();

        //Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(7, true)]
    [InlineData(8, true)]
    [InlineData(9, true)]
    [InlineData(10, true)]
    [InlineData(11, false)]
    [InlineData(6, false)]
    [InlineData(7 | 9, false)]
    [InlineData(8 + 10, false)]
    public void IsValidEnum_WhenEnumHasNoFlagsAttributeAndOnlyFirstNumberValueDeclared(int value, bool expectedResult)
    {
        //Arrange

        //Act
        var result = ((WithoutFirstNumberAssigned)(value)).IsValidForEnum();

        //Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(-7, true)]
    [InlineData(-8, true)]
    [InlineData(-9, true)]
    [InlineData(-10, true)]
    [InlineData(-11, false)]
    [InlineData(7, false)]
    [InlineData(8, false)]
    public void IsValidEnum_WhenEnumHasNoFlagsAttributeAndDeclaredNumberValuesAreNegative(int value, bool expectedResult)
    {
        //Arrange

        //Act
        var result = ((WithNegativeNumbers)(value)).IsValidForEnum();

        //Assert
        result.Should().Be(expectedResult);
    }
}
