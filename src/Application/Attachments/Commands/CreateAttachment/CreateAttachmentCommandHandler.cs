using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.Entities;
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

    public async Task<ErrorOr<AttachmentCratedDto>> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
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

        //TODO: Dla Pawła z przyszłości - validator
        if (request.Payload is null && string.IsNullOrWhiteSpace(request.AdditionalInformation))
        {
            return Failure.InvalidAttachmentInput;
        }

        //TODO: Przerabiamy na 3 metody Create - z obydwoma wymaganymi dodatkami, tylko z payloadem oraz taką tylko z komentarzem
        var newAttachmentResult = Attachment.Create(creatorId, transportId, request.Payload, request.AdditionalInformation, _dateTime);
        if (newAttachmentResult.IsError) 
        {
            return newAttachmentResult.Errors;
        }
        var attachment = newAttachmentResult.Value;

        if (request.ScanId is not null)
        {
            var scanId = new ScanId(request.ScanId.Value);
            //TODO: zmienne lokalne z małej litery, repozytorium tylko po kluczu głównym a transportId ora creatorId sprawdzamy tutaj w handlerzu, i awaicik
            var Scan = _scanRepository.GetByIdAndTransportIdAsync(scanId, transportId, cancellationToken);
            if (Scan is null)
            {
                return Failure.ScanNotExists;
            }
            //TODO: scanId z obiektu scan z bazy a nie z danych wejściowych
            var result = attachment.AddScanId(scanId);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        if (request.TransportUnitId is not null)
        {
            var transportUnitId = new TransportUnitId(request.TransportUnitId.Value);
            var transportUnit = transport.TransportUnits.FirstOrDefault(t => t.Id == transportUnitId);
            if (transportUnit is null)
            {
                return Failure.TransportUnitNotExists;
            }
            //TODO: transportUnitId z obiektu transportUnit z bazy a nie z danych wejściowych
            var result = attachment.AddTransportUnitId(transportUnitId);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        _attachmentRepository.Add(newAttachmentResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AttachmentCratedDto
        {
            Id = newAttachmentResult.Value.Id.Value
        };

        

    }
}
