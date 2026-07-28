using MediatR;

namespace AuthenticationService.CrossCutting.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public bool IsEmailVerified { get; set; }

    public byte Status { get; set; }
}