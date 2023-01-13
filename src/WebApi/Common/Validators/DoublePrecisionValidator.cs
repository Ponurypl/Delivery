using System.Globalization;

namespace MultiProject.Delivery.WebApi.Common.Validators;

public static class DoublePrecisionValidator
{
    //TODO: co trzeba by zrobić żeby użycie tej metody wpisywało się w swaggera, automagicznie jak np. GreaterThan() z fluenta?
    //Trzeba poczytać dokumentację fastendpointa/fluenta/nswaga, prawdopodobnie trzeba zaimplementować ten customowy validator w sposób "pełny" z dziedziczeniem itp.
    public static IRuleBuilder<T,double?> PrecisionScale<T> (this IRuleBuilder<T, double?> ruleBuilder, int precision, int scale)
    {
        return ruleBuilder.Must(d =>
        {
            //TODO: do przerobienia na pętlę z matematyką zamiast konwersją na stringa
            CultureInfo c = new("pl-PL");
            string[] number = d!.Value.ToString(c).Split(c.NumberFormat.NumberDecimalSeparator[0]);
            if (number.Length == 2)
            {
                if (number[0].Length + number[1].Length > precision) return false;
                if (number[1].Length > scale) return false;
                return true;
            }
            if (number.Length == 1)
            {
                if (number[0].Length > precision) return false;
                return true;
            }
            return false;
        }).WithMessage($"Number does not meet presion of {precision}, or scale of {scale}");

    }

    public static IRuleBuilder<T, double> PrecisionScale<T>(this IRuleBuilder<T, double> ruleBuilder, int precision, int scale)
    {
        return ruleBuilder.Must(d =>
        {
            //TODO: do przerobienia na pętlę z matematyką zamiast konwersją na stringa
            CultureInfo c = new("pl-PL");
            string[] number = d.ToString(c).Split(c.NumberFormat.NumberDecimalSeparator[0]);
            if (number.Length == 2)
            {
                if (number[0].Length + number[1].Length > precision) return false;
                if (number[1].Length > scale) return false;
                return true;
            }
            if (number.Length == 1)
            {
                if (number[0].Length > precision) return false;
                return true;
            }
            return false;
        }).WithMessage($"Number does not meet presion of {precision}, or scale of {scale}");

    }

}
