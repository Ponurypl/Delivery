using MultiProject.Delivery.Application.Common.Interfaces;
using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Exceptions;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class Transport : IAggregateRoot
{
    public int Id { get; set; }
    public User Deliverer { get; set; } = null!;
    public TransportStatus Status { get; set; }
    public string Number { get; set; } = default!;
    public string? AditionalInformation { get; set; }
    public double? TotalWeight { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public User Manager { get; set; } = null!;
    public List<TransportUnit> TransportUnits { get; set; } = new();

    private Transport(User deliverer, string number, string? aditionalInformation, double? totalWeight, DateTime startDate, 
                      User manager, IDateTime dateTimeProvider)
    {
        Deliverer = deliverer;
        Status = TransportStatus.New;
        Number = number;
        AditionalInformation = aditionalInformation;
        TotalWeight = totalWeight;
        CreationDate = dateTimeProvider.Now;
        StartDate = startDate;
        Manager = manager;        
    }


    public static Transport Create(User deliverer, string number, string? aditionalInformation, double? totalWeight, DateTime startDate,
        User manager, IDateTime dateTimeProvider)
    {
        if (deliverer is null) throw new UserNotFoundException(nameof(request.DelivererId));
        if (deliverer.Role is not Users.Enums.UserRole.Deliverer) throw new UserRoleException(deliverer.Id);

        if (manager is null) throw new UserNotFoundException(nameof(request.ManagerId));
        if (manager.Role is not Users.Enums.UserRole.Manager) throw new UserRoleException(nameof(request.ManagerId));

        var newTransport = new Transport(deliverer, number, aditionalInformation, totalWeight, startDate, manager, dateTimeProvider);
        return newTransport;
    }
}
