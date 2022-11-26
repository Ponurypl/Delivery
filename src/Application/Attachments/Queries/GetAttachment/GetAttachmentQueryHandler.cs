using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;

namespace MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;
internal class GetAttachmentQueryHandler : IQueryHandler<GetAttachmentQuery, AttachmentDto>
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IMapper _mapper;

    public GetAttachmentQueryHandler(IAttachmentRepository attachmentRepository, IMapper mapper)
    {
        _attachmentRepository = attachmentRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<AttachmentDto>> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {
        Attachment? attachment = await _attachmentRepository.GetByIdAsync(new AttachmentId(request.Id), cancellationToken);
        if (attachment is null)
        {
            return Failure.AttachmentNotExists;
        }

        return _mapper.Map<AttachmentDto>(attachment);

    }
}
