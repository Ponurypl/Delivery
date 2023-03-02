using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;

internal class CreateAttachmentCommandHandler : ICommandHandler<CreateAttachmentCommand, AttachmentCreatedDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTime;
    private readonly IScanRepository _scanRepository;
    private readonly ITransportRepository _transportRepository;
    private readonly IAttachmentRepository _attachmentRepository;

    public CreateAttachmentCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IDateTime dateTime,
                                          IScanRepository scanRepository, ITransportRepository transportRepository,
                                          IAttachmentRepository attachmentRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _scanRepository = scanRepository;
        _transportRepository = transportRepository;
        _attachmentRepository = attachmentRepository;
    }

    public async Task<ErrorOr<AttachmentCreatedDto>> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
    {
        UserId creatorId = new(request.CreatorId);
        User? creator = await _userRepository.GetByIdAsync(creatorId, cancellationToken);
        if (creator is null)
        {
            return Failure.UserNotExists;
        }

        TransportId transportId = new(request.TransportId);
        Transport? transport = await _transportRepository.GetByIdAsync(transportId, cancellationToken);
        if (transport is null)
        {
            return Failure.TransportNotExists;
        }

        ErrorOr<Attachment> attachmentResult;
        if (request.Payload != null && request.AdditionalInformation is not null)
        {
            attachmentResult = Attachment.Create(creatorId, transportId, request.Payload, request.AdditionalInformation, _dateTime);
        }
        else if (request.Payload != null && request.AdditionalInformation is null)
        {
            attachmentResult = Attachment.Create(creatorId, transportId, request.Payload, _dateTime);
        }
        else if (request.Payload == null && request.AdditionalInformation is not null)
        {
            attachmentResult = Attachment.Create(creatorId, transportId, request.AdditionalInformation, _dateTime);
        }
        else
        {
            return Failure.InvalidAttachmentInput;
        }

        if (attachmentResult.IsError)
        {
            return attachmentResult.Errors;
        }

        Attachment attachment = attachmentResult.Value;

        if (request.TransportUnitId is not null)
        {
            TransportUnitId transportUnitId = new(request.TransportUnitId.Value);
            TransportUnit? transportUnit = transport.TransportUnits.FirstOrDefault(t => t.Id == transportUnitId);
            if (transportUnit is null)
            {
                return Failure.TransportUnitNotExists;
            }

            if (request.ScanId is null)
            {
                ErrorOr<Updated> transportUnitResult = attachment.SetTransportUnit(transportUnit.Id);
                if (transportUnitResult.IsError)
                {
                    return transportUnitResult.Errors;
                }
            }
            else
            {
                ScanId scanId = new(request.ScanId.Value);
                Scan? scan = await _scanRepository.GetByIdAsync(scanId, cancellationToken);
                if (scan is null)
                {
                    return Failure.ScanNotExists;
                }

                if (scan.DelivererId != creator.Id || scan.TransportUnitId != transportUnit.Id) 
                {
                    return Failure.InvalidAttachmentInput;
                }

                ErrorOr<Updated> scanResult = attachment.SetScan(transportUnit.Id, scan.Id);
                if (scanResult.IsError)
                {
                    return scanResult.Errors;
                }
            }
        }

        _attachmentRepository.Add(attachment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AttachmentCreatedDto
        {
            Id = attachment.Id.Value
        };

        

    }
}
