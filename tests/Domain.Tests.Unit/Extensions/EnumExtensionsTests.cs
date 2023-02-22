using MultiProject.Delivery.Domain.Common.Extensions;

namespace MultiProject.Delivery.Domain.Tests.Unit.Extensions;
public class EnumExtensionsTests
{
    [Flags]
    enum WithFlags
    {
        First = 1,
        Second = 2,
        Third = 4,
        Fourth = 8
    }

    enum WithoutFlags
    {
        First = 1,
        Second = 22,
        Third = 55,
        Fourth = 13,
        Fifth = 127
    }

    enum WithoutNumbers
    {
        First, // 1
        Second, // 2
        Third, // 3
        Fourth // 4
    }

    enum WithoutFirstNumberAssigned
    {
        First = 7,
        Second, // 8
        Third, // 9
        Fourth // 10
    }


    enum WithNagativeNumbers
    {
        First = -7,
        Second = -8,
        Third = -9,
        Fourth = -10
    }

    [Fact]
    public void IsValidEnumTests()
    {
        //Arrange

        //Act

        //Assert
        ((WithFlags)(1 | 4)).IsValidForEnum().Should().Be(true);
        ((WithFlags)(1 | 4)).IsValidForEnum().Should().Be(true);
        ((WithFlags)(1 | 4 | 2)).IsValidForEnum().Should().Be(true);
        ((WithFlags)(2)).IsValidForEnum().Should().Be(true);
        ((WithFlags)(3)).IsValidForEnum().Should().Be(true);
        ((WithFlags)(1 + 2 + 4 + 8)).IsValidForEnum().Should().Be(true);
        ((WithFlags)(12)).IsValidForEnum().Should().Be(true);

        ((WithFlags)(16)).IsValidForEnum().Should().Be(false);
        ((WithFlags)(17)).IsValidForEnum().Should().Be(false);
        ((WithFlags)(18)).IsValidForEnum().Should().Be(false);
        ((WithFlags)(0)).IsValidForEnum().Should().Be(false);

        ((WithoutFlags)1).IsValidForEnum().Should().Be(true);
        ((WithoutFlags)22).IsValidForEnum().Should().Be(true);
        ((WithoutFlags)(53 | 6)).IsValidForEnum().Should().Be(true);   // Will end up being Third
        ((WithoutFlags)(22 | 25 | 99)).IsValidForEnum().Should().Be(true); // Will end up being Fifth
        ((WithoutFlags)55).IsValidForEnum().Should().Be(true);
        ((WithoutFlags)127).IsValidForEnum().Should().Be(true);

        ((WithoutFlags)48).IsValidForEnum().Should().Be(false);
        ((WithoutFlags)50).IsValidForEnum().Should().Be(false);
        ((WithoutFlags)(1 | 22)).IsValidForEnum().Should().Be(false);
        ((WithoutFlags)(9 | 27 | 4)).IsValidForEnum().Should().Be(false);

        ((WithoutNumbers)0).IsValidForEnum().Should().Be(true);
        ((WithoutNumbers)1).IsValidForEnum().Should().Be(true);
        ((WithoutNumbers)2).IsValidForEnum().Should().Be(true);
        ((WithoutNumbers)3).IsValidForEnum().Should().Be(true);
        ((WithoutNumbers)(1 | 2)).IsValidForEnum().Should().Be(true); // Will end up being Third
        ((WithoutNumbers)(1 + 2)).IsValidForEnum().Should().Be(true); // Will end up being Third

        ((WithoutNumbers)4).IsValidForEnum().Should().Be(false);
        ((WithoutNumbers)5).IsValidForEnum().Should().Be(false);
        ((WithoutNumbers)25).IsValidForEnum().Should().Be(false);
        ((WithoutNumbers)(1 + 2 + 3)).IsValidForEnum().Should().Be(false);

        ((WithoutFirstNumberAssigned)7).IsValidForEnum().Should().Be(true);
        ((WithoutFirstNumberAssigned)8).IsValidForEnum().Should().Be(true);
        ((WithoutFirstNumberAssigned)9).IsValidForEnum().Should().Be(true);
        ((WithoutFirstNumberAssigned)10).IsValidForEnum().Should().Be(true);

        ((WithoutFirstNumberAssigned)11).IsValidForEnum().Should().Be(false);
        ((WithoutFirstNumberAssigned)6).IsValidForEnum().Should().Be(false);
        ((WithoutFirstNumberAssigned)(7 | 9)).IsValidForEnum().Should().Be(false);
        ((WithoutFirstNumberAssigned)(8 + 10)).IsValidForEnum().Should().Be(false);

        ((WithNagativeNumbers)(-7)).IsValidForEnum().Should().Be(true);
        ((WithNagativeNumbers)(-8)).IsValidForEnum().Should().Be(true);
        ((WithNagativeNumbers)(-9)).IsValidForEnum().Should().Be(true);
        ((WithNagativeNumbers)(-10)).IsValidForEnum().Should().Be(true);

        ((WithNagativeNumbers)(-11)).IsValidForEnum().Should().Be(false);
        ((WithNagativeNumbers)(7)).IsValidForEnum().Should().Be(false);
        ((WithNagativeNumbers)(8)).IsValidForEnum().Should().Be(false);
    }
}
