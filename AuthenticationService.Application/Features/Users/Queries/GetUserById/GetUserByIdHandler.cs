using AuthenticationService.Application.DTOs.Users;
using AuthenticationService.Application.Exceptions;
using AuthenticationService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Application.Features.Users.Queries.GetUserById
{
    
    namespace AuthenticationService.Application.Queries.GetUserById
    {
        public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            private readonly IUserRepository _userRepository;

            public GetUserByIdHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<UserDto> Handle(
                GetUserByIdQuery request,
                CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new NotFoundException($"User with Id '{request.Id}' was not found.");
                }

                return new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    IsEmailVerified = user.IsEmailVerified,
                    Status = user.Status,
                    CreatedDate = user.CreatedDate
                };

               
            }
        }
    }
}
