using Api.Models.DTO;
using Api.Services.Extencion;
using AutoMapper;
using Data.Catalog;
using Data.Entities;
using Microsoft.Extensions.Logging;

namespace Api.Services.Catalogs;

public class UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger, IHashService hashService) : IUserService
{
    public async Task<UserDto> CreateAsync(UserDto userDto)
    {
        logger.LogInformation("Attempting to create a new user");
        
        var user = mapper.Map<User>(userDto);
        
        if (!string.IsNullOrWhiteSpace(userDto.Password))
        {
            user.Password = hashService.Hash(userDto.Password);
        }
        
        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();
        
        logger.LogInformation("User created successfully with ID {UserId}", user.UserId);
        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateAsync(UserDto userDto)
    {
        logger.LogInformation("Attempting to update user with ID {UserId}", userDto.UserId);
        
        var user = mapper.Map<User>(userDto);
        
        if (!string.IsNullOrWhiteSpace(userDto.Password))
        {
            user.Password = hashService.Hash(userDto.Password);
        }
        else
        {
            var existingUser = await userRepository.GetByIdAsync(userDto.UserId);
            if (existingUser != null) user.Password = existingUser.Password;
        }
        
        await userRepository.UpdateAsync(user);
        await userRepository.SaveChangesAsync();
        
        logger.LogInformation("User updated successfully with ID {UserId}", user.UserId);
        return mapper.Map<UserDto>(user); 
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        logger.LogInformation("Fetching all user");
        var users = await userRepository.GetAllAsync();
        return mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetByIdAsync(int userId)
    {
        logger.LogInformation("Fetching user with ID {UserId}", userId);
        var user = await userRepository.GetByIdAsync(userId);
        return mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllByUserIdAsync()
    {
        logger.LogInformation("Fetching active user from service");
        var users = await userRepository.GetAllAsync();
        return mapper.Map<IEnumerable<UserDto>>(users);
    }
}