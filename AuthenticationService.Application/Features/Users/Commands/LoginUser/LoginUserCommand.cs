using MediatR;

namespace AuthenticationService.CrossCutting.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
} 