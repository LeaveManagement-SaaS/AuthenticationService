using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Interfaces;
using AuthenticationService.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace AuthenticationService.CrossCutting.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repository;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
         
    public CreateUserHandler(IUserRepository repository, ApplicationDbContext context, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _context = context;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    } 

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        user.PasswordHash = _passwordHasher.HashPassword(request.Password);

        await _repository.AddAsync(user);

        return user.Id;
    }
}