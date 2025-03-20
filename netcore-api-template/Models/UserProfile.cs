using AutoMapper;
using netcore_api_template.Models;

namespace netcore_api_template.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDataDto>(); // Maps User → UserDataDto
        CreateMap<User, CreateUserDto>().ReverseMap(); // Maps User → CreateUserDto and CreateUserDto → User
        CreateMap<User, UpdateUserDto>().ReverseMap(); // Maps User → UpdateUserDto and UpdateUserDto → User
    }
}
