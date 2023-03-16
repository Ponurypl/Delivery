using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Application.Dictionaries.Queries.GetUnitOfMeasure;
internal class GetUnitOfMeasureQueryHandler : IQueryHandler<GetUnitOfMeasureQuery, GetUnitOfMeasureDto>
{
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;

    public GetUnitOfMeasureQueryHandler(IUnitOfMeasureRepository unitOfMeasureRepository)
    {
        _unitOfMeasureRepository = unitOfMeasureRepository;
    }

    public async Task<ErrorOr<GetUnitOfMeasureDto>> Handle(GetUnitOfMeasureQuery request, CancellationToken cancellationToken)
    {
        UnitOfMeasure? unitOfMeasure = await _unitOfMeasureRepository.GetByIdAsync(new UnitOfMeasureId(request.Id), cancellationToken);

        if (unitOfMeasure is null)
        {
            return Failure.UnitOfMeasureNotExists;
        }

        return new GetUnitOfMeasureDto() { Name = unitOfMeasure.Name, Symbol = unitOfMeasure.Symbol, Description = unitOfMeasure.Description };
    }
}
