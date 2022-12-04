using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Configurations;
internal class MultiUnitDetailsPgConfiguration : IEntityTypeConfiguration<MultiUnitDetails>
{
    public void Configure(EntityTypeBuilder<MultiUnitDetails> builder)
    {
        builder.ToTable("multi_units_details");

        builder.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("multi_unit_id")
               .HasConversion<MultiUnitDetailsId.EfCoreValueConverter>()
               .UseIdentityColumn();
        builder.Property(x => x.UnitOfMeasureId)
               .IsRequired()
               .HasColumnName("unit_of_measure_id")
               .HasConversion<UnitOfMeasureId.EfCoreValueConverter>()
               .HasComment("id from table units_of_measure");
        builder.Property(x => x.Amount)
               .IsRequired()
               .HasColumnName("amount")
               .HasPrecision(5,3)
               .HasComment("amount to be delivierd, dependens on type of unit of measure for example it can be pices/kilograms/meters etc.");
        builder.Property(x => x.TransportUnit.Id)
               .IsRequired()
               .HasColumnName("transport_unit_id")
               .HasConversion<TransportUnitId.EfCoreValueConverter>()
               .HasComment("id from table transport_units");


        builder.HasKey(x => x.Id);
        builder.HasOne<UnitOfMeasure>().WithMany().HasForeignKey(x => x.UnitOfMeasureId).IsRequired();
        builder.HasOne<TransportUnit>().WithMany().HasForeignKey(x => x.TransportUnit.Id).IsRequired();
    }
}
