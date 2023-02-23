namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

[Flags]
public enum WithFlags
{
    First = 1,
    Second = 2,
    Third = 4,
    Fourth = 8
}

public enum WithoutFlags
{
    First = 1,
    Second = 22,
    Third = 55,
    Fourth = 13,
    Fifth = 127
}

public enum WithoutNumbers
{
    First, // 1
    Second, // 2
    Third, // 3
    Fourth // 4
}

public enum WithoutFirstNumberAssigned
{
    First = 7,
    Second, // 8
    Third, // 9
    Fourth // 10
}

public enum WithNagativeNumbers
{
    First = -7,
    Second = -8,
    Third = -9,
    Fourth = -10
}