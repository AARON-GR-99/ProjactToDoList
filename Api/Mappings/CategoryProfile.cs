using Api.Models.DTO;
using Data.Entities;

namespace TodoList.Mappings;

public class CategoryProfile : AutoMapper.Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}