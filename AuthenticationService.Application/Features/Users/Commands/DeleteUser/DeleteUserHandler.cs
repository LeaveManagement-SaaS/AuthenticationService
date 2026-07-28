using MediatR;
using AuthenticationService.Domain.Interfaces;

namespace AuthenticationService.CrossCutting.Users.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);

        if (user == null)
            return false;

        await _repository.DeleteAsync(request.Id);

        return true;
    }
} 