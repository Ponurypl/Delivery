using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Users.Queries.GetUserLocation;
public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, GetUserLocationDto> () //TODO: jak zmusić maperra do zmapowania bez zagnieżdzania i wypisywania wszystkiego ręcznie? Potrzebne z usera jest tylko AdvancedGeolocation z usera
              .Map(d => d.Accuracy, s => s.Location!.Accuracy)
              .Map(d => d.Heading, s => s.Location!.Heading)
              .Map(d => d.LastUpdateDate, s => s.Location!.LastUpdateDate)
              .Map(d => d.Latitude, s => s.Location!.Latitude)
              .Map(d => d.Longitude, s => s.Location!.Longitude)
              .Map(d => d.Speed, s=> s.Location!.Speed);
    }
}
