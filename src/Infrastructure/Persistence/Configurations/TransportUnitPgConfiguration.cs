using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Configurations;

internal sealed class TransportUnitPgConfiguration : IEntityTypeConfiguration<TransportUnit>
{
    public void Configure(EntityTypeBuilder<TransportUnit> builder)
    {
        builder.ToTable("transport_units");

        builder.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("transport_unit_id")
               .UseIdentityColumn()
               .HasConversion<TransportUnitId.EfCoreValueConverter>();
        builder.Property(x => x.Status).IsRequired().HasColumnName("status");
        builder.Property(x => x.Number).IsRequired().HasColumnName("number").HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasColumnName("description").HasMaxLength(200);
        builder.Property(x => x.Recipient.Name).HasColumnName("recipient_name");
        builder.Property(x => x.Recipient.LastName).HasColumnName("recipient_last_name");
        //TODO: recipient
        builder.Property(x => x.AdditionalInformation).HasColumnName("additional_information").HasMaxLength(2000);


        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Transport).WithMany(x => x.TransportUnits).HasForeignKey("transport_id"); //TODO: do zweryfikowania
        builder.HasOne(x => (UniqueUnitDetails)x.UnitDetails).WithOne(x => x.TransportUnit);
        builder.HasOne(x => (MultiUnitDetails)x.UnitDetails).WithOne(x => x.TransportUnit);
    }
}