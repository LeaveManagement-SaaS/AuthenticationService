namespace AuthenticationService.CrossCutting.Users.Commands.LoginUser
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
} 