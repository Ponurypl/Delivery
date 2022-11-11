using MultiProject.Delivery.Domain.Attachements.Enums;
using MultiProject.Delivery.Domain.Common.Interfaces;
using System.Reflection.Metadata;

namespace MultiProject.Delivery.Domain.Attachements.Entities;

public sealed class Attachement : IAggregateRoot
{
    public int Id { get; set; }
    public Guid CreatorId { get; set; }
    public int TransportId { get; set; } 
    public int? ScanId { get; set; }
    public int? TransportUnitId { get; set; }
    public AttachementStatus Status { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public Blob? Payload { get; set; }
    public string? AditionalInformation { get; set; }

}
