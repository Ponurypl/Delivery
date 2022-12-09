using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Configurations;
internal sealed class UserPgConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("user_id")
               .HasConversion<UserId.EfCoreValueConverter>();                                                                
        builder.Property(x => x.IsActive)
               .IsRequired()
               .HasColumnName("active")
               .HasComment("Is user active or inactive, inactive users cannot log in, and be assigned to new deliveries.");
        builder.Property(x => x.Role)
               .IsRequired()
               .HasColumnName("role")
               .HasComment("What roles has user assigned (bit field) 0 - no role, 1 - Deliverer, 2 - Manager");
        builder.Property(x => x.Username)
               .IsRequired()
               .HasColumnName("username")
               .HasMaxLength(50)
               .HasComment("username and login");
        builder.Property(x => x.Password)
               .IsRequired()
               .HasColumnName("password")
               .HasMaxLength(100)
               .HasComment("password hash");
        builder.Property(x => x.PhoneNumber)
               .IsRequired()
               .HasColumnName("phone_number")
               .HasMaxLength(15)
               .HasComment("user phone number without whitespaces or separators");
        builder.OwnsOne(x => x.Location).Property(x => x.Longitude)
               .HasColumnName("geolocation_longitude")
               .HasPrecision(3, 5)
               .HasComment("user last known longitude, with precision up to 1m");
        builder.OwnsOne(x => x.Location).Property(x => x.Latitude)
               .HasColumnName("geolocation_latitude")
               .HasPrecision(3, 5)
               .HasComment("user last known latitude, with precision up to 1m");
        builder.OwnsOne(x => x.Location).Property(x => x.Accuracy)
               .HasColumnName("geolocation_accuracy")
               .HasPrecision(3, 0)
               .HasComment("level of accuracy of longitude and latitude in meters. Can be null if speed is 0");
        builder.OwnsOne(x => x.Location).Property(x => x.Heading)
               .HasColumnName("geolocation_heading")
               .HasPrecision(4, 2)
               .HasComment("user last known heading in degrees where 0 is north");
        builder.OwnsOne(x => x.Location).Property(x => x.Speed)
               .HasColumnName("geolocation_speed")
               .HasPrecision(4, 2)
               .HasComment("user last known speed in m/s");
        builder.OwnsOne(x => x.Location).Property(x => x.LastUpdateDate)
               .HasColumnName("geolocation_last_update")
               .HasComment("last date of geolocation update");

        builder.HasKey(x => x.Id);
    }
}
