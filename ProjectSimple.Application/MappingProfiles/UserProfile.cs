using AutoMapper;
using ProjectSimple.Application.Services.User.Commands.CreateUser;
using ProjectSimple.Application.Services.User.Queries.GetUserDetails;
using ProjectSimple.Application.Services.User.Queries.GetUsers;
using ProjectSimple.Domain.Models;

namespace ProjectSimple.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UserDetailsDTO>();
        CreateMap<CreateUserCommand, User>();
    }
}
