using Api.Models.DTO;
using Data.Entities;

namespace TodoList.Mappings;

public class TaskProfile : AutoMapper.Profile
{
    public TaskProfile()
    {
        CreateMap<Data.Entities.Task, TaskDto>().ReverseMap();
    }
}