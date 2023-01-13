using MultiProject.Delivery.WebApi.Common.Validators;

namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUserLocation;

public class UpdateUserLocationValidator : Validator<UpdateUserLocationRequest>
{
    public UpdateUserLocationValidator()
    {
        RuleFor(x => x.Accuracy).GreaterThan(0).PrecisionScale(3,0);
        RuleFor(x => x.Heading).GreaterThanOrEqualTo(0).LessThan(360).PrecisionScale(4,2);
        RuleFor(x => x.Speed).GreaterThanOrEqualTo(0).PrecisionScale(4,2);
        RuleFor(x => x.Latitude).NotNull().PrecisionScale(4,2); 
        RuleFor(x => x.Longitude).PrecisionScale(4, 2);
        //TODO: ymm nie mogę PrecisionScale użyć jako pierwszego, a w sumie chyba nie ma po co sprawdzać notnull?
        //TODO: czy zrobienie scali każdego pola jako global static, nie byłoby tu dobrym pomysłem?
        //Jeśli trzeba będzie zmienić precyzję pola z 4,2 na 5,2 to trzeba będzie odszukać wszytkie validatory które to sprawdzają i je poprawiać.
    }
}
