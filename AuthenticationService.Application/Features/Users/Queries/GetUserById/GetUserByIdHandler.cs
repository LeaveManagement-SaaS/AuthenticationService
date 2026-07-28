using AuthenticationService.Application.DTOs.Users;
using AuthenticationService.Application.Exceptions;
using AuthenticationService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace AuthenticationService.Application.Features.Users.Queries.GetUserById
{
    
    namespace AuthenticationService.Application.Queries.GetUserById
    {
        public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(
                GetUserByIdQuery request,
                CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);


                if (user == null)
                {
                    throw new NotFoundException(
                        $"User with Id '{request.Id}' was not found.");
                }


                return _mapper.Map<UserDto>(user);

            }
        }
    }
}
