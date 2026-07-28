using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthenticationService.CrossCutting.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;
    private IMapper _mapper;

    public UpdateUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id); 

        if (user == null)
            return false; 

        var users = _mapper.Map<User>(request); 

        await _repository.UpdateAsync(users);

        return true;
    }
}