using Api.Models.DTO;
using AutoMapper;
using Data.Repisitories.Catalogs;
using Microsoft.Extensions.Logging;

namespace Api.Services.Catalogs;

public class TaskService(ITaskRepository taskRepository, IMapper mapper, ILogger<TaskService> logger) : ITaskService
{
    public async Task<TaskDto> CreateAsync(TaskDto taskDto)
    {
        logger.LogInformation("Attempting to create a new task");

        var task = mapper.Map<Data.Entities.Task>(taskDto);
        await taskRepository.AddAsync(task);
        await taskRepository.SaveChangesAsync();

        logger.LogInformation("Task created successfully with ID {TaskId}", task.TaskId);

        return mapper.Map<TaskDto>(task);
    }

    public async Task<TaskDto> UpdateAsync(TaskDto taskDto)
    {
        logger.LogInformation("Attempting to update task with ID {TaskId}", taskDto.TaskId);

        var task = mapper.Map<Data.Entities.Task>(taskDto);
        await taskRepository.UpdateAsync(task);
        await taskRepository.SaveChangesAsync();

        logger.LogInformation("Task updated successfully with ID {TaskId}", task.TaskId);

        return mapper.Map<TaskDto>(task);
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        logger.LogInformation("Fetching all tasks");

        var tasks = await taskRepository.GetAllAsync();
        return mapper.Map<IEnumerable<TaskDto>>(tasks);
    }

    public async Task<TaskDto> GetByIdAsync(int taskId)
    {
        logger.LogInformation("Fetching task with ID {TaskId}", taskId);

        var task = await taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            logger.LogWarning("Task with ID {TaskId} not found", taskId);
            throw new KeyNotFoundException($"Task with ID {taskId} not found");
        }

        return mapper.Map<TaskDto>(task);
    }
}
