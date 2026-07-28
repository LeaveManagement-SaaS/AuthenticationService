using MediatR;

namespace AuthenticationService.CrossCutting.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}