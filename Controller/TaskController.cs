using Api.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[Authorize]
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
            var createdTask = await taskService.CreateAsync(taskDto);
            logger.LogInformation("Task created successfully with ID {TaskId}", createdTask.TaskId);
            return Ok(ApiResponseHelper.Success(createdTask, "Task created successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a task.");
            return StatusCode(500, ApiResponseHelper.Error<TaskDto>("An error occurred while creating the task", new ValidationError { Field = "InternalError", Message = ex.Message }));
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
            return Ok(ApiResponseHelper.Success(updatedTask, "Task updated successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating the task with ID {TaskId}", id);
            return StatusCode(500, ApiResponseHelper.Error<TaskDto>("An error occurred while updating the task", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Attempt to retrieve all tasks");

        try
        {
            var tasks = await taskService.GetAllAsync();
            if (tasks == null || !tasks.Any())
            {
                logger.LogWarning("No tasks found");
                return NotFound(ApiResponseHelper.Error<List<TaskDto>>("No tasks found."));
            }

            var taskDtos = mapper.Map<List<TaskDto>>(tasks);
            logger.LogInformation("Tasks retrieved successfully");
            return Ok(ApiResponseHelper.Success(taskDtos, "Tasks retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all tasks.");
            return StatusCode(500, ApiResponseHelper.Error<List<TaskDto>>("An error occurred while retrieving the tasks. Please contact support.", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        logger.LogInformation("Attempt to retrieve task with ID {TaskId}", id);

        try
        {
            var task = await taskService.GetByIdAsync(id);
            if (task == null || task.TaskId == 0)
            {
                logger.LogWarning("Task with ID {TaskId} not found", id);
                return NotFound(ApiResponseHelper.Error<TaskDto>("Task not found."));
            }

            logger.LogInformation("Task with ID {TaskId} retrieved successfully", id);
            return Ok(ApiResponseHelper.Success(task, "Task retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving the task with ID {TaskId}", id);
            return StatusCode(500, ApiResponseHelper.Error<TaskDto>("An error occurred while retrieving the task", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("getactive")]
    public async Task<IActionResult> GetActiveTasks()
    {
        logger.LogInformation("Attempt to retrieve active Tasks");

        try
        {
            var activeTasks = await taskService.GetActiveTasksAsync();
            if (activeTasks == null || !activeTasks.Any())
            {
                logger.LogWarning("No active Tasks found");
                return NotFound(ApiResponseHelper.Error<IEnumerable<TaskDto>>("No active Tasks found."));
            }

            logger.LogInformation("Active tasks retrieved successfully");
            return Ok(ApiResponseHelper.Success(activeTasks, "Active Tasks retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving active tasks.");
            return StatusCode(500, ApiResponseHelper.Error<string>("An error occurred while retrieving active tasks. Please contact support.", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }
}
