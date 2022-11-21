using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Dictionaries.Entities;

namespace MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;

public sealed class CreateUnitOfMeasureCommandHandler : ICommandHandler<CreateUnitOfMeasureCommand>
{
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUnitOfMeasureCommandHandler(IUnitOfWork unitOfWork, IUnitOfMeasureRepository unitOfMeasureRepository)
    {
        _unitOfWork = unitOfWork;
        _unitOfMeasureRepository = unitOfMeasureRepository;
    }

    public async Task<ErrorOr<Success>> Handle(CreateUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        var newUnitOfMeasure = UnitOfMeasure.Create(request.Name, request.Symbol, request.Description);

        if (newUnitOfMeasure.IsError) return newUnitOfMeasure.Errors;

        _unitOfMeasureRepository.Add(newUnitOfMeasure.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);        

        return Result.Success;
    }
}
