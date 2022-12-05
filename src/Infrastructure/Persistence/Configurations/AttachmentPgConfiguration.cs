using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Configurations;

internal sealed class AttachmentPgConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("attachments");

        builder.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("attachment_id")
               .UseIdentityColumn()
               .HasConversion<AttachmentId.EfCoreValueConverter>();
        builder.Property(x => x.CreatorId)
               .IsRequired()
               .HasColumnName("creator_id")
               .HasConversion<UserId.EfCoreValueConverter>()
               .HasComment("id from table users");
        builder.Property(x => x.Status)
               .IsRequired()
               .HasColumnName("status")
               .HasComment("0 - Valid, 1 - Deleted");
        builder.Property(x => x.TransportId)
               .IsRequired()
               .HasColumnName("transport_id")
               .HasConversion<TransportId.EfCoreValueConverter>()
               .HasComment("id from table transports");
        builder.Property(x => x.LastUpdateDate).IsRequired().HasColumnName("last_update");
        builder.Property(x => x.TransportUnitId)
               .HasColumnName("transport_unit_id")
               .HasConversion<TransportUnitId.EfCoreValueConverter>()
               .HasComment("id from table transport_units");
        builder.Property(x => x.ScanId)
               .HasColumnName("scan_id")
               .HasConversion<ScanId.EfCoreValueConverter>()
               .HasComment("id from table scans");
        builder.Property(x => x.Payload).HasColumnName("payload");
        builder.Property(x => x.AdditionalInformation).HasColumnName("additional_information").HasMaxLength(2000);

        builder.HasKey(x => x.Id);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.CreatorId);
        builder.HasOne<Transport>().WithMany().HasForeignKey(x => x.TransportId);
        builder.HasOne<TransportUnit>().WithMany().HasForeignKey(x => x.TransportUnitId);
        builder.HasOne<Scan>().WithMany().HasForeignKey(x => x.ScanId);
    }
}