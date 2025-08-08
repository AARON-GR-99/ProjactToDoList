using Api.Models.DTO;
using Api.Services.Catalogs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TaskController(
    IMapper mapper,
    ITaskService taskService,
    ILogger<TaskController> logger)
    : ControllerBase
{

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] TaskDto taskDto)
    {
        logger.LogInformation("Attempt to create a new task");

        try
        {
            var createTask = await taskService.CreateAsync(taskDto);
            logger.LogInformation("Task created successfully with ID {TaskId}", createTask.TaskId);
            return Ok(createTask);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a task."); 
            return StatusCode(500);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskDto taskDto)
    {
        logger.LogInformation("Attempt to update task with ID {taskId}", id);

        try
        {
            if (id != taskDto.TaskId)
            {
                return BadRequest("Task ID mismatch.");
            }

            var updatedTask = await taskService.UpdateAsync(taskDto);
            logger.LogInformation("Task updated successfully with ID {TaskId}", updatedTask.TaskId); 
            return Ok(updatedTask);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating the task with ID {TaskId}", id); 
            return StatusCode(500);
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Attempt to retrieve all tasks");

        try
        {
            var tasks = await taskService.GetAllAsync();
            if (!tasks.Any())
            {
                logger.LogWarning("No tasks found"); 
                return NotFound("No tasks found");
            }

            var taskDtos = mapper.Map<List<TaskDto>>(tasks);
            logger.LogInformation("Tasks retrieved successfully"); 
            return Ok(taskDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all tasks."); 
            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        logger.LogInformation("Attempt to retrieve task with ID {TaskId}", id);

        try
        {
            var task = await taskService.GetByIdAsync(id);
            if (task.TaskId == 0)
            {
                logger.LogWarning("Task with ID {TaskId} not found", id); 
                return NotFound(id);
            }

            logger.LogInformation("Task with ID {TaskId} retrieved successfully", id); 
            return Ok(task);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving the task with ID {TaskId}", id); 
            return StatusCode(500);
        }
    }
}
