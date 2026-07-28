using AutoMapper;
using AuthenticationService.Application.DTOs.Users;
using AuthenticationService.Domain.Entities;
using AuthenticationService.CrossCutting.Users.Commands.CreateUser;
using AuthenticationService.CrossCutting.Users.Commands.UpdateUser;

namespace AuthenticationService.CrossCutting.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Entity -> DTO
            CreateMap<User, UserDto>();

            // DTO -> Entity
            CreateMap<UserDto, User>();

            // Create Command -> Entity
            CreateMap<CreateUserCommand, User>().ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Update Command -> Entity
            CreateMap<UpdateUserCommand, User>();
        }
    }
}