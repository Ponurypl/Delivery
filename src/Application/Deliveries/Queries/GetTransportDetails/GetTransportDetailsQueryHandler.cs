using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Models;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

public sealed class GetTransportDetailsQueryHandler : IQueryHandler<GetTransportDetailsQuery, TransportDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ITransportRepository _transportRepository;

    public GetTransportDetailsQueryHandler(IMapper mapper, ITransportRepository transportRepository)
    {
        _mapper = mapper;
        _transportRepository = transportRepository;
    }

    public async Task<ErrorOr<TransportDetailsDto>> Handle(GetTransportDetailsQuery request, CancellationToken cancellationToken)
    {
        TransportId transportId = new TransportId(request.Id);
        TransportDbModel? transport = await _transportRepository.GetTransportAsync(transportId);

        if (transport is null)
        {
            return Failure.TransportNotExists;
        }

        List<int> attachments = await _transportRepository.GetAttachmentsAsync(transportId);
        List<TransportUnitDbModel> transportUnits = await _transportRepository.GetTransportUnitsAsync(transportId);

        TransportDetailsDto result = _mapper.Map<TransportDetailsDto>(transport);
        result.Attachments.AddRange(attachments); //TODO: lepiej zrobić przypisanie? czy AddRange Ok?

        foreach (TransportUnitDbModel unit in transportUnits)
        {
            TransportUnitDto dto = _mapper.Map<TransportUnitDto>(unit);

            TransportUnitId unitId = new(unit.Id);
            dto.Scans.AddRange(await _transportRepository.GetScansAsync(unitId));
            dto.Attachments.AddRange(await _transportRepository.GetAttachmentsAsync(transportId, unitId));

            result.TransportUnits.Add(dto);
        }


        return result;
    }
}