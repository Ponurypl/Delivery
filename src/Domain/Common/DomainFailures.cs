namespace MultiProject.Delivery.Domain.Common;

internal static class DomainFailures
{
    public static class Common
    {
        public static Error MissingRequiredDependency => Error.Unexpected(nameof(MissingRequiredDependency));
        public static Error MissingParentObject => Error.Validation(nameof(MissingParentObject));
    }
    
    public static class Geolocations
    {
        public static Error InvalidGeolocation => Error.Validation(nameof(InvalidGeolocation));
        public static Error InvalidAdvancedGeolocation => Error.Validation(nameof(InvalidAdvancedGeolocation));
    }

    public static class Deliveries
    {
        public static Error InvalidUnitBarcode => Error.Validation(nameof(InvalidUnitBarcode)); //TODO: Albo to zostaje i reszta powinna być bardziej szczegółowa albo to powinno byc InvalidUniqueUnitDetails
        public static Error InvalidUnitAmount => Error.Validation(nameof(InvalidUnitAmount));
        public static Error InvalidTransportUnit => Error.Validation(nameof(InvalidTransportUnit));
        public static Error InvalidTransportUnitDetails => Error.Validation(nameof(InvalidTransportUnitDetails));
        public static Error InvalidRecipient => Error.Validation(nameof(InvalidRecipient));

    }

    public static class Dictionaries
    {
        public static Error InvalidUnitOfMeasure => Error.Validation(nameof(InvalidUnitOfMeasure));
    }

    public static class Scans
    {
        public static Error InvalidQuantity => Error.Validation(nameof(InvalidQuantity));
    }

    public static class Users
    {
        public static Error InvalidPhoneNumber => Error.Validation(nameof(InvalidPhoneNumber));
        public static Error UserDoesNotMeetRole => Error.Validation(nameof(UserDoesNotMeetRole)); //TODO: Przemyśleć ten error (typ lub nazwa)
    }
}