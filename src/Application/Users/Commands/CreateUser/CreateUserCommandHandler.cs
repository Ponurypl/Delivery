using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IHandler<CreateUserCommand, UserCreatedDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserCreatedDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Result<User> newUserResult = User.Create(request.Role, request.Login, request.Password, request.PhoneNumber);

        if (newUserResult.IsSuccess)
        {
            _userRepository.Add(newUserResult.Value);
            await _unitOfWork.SaveChangesAsync();

            return new UserCreatedDto { Id = newUserResult.Value.Id };
        }
        else throw new Exception("User creation error" + newUserResult.Error);
    }
}
