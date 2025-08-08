using Api.Models.DTO;

namespace Api.Services.Catalogs;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task<TaskDto> GetByIdAsync(int taskId);
    Task<TaskDto> CreateAsync(TaskDto taskDto);
    Task<TaskDto> UpdateAsync(TaskDto taskDto);
}