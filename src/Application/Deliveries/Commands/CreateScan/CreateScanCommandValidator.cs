﻿namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed class CreateScanCommandValidator : AbstractValidator<CreateScanCommand>
{
    public CreateScanCommandValidator()
    {
        RuleFor(x => x.DelivererId).NotEmpty();
        RuleFor(x => x.TransportId).NotEmpty();
        RuleFor(x => x.TransportUnitId).NotEmpty();
        RuleFor(x => x.Quantity).PrecisionScale(8, 3);
        RuleFor(x => x.Location).SetValidator(new ScanGeolocationValidator()).When(x => x.Location is not null);
    }
}
