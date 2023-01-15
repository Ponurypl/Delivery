namespace MultiProject.Delivery.Application.Common.Validators;

public static class DoubleValidationExtensions
{
    /// <summary>
    /// Allows a double to be validated for scale and precision.
    /// Scale would be the number of digits to the right of the double point.
    /// Precision would be the number of digits. This number includes both the left and the right sides of the double point.
    ///
    /// 123.4500 has an scale of 4 and a precision of 7, but an effective scale
    /// and precision of 2 and 5 respectively.
    /// </summary>
    public static IRuleBuilder<T, double?> PrecisionScale<T>(this IRuleBuilder<T, double?> ruleBuilder, int precision,
                                                             int scale)
    {
        return ruleBuilder.SetValidator(new PrecisionScaleValidator<T>(scale, precision));
    }

    /// <summary>
    /// Allows a double to be validated for scale and precision.
    /// Scale would be the number of digits to the right of the double point.
    /// Precision would be the number of digits. This number includes both the left and the right sides of the double point.
    ///
    /// 123.4500 has an scale of 4 and a precision of 7, but an effective scale
    /// and precision of 2 and 5 respectively.
    /// </summary>
    public static IRuleBuilder<T, double> PrecisionScale<T>(this IRuleBuilder<T, double> ruleBuilder, int precision,
                                                             int scale)
    {
        return ruleBuilder.SetValidator(new PrecisionScaleValidator<T>(scale, precision));
    }
}