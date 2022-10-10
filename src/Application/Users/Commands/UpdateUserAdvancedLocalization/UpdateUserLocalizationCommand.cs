namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserAdvancedLocalization;

public sealed record UpdateUserAdvancedLocalizationCommand : ICommand
{
    public Guid UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Accuracy { get; set; }
    public double? GeoHeading { get; set; }
    public double? GeoSpeed { get; set; }
}
