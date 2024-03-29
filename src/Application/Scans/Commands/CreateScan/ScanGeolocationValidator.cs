﻿namespace MultiProject.Delivery.Application.Scans.Commands.CreateScan;

public sealed class ScanGeolocationValidator : AbstractValidator<ScanGeolocation>
{
    public ScanGeolocationValidator()
    {
        RuleFor(x => x.Accuracy).GreaterThan(0).PrecisionScale(3, 0);
        RuleFor(x => x.Latitude).NotEmpty().PrecisionScale(8, 5);
        RuleFor(x => x.Longitude).NotEmpty().PrecisionScale(8, 5);
    }
}
