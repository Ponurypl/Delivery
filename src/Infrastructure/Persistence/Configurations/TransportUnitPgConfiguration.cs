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
               .ValueGeneratedOnAdd()
               .HasConversion<TransportUnitId.EfCoreValueConverter>();
        builder.Property(x => x.Status)
               .IsRequired()
               .HasColumnName("status")
               .HasComment("Transport unit status 1 - New, 2 - PartiallyDelivered, 3 - Delivered, 4 - Deleted");
        builder.Property(x => x.Number).IsRequired().HasColumnName("number").HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasColumnName("description").HasMaxLength(200);
        builder.OwnsOne(x => x.Recipient).Property(x => x.CompanyName).HasColumnName("recipient_company_name").HasMaxLength(200);
        builder.OwnsOne(x => x.Recipient).Property(x => x.Name).HasColumnName("recipient_name").HasMaxLength(200);
        builder.OwnsOne(x => x.Recipient).Property(x => x.LastName).HasColumnName("recipient_last_name").HasMaxLength(200);
        builder.OwnsOne(x => x.Recipient).Property(x => x.PhoneNumber)
               .IsRequired()
               .HasColumnName("recipient_phone_number")
               .HasMaxLength(15)
               .HasComment("phone number without whitespaces or separators");
        builder.OwnsOne(x => x.Recipient).Property(x => x.FlatNumber).HasColumnName("recipient_flat_number").HasMaxLength(5);
        builder.OwnsOne(x => x.Recipient).Property(x => x.StreetNumber).HasColumnName("recipient_street_number").IsRequired().HasMaxLength(5);
        builder.OwnsOne(x => x.Recipient).Property(x => x.Street).HasColumnName("recipient_street").HasMaxLength(200);
        builder.OwnsOne(x => x.Recipient).Property(x => x.Town).HasColumnName("recipient_town").IsRequired().HasMaxLength(200);
        builder.OwnsOne(x => x.Recipient).Property(x => x.Country).HasColumnName("recipient_country").IsRequired().HasMaxLength(200);
        builder.OwnsOne(x => x.Recipient).Property(x => x.PostCode).HasColumnName("recipient_post_code").IsRequired().HasMaxLength(200);
        builder.Property(x => x.AdditionalInformation).HasColumnName("additional_information").HasMaxLength(2000);


        builder.Property<TransportId>("transport_id").HasConversion<TransportId.EfCoreValueConverter>();
        builder.Property<UniqueUnitDetailsId?>("unique_unit_id").HasConversion<UniqueUnitDetailsId.EfCoreValueConverter>();
        builder.Property<MultiUnitDetailsId?>("multi_unit_id").HasConversion<MultiUnitDetailsId.EfCoreValueConverter>();

        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Transport).WithMany(x => x.TransportUnits).HasForeignKey("transport_id");
        builder.HasOne(x => x.UniqueUnitDetails).WithOne(x => x.TransportUnit).HasForeignKey<TransportUnit>("unique_unit_id");
        builder.HasOne(x => x.MultiUnitDetails).WithOne(x => x.TransportUnit).HasForeignKey<TransportUnit>("multi_unit_id");
    }
}