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
        //TODO: UnitOfMeasure jest agregatem, nie pozwalamy go tworzyć z zewnątrz. Do przerobienia na metodę statyczną Create.
        
        UnitOfMeasure newUnitOfMeasure = new()
        {
            Name = request.Name,
            Description = request.Description,
            Symbol = request.Symbol
        };

        _unitOfMeasureRepository.Add(newUnitOfMeasure);
        await _unitOfWork.SaveChangesAsync();        

        return Result.Success;
    }
}
