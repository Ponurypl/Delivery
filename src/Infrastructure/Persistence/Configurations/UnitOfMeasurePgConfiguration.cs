using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Configurations;

internal sealed class UnitOfMeasurePgConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        builder.ToTable("units_of_measure");

        builder.Property(x => x.Id).IsRequired().HasColumnName("uom_id").UseIdentityColumn().HasConversion<UnitOfMeasureId.EfCoreValueConverter>();
        builder.Property(x => x.Name).IsRequired().HasColumnName("name").HasMaxLength(50);
        builder.Property(x => x.Symbol).IsRequired().HasColumnName("symbol").HasMaxLength(5);
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(200);

        builder.HasKey(x => x.Id);
    }
}