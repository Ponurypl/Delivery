using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Configurations;

internal sealed class TransportPgConfiguration : IEntityTypeConfiguration<Transport>
{
    public void Configure(EntityTypeBuilder<Transport> builder)
    {
        builder.ToTable("transports");

        builder.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("transport_id")
               .UseIdentityColumn()
               .HasConversion<TransportId.EfCoreValueConverter>();
        builder.Property(x => x.DelivererId)
               .IsRequired()
               .HasColumnName("deliverer_id")
               .HasConversion<UserId.EfCoreValueConverter>()
               .HasComment("id from table users. This user will deliver units to recipients and unload them");
        builder.Property(x => x.ManagerId)
               .IsRequired()
               .HasColumnName("manager_id")
               .HasConversion<UserId.EfCoreValueConverter>()
               .HasComment("id from table users. This user is responsible for this transport, and a contact person for deliverer in case of any problems");
        builder.Property(x => x.Status)
               .IsRequired()
               .HasColumnName("status")
               .HasComment("Transport status 0 - New, 1 - Processing, 2 - Finished, 3 - Deleted");
        builder.Property(x => x.Number).IsRequired().HasColumnName("number").HasMaxLength(50);
        builder.Property(x => x.CreationDate).IsRequired().HasColumnName("creation_date");
        builder.Property(x => x.StartDate).IsRequired().HasColumnName("start_date");
        builder.Property(x => x.AdditionalInformation).HasColumnName("additional_information").HasMaxLength(2000);
        builder.Property(x => x.TotalWeight).HasColumnName("total_weight").HasPrecision(9,4);

        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.TransportUnits).WithOne(x => x.Transport);
    }
}