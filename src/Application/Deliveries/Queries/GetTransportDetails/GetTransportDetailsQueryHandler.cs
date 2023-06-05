using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Models;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

public sealed class GetTransportDetailsQueryHandler : IQueryHandler<GetTransportDetailsQuery, TransportDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ITransportRepository _transportRepository;
    private readonly IScanRepository _scanRepository;
    private readonly IAttachmentRepository _attachmentRepository;

    public GetTransportDetailsQueryHandler(IMapper mapper, ITransportRepository transportRepository,
                                           IScanRepository scanRepository, IAttachmentRepository attachmentRepository)
    {
        _mapper = mapper;
        _transportRepository = transportRepository;
        _scanRepository = scanRepository;
        _attachmentRepository = attachmentRepository;
    }

    public async Task<ErrorOr<TransportDetailsDto>> Handle(GetTransportDetailsQuery request, CancellationToken cancellationToken)
    {
        Transport? transport = await _transportRepository.GetByIdAsync(new TransportId(request.Id), cancellationToken);
        if (transport is null)
        {
            return Failure.TransportNotExists;
        }

        TransportDetailsDto result = _mapper.Map<TransportDetailsDto>(transport);
        List<Attachment> attachments =
            await _attachmentRepository.GetAllByTransportIdAsync(transport.Id, cancellationToken);

        result.Attachments.AddRange(attachments.Select(a => a.Id.Value));

        foreach (TransportUnit transportUnit in transport.TransportUnits)
        {
            TransportUnitDto uDto = _mapper.Map<TransportUnitDto>(transportUnit);

            List<Scan> scans = await _scanRepository.GetAllByTransportUnitIdAsync(transportUnit.Id, cancellationToken);

            uDto.Scans.AddRange(scans.Select(s => s.Id.Value));
            uDto.Attachments.AddRange(attachments.Where(a => a.TransportUnitId == transportUnit.Id).Select(a => a.Id.Value));

            result.TransportUnits.Add(uDto);
        }

        //TODO: Do przetestowania czy działa - implementacja dapperowa
        //var transportId = new TransportId(request.Id);

        //var transport = await _transportRepository.GetTransportAsync(transportId);

        //if (transport is null)
        //{
        //    return Failure.TransportNotExists;
        //}

        //var attachments = await _transportRepository.GetAttachmentsAsync(transportId);
        //var transportUnits = await _transportRepository.GetTransportUnitsAsync(transportId);

        //var result = new TransportDetailsDto // do zrobienia mapperem
        //             {
        //                 Id = transport.Id,
        //                 Number = transport.Number,
        //                 CreationDate = transport.CreationDate,
        //                 AdditionalInformation = transport.AdditionalInformation,
        //                 DelivererId = transport.DelivererId,
        //                 ManagerId = transport.ManagerId,
        //                 StartDate = transport.StartDate,
        //                 Status = transport.Status,
        //                 TotalWeight = transport.TotalWeight,
        //                 Attachments = attachments
        //             };

        //foreach (TransportUnitDbModel unit in transportUnits)
        //{
        //    var dto = new TransportUnitDto()
        //              {
        //                  Number = unit.Number, AdditionalInformation = unit.AdditionalInformation,// tu reszta mapowania
        //              };

        //    TransportUnitId unitId = new(unit.Id);
        //    dto.Scans.AddRange(await _transportRepository.GetScansAsync(transportId, unitId));
        //    dto.Attachments.AddRange(await _transportRepository.GetAttachmentsAsync(transportId, unitId));

        //    result.TransportUnits.Add(dto);
        //}


        return result;
    }
}