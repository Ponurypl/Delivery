using System.Reflection.Metadata.Ecma335;

namespace MultiProject.Delivery.Domain.Common.Extensions;
public static class EnumExtensions
{
    public static bool IsValidForEnum<TEnum>(this TEnum enumValue)
        where TEnum : struct
    {
        char firstChar = enumValue.ToString()[0];
        return (firstChar < '0' || firstChar > '9') && firstChar != '-';
    }
}
