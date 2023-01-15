namespace MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;

public sealed class CreateUnitOfMeasureCommandValidator : AbstractValidator<CreateUnitOfMeasureCommand>
{
    public CreateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Symbol).NotEmpty().MaximumLength(5);
        RuleFor(x => x.Description).MaximumLength(200);
    }
}
