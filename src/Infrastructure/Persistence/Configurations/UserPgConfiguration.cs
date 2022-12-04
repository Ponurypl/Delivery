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
               .UseIdentityColumn()
               .HasConversion<UserId.EfCoreValueConverter>();                                                                
        builder.Property(x => x.IsActive)
               .IsRequired()
               .HasColumnName("is_active")
               .HasComment("Is user active or inactive, inactive users cannot log in, and be assinged to new deliveries.");
        //TODO: do zweryfikowania czy nieaktywny użytkownik na pewno nie może być przypisany i zalogować się
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
               .HasMaxLength(50)
               .HasComment("password hash");
        builder.Property(x => x.PhoneNumber)
               .IsRequired()
               .HasColumnName("phone_number")
               .HasMaxLength(15)
               .HasComment("user phonenumber without whitespaces or separators");
        //TODO: tu program krzyczy że location może być null, no prawda, ale nigdzie indziej o to nie płakał,
        //chodzi o to że chcę coś z lokacji jeszcze wyciągnąć?
        builder.Property(x => x.Location!.Longitude)
               .HasColumnName("geolocation_longitude")
               .HasPrecision(3, 5)
               .HasComment("user last known longitude, with precison up to 1m");
        builder.Property(x => x.Location!.Latitude)
               .HasColumnName("geolocation_lattitude")
               .HasPrecision(3, 5)
               .HasComment("user last known lattitude, with precison up to 1m");
        builder.Property(x => x.Location!.Accuracy)
               .HasColumnName("geolocation_accuracy")
               .HasPrecision(3, 0)
               .HasComment("level of accuracy of longitude and lattitude in meters. Can be null if speed is 0");
        builder.Property(x => x.Location!.Heading)
               .HasColumnName("geolocation_heading")
               .HasPrecision(4, 2)
               .HasComment("user last known heading in degrees where 0 is north");
        builder.Property(x => x.Location!.Speed)
               .HasColumnName("geolocation_speed")
               .HasPrecision(4, 2)
               .HasComment("user last known speed in m/s");
        builder.Property(x => x.Location!.ReadDate)
               .HasColumnName("geolocation_readdate")
               .HasComment("last date of geolocation update");

        builder.HasKey(x => x.Id);
    }
}
