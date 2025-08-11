using Api.Models.DTO;
using Data.Entities;

namespace TodoList.Mappings;

public class ProfileProfile : AutoMapper.Profile
{
    public ProfileProfile()
    {
        CreateMap<Profile, ProfileDto>().ReverseMap();
    }
}