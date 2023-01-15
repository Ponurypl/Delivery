namespace MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;

public sealed class CreateUnitOfMeasureCommandValidator : AbstractValidator<CreateUnitOfMeasureCommand>
{
    public CreateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Symbol).NotEmpty();
    }
}
