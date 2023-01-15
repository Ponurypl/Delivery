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
               .ValueGeneratedOnAdd();
        builder.Property(x => x.UnitOfMeasureId)
               .IsRequired()
               .HasColumnName("unit_of_measure_id")
               .HasConversion<UnitOfMeasureId.EfCoreValueConverter>()
               .HasComment("id from table units_of_measure");
        builder.Property(x => x.Amount)
               .IsRequired()
               .HasColumnName("amount")
               .HasPrecision(8,3)
               .HasComment("amount to be delivered, depends on type of unit of measure for example it can be pieces/kilograms/meters etc.");
        
        builder.HasKey(x => x.Id);
        builder.HasOne<UnitOfMeasure>().WithMany().HasForeignKey(x => x.UnitOfMeasureId);
    }
}
