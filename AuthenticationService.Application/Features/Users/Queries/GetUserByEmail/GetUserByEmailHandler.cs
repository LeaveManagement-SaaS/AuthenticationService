using AuthenticationService.Application.Exceptions;
using AuthenticationService.Domain.Interfaces;
using global::AuthenticationService.Application.DTOs.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Application.Features.Users.Queries.GetUserByEmail
{
    
    namespace AuthenticationService.Application.Queries.GetUserByEmail
    {
        public class GetUserByEmailHandler
            : IRequestHandler<GetUserByEmailQuery, UserDto>
        {
            private readonly IUserRepository _userRepository;

            public GetUserByEmailHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<UserDto> Handle(
                GetUserByEmailQuery request,
                CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByEmailAsync(request.Email);

                if (user != null)
                {
                    throw new ConflictException($"User with email '{request.Email}' already exists.");
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
 