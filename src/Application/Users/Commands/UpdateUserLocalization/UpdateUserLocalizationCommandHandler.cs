﻿namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserLocalization;

public sealed record UpdateUserLocalizationCommand : ICommand
{
    public Guid UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Accuracy { get; set; }
    public double? GeoHeading { get; set; }
    public double? GeoSpeed { get; set; }
}