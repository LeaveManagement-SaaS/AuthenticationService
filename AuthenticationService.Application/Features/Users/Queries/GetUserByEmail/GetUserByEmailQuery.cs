using AuthenticationService.Application.DTOs.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
