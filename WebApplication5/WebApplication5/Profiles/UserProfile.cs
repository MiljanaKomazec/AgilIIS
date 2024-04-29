using AutoMapper;
using WebApplication5.DTO;
using WebApplication5.Models;

namespace WebApplication5.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRoleDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<Role, RoleDto>();
        }
    }
}
