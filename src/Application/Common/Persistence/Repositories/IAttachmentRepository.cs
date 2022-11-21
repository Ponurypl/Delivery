using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;
public interface IAttachmentRepository
{
    void Add(Attachment attachment);
    Task<Attachment?> GetByIdAsync(AttachmentId id, CancellationToken cancellationToken = default);
    Task<List<Attachment>> GetAllByTransportIdAsync(TransportId transportId, CancellationToken cancellationToken = default);
    Task<List<Attachment>> GetAllByScanIdAsync(ScanId id, CancellationToken cancellationToken = default);

}
