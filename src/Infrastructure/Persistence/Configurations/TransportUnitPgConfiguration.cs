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
        builder.Property(x => x.Status)
               .IsRequired()
               .HasColumnName("status")
               .HasComment("Transport unit status 1 - New, 2 - PartiallyDelivered, 3 - Delivered, 4 - Deleted");
        builder.Property(x => x.Number).IsRequired().HasColumnName("number").HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasColumnName("description").HasMaxLength(200);
        builder.Property(x => x.Recipient.CompanyName).HasColumnName("recipient_company_name").HasMaxLength(200);
        builder.Property(x => x.Recipient.Name).HasColumnName("recipient_name").HasMaxLength(200);
        builder.Property(x => x.Recipient.LastName).HasColumnName("recipient_last_name").HasMaxLength(200);
        builder.Property(x => x.Recipient.PhoneNumber)
               .IsRequired()
               .HasColumnName("recipient_phone_number")
               .HasMaxLength(15)
               .HasComment("phone number without whitespaces or separators");
        builder.Property(x => x.Recipient.FlatNumber).HasColumnName("recipient_flat_number").HasMaxLength(5);
        builder.Property(x => x.Recipient.StreetNumber).HasColumnName("recipient_street_number").IsRequired().HasMaxLength(5);
        builder.Property(x => x.Recipient.Street).HasColumnName("recipient_street").HasMaxLength(200);
        builder.Property(x => x.Recipient.Town).HasColumnName("recipient_town").IsRequired().HasMaxLength(200);
        builder.Property(x => x.Recipient.Country).HasColumnName("recipient_country").IsRequired().HasMaxLength(200);
        builder.Property(x => x.Recipient.PostCode).HasColumnName("recipient_post_code").IsRequired().HasMaxLength(200);
        
        builder.Property(x => x.AdditionalInformation).HasColumnName("additional_information").HasMaxLength(2000);


        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Transport).WithMany(x => x.TransportUnits).HasForeignKey("transport_id");
        builder.HasOne(x => (UniqueUnitDetails)x.UnitDetails).WithOne(x => x.TransportUnit);
        builder.HasOne(x => (MultiUnitDetails)x.UnitDetails).WithOne(x => x.TransportUnit);
    }
}