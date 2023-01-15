namespace MultiProject.Delivery.Application.Common.Validators;

public static class DoubleValidationExtensions
{
    public static IRuleBuilder<T, double?> PrecisionScale<T>(this IRuleBuilder<T, double?> ruleBuilder, int precision,
                                                             int scale)
    {
        return ruleBuilder.SetValidator(new PrecisionScaleValidator<T>(scale, precision));
    }
    
    public static IRuleBuilder<T, double> PrecisionScale<T>(this IRuleBuilder<T, double> ruleBuilder, int precision,
                                                             int scale)
    {
        return ruleBuilder.SetValidator(new PrecisionScaleValidator<T>(scale, precision));
    }
}