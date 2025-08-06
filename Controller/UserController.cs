using Api.Models.DTO;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController(
    IMapper mapper,
    IUserService userService,
    ILogger<User> logger)
    : ControllerBase
{

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        logger.LogInformation("Attempt to create a new User");

        try
        {
            var createUser = await userService.CreateAsync(userDto);
            logger.LogInformation("User created successfully with ID {UserId}", createUser.UserId);
            return Ok(ApiResponseHelper.success(createUser, "User created successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a User.");
            return StatusCode(500, ApiResponseHelper.Error<UserDto>("An error occurred while creating the User", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
    {
        logger.LogInformation("Attempt to update User with ID {UserId}", id);

        try
        {
            if (id != userDto.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            var updatedUser = await userService.UpdateAsync(userDto);
            logger.LogInformation("User updated successfully with ID {UserId}", updatedUser.UserId);
            return Ok(ApiResponseHelper.Success(updatedUser, "User updated successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating the user with ID {UserId}", id);
            return StatusCode(500, ApiResponseHelper.Error<UserDto>("An error occurred while updating the user", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Attempt to retrieve all users");

        try
        {
            var users = await userService.GetAllAsync();
            if (users == null || !users.Any())
            {
                logger.LogWarning("No users found");
                return NotFound(ApiResponseHelper.Error<List<UserDto>>("No users found."));
            }

            var userDtos = mapper.Map<List<UserDto>>(users);
            logger.LogInformation("Users retrieved successfully");
            return Ok(ApiResponseHelper.Success(userDtos, "Users retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all users.");
            return StatusCode(500, ApiResponseHelper.Error<List<UserDto>>("An error occurred while retrieving the banks. Please contact support.", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        logger.LogInformation("Attempt to retrieve user with ID {UserId}", id);

        try
        {
            var user = await userService.GetByIdAsync(id);
            if (user == null || user.UserId == 0)
            {
                logger.LogWarning("User with ID {UserId} not found", id);
                return NotFound(ApiResponseHelper.Error<UserDto>("User not found."));
            }

            logger.LogInformation("User with ID {UserId} retrieved successfully", id);
            return Ok(ApiResponseHelper.Success(user, "User retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving the User with ID {UserId}", id);
            return StatusCode(500, ApiResponseHelper.Error<UserDto>("An error occurred while retrieving the user", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }

    [HttpGet("getactive")]
    public async Task<IActionResult> GetActiveUsers()
    {
        logger.LogInformation("Attempt to retrieve active users");

        try
        {
            var activeUsers = await userService.GetActiveUsersAsync();
            if (activeUsers == null || !activeUsers.Any())
            {
                logger.LogWarning("No active users found");
                return NotFound(ApiResponseHelper.Error<IEnumerable<UserDto>>("No active users found."));
            }

            logger.LogInformation("Active urses retrieved successfully");
            return Ok(ApiResponseHelper.Success(activeUsers, "Active users retrieved successfully."));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving active users.");
            return StatusCode(500, ApiResponseHelper.Error<string>("An error occurred while retrieving active users. Please contact support.", new ValidationError { Field = "InternalError", Message = ex.Message }));
        }
    }
}
