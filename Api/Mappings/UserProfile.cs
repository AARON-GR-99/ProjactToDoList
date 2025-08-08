using Api.Models.DTO;
using Data.Entities;

namespace TodoList.Mappings;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<User,  UserDto>().ReverseMap();
    }
}