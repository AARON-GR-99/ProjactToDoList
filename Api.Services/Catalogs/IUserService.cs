using Api.Models.DTO;

namespace Api.Services.Catalogs;

public interface IUserService
{
    Task<IEnumerable<UserDto> > GetAllAsync();
    Task<UserDto> CreateAsync(UserDto userDto);
    Task<UserDto> UpdateAsync(UserDto userDto);
    Task<UserDto> GetByIdAsync(int userId);
    Task<IEnumerable<UserDto>> GetAllByUserIdAsync();
}