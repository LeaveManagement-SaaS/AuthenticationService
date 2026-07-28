using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuthenticationService.Application.DTOs.Users;

namespace AuthenticationService.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }
}
