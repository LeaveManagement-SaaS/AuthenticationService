using AuthenticationService.Application.DTOs.Users;
using AuthenticationService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace AuthenticationService.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersHandler
    : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;   

        public GetAllUsersHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
