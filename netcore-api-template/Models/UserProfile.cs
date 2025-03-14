using AutoMapper;
using netcore_api_template.Models;

namespace netcore_api_template.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDataDto>(); // Maps User → UserDataDto
    }
}
