using System.Reflection;

namespace MultiProject.Delivery.Domain.Common.Extensions;

public static class EnumExtensions
{
    public static bool IsValidForEnum<TEnum>(this TEnum value)
        where TEnum : struct
    {
        var underlyingEnumType = typeof(TEnum);
        if (underlyingEnumType.GetCustomAttribute<FlagsAttribute>() is null)
        {
            return Enum.IsDefined(underlyingEnumType, value);
        }

        var standardizedValue = Convert.ToInt32(value);
        int mask = 0;
        foreach (var enumValue in Enum.GetValues(underlyingEnumType))
        {
            var enumValueAsInt32 = Convert.ToInt32(enumValue);
            if ((enumValueAsInt32 & standardizedValue) == enumValueAsInt32)
            {
                mask |= enumValueAsInt32;
                if (mask == standardizedValue)
                    return true;
            }
        }

        return false;
    }
}
