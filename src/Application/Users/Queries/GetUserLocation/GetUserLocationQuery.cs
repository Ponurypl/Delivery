namespace MultiProject.Delivery.Application.Users.Queries.GetUserLocation;
public sealed record GetUserLocationQuery : IQuery<GetUserLocationDto>
{
    public required Guid UserId { get; init; }
}
