using Api.Models.DTO;

namespace Api.Services.Catalogs;

public interface ILoginService
{
    Task<string?> LoginAsync(UserLoginDto loginDto);
}