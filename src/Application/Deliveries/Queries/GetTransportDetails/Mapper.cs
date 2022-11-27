using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Interfaces;
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
              .Map(d => d.Barcode, s => GetBarcode(s.UnitDetails))
              .Map(d => d.UnitOfMeasureId, s => GetUnitOfMeasureId(s.UnitDetails))
              .Map(d => d.Amount, s => GetAmount(s.UnitDetails))
              .Ignore(d => d.Scans)
              .Ignore(d => d.Attachments);

        config.NewConfig<Transport, TransportDetailsDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.Status, s => s.Status.ToString())
              .Map(d => d.DelivererId, s => s.DelivererId.Value)
              .Map(d => d.ManagerId, s => s.ManagerId.Value)
              .Ignore(d => d.Attachements)
              .Ignore(d => d.TransportUnits);

    }

    private static string? GetBarcode(IUnitDetails unitDetails)
    {
        if (unitDetails is UniqueUnitDetails u)
        {
            return u.Barcode;
        }

        return null;
    }

    private static int? GetUnitOfMeasureId(IUnitDetails unitDetails)
    {
        if (unitDetails is MultiUnitDetails mtd)
        {
            return mtd.UnitOfMeasureId.Value;
        }

        return null;
    }

    private static double? GetAmount(IUnitDetails unitDetails)
    {
        if (unitDetails is MultiUnitDetails mtd)
        {
            return mtd.Amount;
        }

        return null;
    }
}