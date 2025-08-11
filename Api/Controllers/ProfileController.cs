using Api.Models.DTO;
using Api.Services.Catalogs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(
    IMapper mapper,
    IProfileService profileService,
    ILogger<Data.Entities.Profile> logger)
    : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ProfileDto profileDto)
    {
        logger.LogInformation("Attempt to create a new Profile");

        try
        {
            var createdProfile = await profileService.CreateAsync(profileDto);
            logger.LogInformation("Profile created successfully with ID {ProfileId}", createdProfile.ProfileId);
            return Ok(createdProfile);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a Profile.");
            return BadRequest();
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProfileDto profileDto)
    {
        logger.LogInformation("Attempt to update Profile with ID {ProfileId}", id);

        try
        {
            if (id != profileDto.ProfileId)
            {
                return BadRequest("Profile ID mismatch.");
            }

            var updatedProfile = await profileService.UpdateAsync(profileDto);
            logger.LogInformation("Profile updated successfully with ID {ProfileId}", updatedProfile.ProfileId);
            return Ok(updatedProfile);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating the Profile with ID {ProfileId}", id);
            return BadRequest();
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Attempt to retrieve all profiles");

        try
        {
            var profiles = await profileService.GetAllAsync();
            if (!profiles.Any())
            {
                logger.LogWarning("No profiles found");
                return NotFound("No profiles found");
            }

            var profileDtos = mapper.Map<List<ProfileDto>>(profiles);
            logger.LogInformation("Profiles retrieved successfully");
            return Ok(profileDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all profiles.");
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        logger.LogInformation("Attempt to retrieve profile with ID {ProfileId}", id);

        try
        {
            var profile = await profileService.GetByIdAsync(id);
            if (profile.ProfileId == 0)
            {
                logger.LogWarning("Profile with ID {ProfileId} not found", id);
                return NotFound(id);
            }

            logger.LogInformation("Profile with ID {ProfileId} retrieved successfully", id);
            return Ok(profile);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving the Profile with ID {ProfileId}", id);
            return BadRequest();
        }
    }
}
