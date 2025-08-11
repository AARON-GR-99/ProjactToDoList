using Api.Models.DTO;

namespace Api.Services.Catalogs;

public interface IProfileService
{
    Task<IEnumerable<ProfileDto>> GetAllAsync();
    Task<ProfileDto> CreateAsync(ProfileDto profileDto);
    Task<ProfileDto> UpdateAsync(ProfileDto profileDto);
    Task<ProfileDto> GetByIdAsync(int profileId);
    Task<IEnumerable<ProfileDto>> GetAllActiveAsync();
}