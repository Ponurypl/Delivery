using MultiProject.Delivery.Domain.Common.ValueTypes;

namespace MultiProject.Delivery.Domain.Deliveries.Exceptions;

internal static class Failures
{
    public static Error DelivererNotFound => new Error("UserNotFound", "Deliverer not found");
    public static Error ManagerNotFound => new Error("UserNotFound", "Manager not found");
    public static Error DelivererInsufficientRole => new Error("UserInsufficientRole", "Deliverer has insufficient role"); 
    public static Error ManagerInsufficientRole => new Error("UserInsufficientRole", "Manager has insufficient role");
}
