using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;
internal class CreateAttachmentCommandHandler : ICommandHandler<CreateAttachmentCommand, AttachmentCratedDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTime;
    private readonly IScanRepository _scanRepository;
    private readonly ITransportRepository _transportRepository;
    private readonly IAttachmentRepository _attachmentRepository;

    public CreateAttachmentCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IDateTime dateTime, IScanRepository scanRepository, ITransportRepository transportRepository, IAttachmentRepository attachmentRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _scanRepository = scanRepository;
        _transportRepository = transportRepository;
        _attachmentRepository = attachmentRepository;
    }

    public async Task<ErrorOr<AttachmentCratedDto>> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
    {
        var creatorId = new UserId(request.CreatorId);
        var creator = await _userRepository.GetByIdAsync(creatorId, cancellationToken);
        if (creator is null)
        {
            return Failure.UserNotExists;
        }

        TransportId transportID = new (request.TransportId);
        var transport = await _transportRepository.GetByIdAsync(transportID, cancellationToken);
        if (transport is null)
        {
            return Failure.TransportNotExists;
        }

        if (request.Payload is null && string.IsNullOrWhiteSpace(request.AdditionalInformation))
        {
            return Failure.InvalidAttachmentInput;
        }

        //TODO: czy payload i additionalinformation powinno być osobną metodą podobnie jak scanid i transportunitid dla spójności?
        var newAttachmentResult = Attachment.Create(creatorId, transportID, request.Payload, request.AdditionalInformation, _dateTime, cancellationToken);
        if (newAttachmentResult.IsError) 
        {
            return newAttachmentResult.Errors;
        }
        var attachment = newAttachmentResult.Value;

        if (request.ScanId is not null)
        {
            var scanId = new ScanId(request!.ScanId.Value);
            var Scan = _scanRepository.GetByIdAndTransportIdAsync(scanId, transportID, cancellationToken);
            if (Scan is null)
            {
                return Failure.ScanNotExists;
            }
            var result = attachment.AddScanId(scanId);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        if (request.TransportUnitId is not null)
        {
            var transportUnitId = new TransportUnitId(request!.TransportUnitId.Value);
            var transportUnit = transport.TransportUnits.FirstOrDefault(t => t.Id == transportUnitId);
            if (transportUnit is null)
            {
                return Failure.TransportUnitNotExists;
            }
            var result = attachment.AddTransportUnitId(transportUnitId);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        _attachmentRepository.Add(newAttachmentResult.Value);
        await _unitOfWork.SaveChangesAsync();

        return new AttachmentCratedDto
        {
            Id = newAttachmentResult.Value.Id.Value
        };

        

    }
}
