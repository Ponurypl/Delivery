﻿using MultiProject.Delivery.Domain.Common.DateTimeProvider;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

public class CreateTransportValidator : Validator<CreateTransportRequest>
{
    public CreateTransportValidator(IDateTime dateTime)
    {

        RuleFor(x => x.DelivererId).NotEmpty();
        RuleFor(x => x.ManagerId).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty().GreaterThan(dateTime.UtcNow);
        RuleFor(x => x.Number).NotEmpty();

        RuleFor(x => x.TransportUnits).NotEmpty().WithMessage("At least One TransportUnit must be specified in delivery");
        RuleForEach(x => x.TransportUnits).SetValidator(new TransportUnitValidator());

        RuleFor(x => x.TotalWeight).GreaterThan(0).When(x => x.TotalWeight is not null);
    }
}
