using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Recipient, RecipientDto>();
        config.NewConfig<TransportUnit, TransportUnitDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.Status, s => s.Status.ToString())
              .Map(d => d.Barcode, s => s.UniqueUnitDetails!.Barcode)
              .Map(d => d.UnitOfMeasureId, s => s.MultiUnitDetails!.UnitOfMeasureId.Value)
              .Map(d => d.Amount, s => s.MultiUnitDetails!.Amount)
              .Ignore(d => d.Scans)
              .Ignore(d => d.Attachments);

        config.NewConfig<Transport, TransportDetailsDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.Status, s => s.Status.ToString())
              .Map(d => d.DelivererId, s => s.DelivererId.Value)
              .Map(d => d.ManagerId, s => s.ManagerId.Value)
              .Ignore(d => d.Attachments)
              .Ignore(d => d.TransportUnits);

    }
}