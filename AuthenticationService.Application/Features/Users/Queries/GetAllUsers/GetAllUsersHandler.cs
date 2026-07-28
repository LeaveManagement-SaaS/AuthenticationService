using AuthenticationService.Application.DTOs.Users;
using AuthenticationService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersHandler
    : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _repository;

        public GetAllUsersHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserDto>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();

            return users.Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).ToList();
        }
    }
}
