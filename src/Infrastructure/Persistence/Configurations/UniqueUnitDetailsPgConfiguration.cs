using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Configurations;
internal class UniqueUnitDetailsPgConfiguration : IEntityTypeConfiguration<UniqueUnitDetails>
{
    public void Configure(EntityTypeBuilder<UniqueUnitDetails> builder)
    {
        builder.ToTable("unique_units_details");

        builder.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("unique_unit_id")
               .HasConversion<UniqueUnitDetailsId.EfCoreValueConverter>()
               .UseIdentityColumn();
        builder.Property(x => x.Barcode).IsRequired().HasColumnName("barcode");
        builder.Property(x => x.TransportUnit.Id)
               .IsRequired()
               .HasColumnName("trasnposrt_unit_id")
               .HasConversion<TransportUnitId.EfCoreValueConverter>()
               .HasComment("id from table transport_units");

        builder.HasKey(x => x.Id);
        builder.HasOne<TransportUnit>().WithMany().HasForeignKey(x => x.TransportUnit.Id);
    }
}
