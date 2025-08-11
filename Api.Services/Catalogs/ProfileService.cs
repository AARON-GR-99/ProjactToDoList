using Api.Models.DTO;
using AutoMapper;
using Data.Catalog;
using Data.Entities;
using Microsoft.Extensions.Logging;

namespace Api.Services.Catalogs;

public class ProfileService(IProfileRepository profileRepository, IMapper mapper, ILogger<ProfileService> logger) : IProfileService
{
    public async Task<ProfileDto> CreateAsync(ProfileDto profileDto)
    {
        logger.LogInformation("Attempting to create a new profile");

        var profile = mapper.Map<Data.Entities.Profile>(profileDto);
        await profileRepository.AddAsync(profile);
        await profileRepository.SaveChangesAsync();

        logger.LogInformation("Profile created successfully with ID {ProfileId}", profile.ProfileId);
        return mapper.Map<ProfileDto>(profile);
    }

    public async Task<ProfileDto> UpdateAsync(ProfileDto profileDto)
    {
        logger.LogInformation("Attempting to update profile with ID {ProfileId}", profileDto.ProfileId);

        var profile = mapper.Map<Data.Entities.Profile>(profileDto);
        await profileRepository.UpdateAsync(profile);
        await profileRepository.SaveChangesAsync();

        logger.LogInformation("Profile updated successfully with ID {ProfileId}", profile.ProfileId);
        return mapper.Map<ProfileDto>(profile);
    }

    public async Task<IEnumerable<ProfileDto>> GetAllAsync()
    {
        logger.LogInformation("Fetching all profiles");
        var profiles = await profileRepository.GetAllAsync();
        return mapper.Map<IEnumerable<ProfileDto>>(profiles);
    }

    public async Task<ProfileDto> GetByIdAsync(int profileId)
    {
        logger.LogInformation("Fetching profile with ID {ProfileId}", profileId);
        var profile = await profileRepository.GetByIdAsync(profileId);
        return mapper.Map<ProfileDto>(profile);
    }

    public async Task<IEnumerable<ProfileDto>> GetAllActiveAsync()
    {
        logger.LogInformation("Fetching all active profiles");
        var profiles = await profileRepository.GetAllAsync();
        var activeProfiles = profiles.Where(p => p.IsActive);
        return mapper.Map<IEnumerable<ProfileDto>>(activeProfiles);
    }
}
