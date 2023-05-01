using MediatR;
using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Users.Commands.UpdateUser;
internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IMapper mapper, IPublisher publisher, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _publisher = publisher;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    //TODO: Nie powinniśmy mieć handlera w takich przypadkach Task<ErrorOr<Updated>>?
    public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? userToUpdate = await _userRepository.GetByIdAsync(new UserId(request.UserId),cancellationToken);
        if (userToUpdate is null) return Failure.UserNotExists;
        bool originalIsActive = userToUpdate.IsActive;

        ErrorOr<Updated> updateResult = userToUpdate.UpdateUser((Domain.Users.Enums.UserRole)request.Role, request.IsActive, request.PhoneNumber);
        if (updateResult.IsError) return updateResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);


        //TODO: Seria pytań
        //Czy podanie cancellationToken w Publish nie jest czasem złym pomysłem? Może się anulować to co obsługiwane jest przez Publish jak zakończy się ten Handle?
        //Lepiej podawać komunikaty w formie przed i po czy raczej, masz ID tego co się zmieniło domyśl się co się zmieniło.
        //Zakładam że konstruowanie takich event-ów pod to co będzie taki event obsługiwać nie jest najlepszym pomysłem i powinny raczej być w miarę uniwersalne?
        //Jak te dwa pytania mają się do tego co niżej?
        //Czy oprogramowanie sobie np. UI że nasłuchuje na tego typu event-y i się odświeża jak ktoś inny coś zmieni jest... rozsądne? Przykład, mapka gdzie zaznaczeni są kierowcy.
        await _publisher.Publish(new UserUpdated(){ Id = userToUpdate.Id.Value}, cancellationToken);
        if (userToUpdate.IsActive == false && originalIsActive)
        {
            await _publisher.Publish(new UserDeactivated() { Id = userToUpdate.Id.Value }, cancellationToken);
        }

        return Result.Success;
    }
}
