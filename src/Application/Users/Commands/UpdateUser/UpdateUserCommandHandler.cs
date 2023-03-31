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

    public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? userToUpdate = await _userRepository.GetByIdAsync(new UserId(request.UserId),cancellationToken);
        if (userToUpdate is null) return Failure.UserNotExists;

        userToUpdate.UpdateUser((Domain.Users.Enums.UserRole)request.Role, request.IsActive, request.PhoneNumber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _publisher.Publish(userToUpdate, cancellationToken);
        return Result.Success;
    }
}
