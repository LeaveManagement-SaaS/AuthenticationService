using MediatR;
using AuthenticationService.Domain.Interfaces;

namespace AuthenticationService.CrossCutting.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);

        if (user == null)
            return false;

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Phone = request.Phone;
        user.IsEmailVerified = request.IsEmailVerified;
        user.Status = request.Status;

        await _repository.UpdateAsync(user);

        return true;
    }
}