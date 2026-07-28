using AuthenticationService.Application.Exceptions;
using AuthenticationService.Domain.Interfaces;
using AutoMapper;
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
            private readonly IMapper _mapper;
            public GetUserByEmailHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
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

                return _mapper.Map<UserDto>(user);
            }
        }
    }
}
 