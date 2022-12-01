using FluentValidation;

namespace MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;
internal class CreateUnitOfMeasureCommandValidator : AbstractValidator<CreateUnitOfMeasureCommand>
{
    public CreateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Symbol).NotEmpty();
    }
}
