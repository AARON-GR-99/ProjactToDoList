using Api.Models.DTO;
using Api.Services.Catalogs;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

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
            return Ok(createUser);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a User.");
            return BadRequest();
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
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating the user with ID {UserId}", id);
            return BadRequest();
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Attempt to retrieve all users");

        try
        {
            var users = await userService.GetAllAsync();
            if (!users.Any())
            {
                logger.LogWarning("No users found");
                return NotFound("No users found");
            }

            var userDtos = mapper.Map<List<UserDto>>(users);
            logger.LogInformation("Users retrieved successfully");
            return Ok(userDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all users.");
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        logger.LogInformation("Attempt to retrieve user with ID {UserId}", id);

        try
        {
            var user = await userService.GetByIdAsync(id);
            if (user.UserId == 0)
            {
                logger.LogWarning("User with ID {UserId} not found", id);
                return NotFound(id);
            }

            logger.LogInformation("User with ID {UserId} retrieved successfully", id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving the User with ID {UserId}", id);
            return BadRequest();
        }
    }
}
