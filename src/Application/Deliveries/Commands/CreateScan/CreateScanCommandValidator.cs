namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed class CreateScanCommandValidator : AbstractValidator<CreateScanCommand>
{
    public CreateScanCommandValidator()
    {
        RuleFor(x => x.DelivererId).NotEmpty();
    }
}
