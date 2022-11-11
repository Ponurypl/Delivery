using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandCommandHandler : ICommandHandler<CreateUserCommand, UserCreatedDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UserCreatedDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUserResult = User.Create(request.Role, request.Login, request.Password, request.PhoneNumber);

        if (newUserResult.IsError) return newUserResult.Errors;

        _userRepository.Add(newUserResult.Value);
        await _unitOfWork.SaveChangesAsync();

        return new UserCreatedDto { Id = newUserResult.Value.Id };
    }
}
