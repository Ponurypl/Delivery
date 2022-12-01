using Microsoft.EntityFrameworkCore;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Repositories;

internal sealed class AttachmentRepository : IAttachmentRepository
{
    private readonly DbSet<Attachment> _attachments;

    public AttachmentRepository(ApplicationDbContext context)
    {
        _attachments = context.Set<Attachment>();
    }

    public void Add(Attachment attachment)
    {
        _attachments.Add(attachment);
    }

    public async Task<Attachment?> GetByIdAsync(AttachmentId id, CancellationToken cancellationToken = default)
    {
        return await _attachments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<List<Attachment>> GetAllByTransportIdAsync(TransportId transportId, CancellationToken cancellationToken = default)
    {
        return await _attachments.Where(a => a.TransportId == transportId).ToListAsync(cancellationToken);
    }

    public async Task<List<Attachment>> GetAllByScanIdAsync(ScanId scanId, CancellationToken cancellationToken = default)
    {
        return await _attachments.Where(a => a.ScanId == scanId).ToListAsync(cancellationToken);
    }
}