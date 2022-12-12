using MultiProject.Delivery.Application.Common.Cryptography;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserCreatedDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IHashService hashService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _hashService = hashService;
    }

    public async Task<ErrorOr<UserCreatedDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //TODO: do przemapowania rola cast na dole nie działa 
        var newUserResult = User.Create((Domain.Users.Enums.UserRole)request.Role, request.Username, _hashService.Hash(request.Password), request.PhoneNumber);

        if (newUserResult.IsError) return newUserResult.Errors;

        var user = newUserResult.Value;
        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserCreatedDto { Id = user.Id.Value };
    }
}
