using AuthenticationService.Application.DTOs.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
