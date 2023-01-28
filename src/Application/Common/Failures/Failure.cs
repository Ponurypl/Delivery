namespace MultiProject.Delivery.Application.Common.Failures;

public static class Failure
{
    public static Error InvalidTransportUnitDetails => Error.Validation(nameof(InvalidTransportUnitDetails));
    public static Error UserNotExists => Error.NotFound(nameof(UserNotExists));
    public static Error UserNameTaken => Error.NotFound(nameof(UserNameTaken));
    public static Error WrongUserOrPassword => Error.Validation(nameof(WrongUserOrPassword));
    public static Error TransportNotExists => Error.NotFound(nameof(TransportNotExists));
    public static Error ScanNotExists => Error.NotFound(nameof(ScanNotExists));
    public static Error TransportUnitNotExists => Error.NotFound(nameof(TransportUnitNotExists));
    public static Error PasswordEqualsLogin => Error.Validation(nameof(PasswordEqualsLogin));
    public static Error InvalidScanInput => Error.Validation(nameof(InvalidScanInput));
    public static Error ScanAbleAmountExceeded(double? amountAvilableFroScan) => Error.Validation(nameof(ScanAbleAmountExceeded)+ $": Avilable amount for scan left: {amountAvilableFroScan}");
    public static Error InvalidAttachmentInput => Error.Validation(nameof(InvalidAttachmentInput));
    public static Error InvalidMessage => Error.Validation(nameof(InvalidMessage));
    public static Error AttachmentNotExists => Error.Validation(nameof(AttachmentNotExists));
    public static Error UnitOfMeasureNotExists => Error.NotFound(nameof(UnitOfMeasureNotExists));
}
