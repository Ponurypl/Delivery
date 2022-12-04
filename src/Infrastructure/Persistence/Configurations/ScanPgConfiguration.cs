using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Configurations;
internal sealed class ScanPgConfiguration : IEntityTypeConfiguration<Scan>
{
    public void Configure(EntityTypeBuilder<Scan> builder)
    {
        builder.ToTable("scans");

        builder.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("scan_id")
               .UseIdentityColumn()
               .HasConversion<ScanId.EfCoreValueConverter>();
        builder.Property(x => x.TransportUnitId)
               .IsRequired()
               .HasColumnName("transport_unit_id")
               .HasConversion<TransportId.EfCoreValueConverter>()
               .HasComment("id from table transport_units");
        builder.Property(x => x.Status)
               .IsRequired()
               .HasColumnName("status")
               .HasComment("scan status 0 - Valid, 1 - Deleted");
        builder.Property(x => x.LastUpdateDate).IsRequired().HasColumnName("last_update_date");
        builder.Property(x => x.DelivererId)
               .IsRequired()
               .HasColumnName("deliverer_id")
               .HasConversion<UserId.EfCoreValueConverter>()
               .HasComment("id from table users. Designates who created this scan");
        builder.Property(x => x.Quantity)
               .HasColumnName("quantity")
               .HasComment("unloaded quntity from multi_unit, null for unique_unit type of transport unit deliviery");
        //TODO: podobnie jak w użytkownikach środowisko ma problem z tym że lokacaja ma prawo być pusta.
        //Załatwiamy wykrzyknikiem czy jakoś inaczej?
        builder.Property(x => x.Location!.Longitude)
               .HasColumnName("location_longitude")
               .HasPrecision(3, 5)
               .HasComment("Longitude of scan, with precison up to 1m");
        builder.Property(x => x.Location!.Latitude)
               .HasColumnName("location_latitude")
               .HasPrecision(3, 5)
               .HasComment("Latitude of scan, with precison up to 1m");
        builder.Property(x => x.Location!.Accuracy)
               .HasColumnName("location_accuracy")
               .HasPrecision(3, 0)
               .HasComment("level of accuracy for longitude and lattitude in meters");

        builder.HasKey(x => x.Id);
        builder.HasOne<TransportUnit>().WithMany().HasForeignKey(x => x.TransportUnitId).IsRequired();
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.DelivererId).IsRequired();
    }
}
