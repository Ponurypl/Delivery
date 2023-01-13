using FluentValidation.Validators;

namespace MultiProject.Delivery.WebApi.Common.Validators;

public class PrecisionScaleValidator<T> : PropertyValidator<T, double>
{
    private readonly int _scale;
    private readonly int _precision;
    public override string Name => "ScalePrecisionValidator";

    public PrecisionScaleValidator(int scale, int precision)
    {
        _scale = scale;
        _precision = precision;

        if (_scale < 0)
            throw new ArgumentOutOfRangeException(nameof(scale), $"Scale must be a positive integer. [value:{_scale}].");
        if (_precision < 0)
            throw new ArgumentOutOfRangeException(nameof(precision), $"Precision must be a positive integer. [value:{_precision}].");
        if (_precision < _scale)
            throw new ArgumentOutOfRangeException(nameof(scale), $"Scale must be less than precision. [scale:{_scale}, precision:{_precision}].");
    }

    public override bool IsValid(ValidationContext<T> context, double doubleValue)
    {
        var scale = GetScale(doubleValue);
        var precision = GetPrecision(doubleValue, scale);
        var actualIntegerDigits = precision - scale;
        var expectedIntegerDigits = _precision - _scale;
        if (scale > _scale || actualIntegerDigits > expectedIntegerDigits)
        {
            context.MessageFormatter
                   .AppendArgument("ExpectedPrecision", _precision)
                   .AppendArgument("ExpectedScale", _scale)
                   .AppendArgument("Digits", actualIntegerDigits < 0 ? 0 : actualIntegerDigits)
                   .AppendArgument("ActualScale", scale);

            return false;
        }

        return true;
    }

    private int GetScale(double number)
    {
        var i = 0;

        while (number != Math.Truncate(number))
        {
            number *= 10;
            i++;
        }

        return i;
    }

    private int GetPrecision(double number, int scale)
    {
        number *= Math.Pow(10, scale);

        return (int)Math.Truncate(Math.Log10(number)) + 1;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return Localized(errorCode, Name);
    }
}