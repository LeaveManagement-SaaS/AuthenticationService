using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Infrastructure.Persistence;


namespace AuthenticationService.CrossCutting.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repository;
    private readonly ApplicationDbContext _context;

    public CreateUserHandler(IUserRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            Phone = request.Phone,
            IsEmailVerified = false,
            Status = 1,
            CreatedDate = DateTime.UtcNow
        };

        await _repository.AddAsync(user);

        return user.Id;
    }
}