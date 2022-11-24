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

        Attachment attachment;
        //TODO: Done? Przerabiamy na 3 metody Create - z obydwoma wymaganymi dodatkami, tylko z payloadem oraz taką tylko z komentarzem
        switch (request.Payload)
        {
            case not null when request.AdditionalInformation is not null:
                {
                    var newAttachmentResult = Attachment.Create(creatorId, transportId, request.Payload, request.AdditionalInformation, _dateTime);
                    if (newAttachmentResult.IsError)
                    {
                        return newAttachmentResult.Errors;
                    }
                    attachment = newAttachmentResult.Value;
                    break;
                }

            case null when request.AdditionalInformation is not null:
                {
                    var newAttachmentResult = Attachment.Create(creatorId, transportId, request.AdditionalInformation, _dateTime);
                    if (newAttachmentResult.IsError)
                    {
                        return newAttachmentResult.Errors;
                    }
                    attachment = newAttachmentResult.Value;
                    break;
                }

            case not null when request.AdditionalInformation is null:
                {
                    var newAttachmentResult = Attachment.Create(creatorId, transportId, request.Payload, _dateTime);
                    if (newAttachmentResult.IsError)
                    {
                        return newAttachmentResult.Errors;
                    }
                    attachment = newAttachmentResult.Value;
                    break;
                }
            default: return Failure.InvalidAttachmentInput;
        }

        if (request.TransportUnitId is not null)
        {
            var transportUnitId = new TransportUnitId(request.TransportUnitId.Value);
            var transportUnit = transport.TransportUnits.FirstOrDefault(t => t.Id == transportUnitId);
            if (transportUnit is null)
            {
                return Failure.TransportUnitNotExists;
            }
            //TODO: Done. transportUnitId z obiektu transportUnit z bazy a nie z danych wejściowych
            var transportUnitResult = attachment.AddTransportUnitId(transportUnit.Id);
            if (transportUnitResult.IsError)
            {
                return transportUnitResult.Errors;
            }

            if (request.ScanId is not null)
            {
                var scanId = new ScanId(request.ScanId.Value);
                //TODO: Done? zmienne lokalne z małej litery, repozytorium tylko po kluczu głównym a transportId ora creatorId sprawdzamy tutaj w handlerzu, i awaicik
                Scan? scan = await _scanRepository.GetByIdAsync(scanId, cancellationToken);
                if (scan is null)
                {
                    return Failure.ScanNotExists;
                }
                // użytkownik dodający attachment musi być tym który go stworzył
                if (scan.DelivererId != creator.Id) 
                {
                    return Failure.InvalidAttachmentInput;
                }
                //TODO: Done. scanId z obiektu scan z bazy a nie z danych wejściowych
                var scanResult = attachment.AddScanId(scan.Id);
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
