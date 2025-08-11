using Api.Models.DTO;
using Api.Services.Extencion;
using Data.Catalog;
using Microsoft.Extensions.Logging;

namespace Api.Services.Catalogs;

public class LoginService(IUserRepository userRepository, IHashService hashService, ILogger<UserService> logger) : ILoginService
{

    public async Task<string?> LoginAsync(UserLoginDto userLoginDto)
    {
        logger.LogInformation("Intentando iniciar sesión para {Correo}", userLoginDto.Correo);
        // 1. Buscar usuario por correo
        var user = await userRepository.GetByUserNameAsync(userLoginDto.Correo);
        if (user == null)
        {
            logger.LogInformation("Usuario con correo {Correo} no encontrado", userLoginDto.Correo);
            return null;
        }
        
        bool passwordIsValid = hashService.Verify(userLoginDto.Password, user.Password);
        if (!passwordIsValid)
        {
            logger.LogWarning("Contraseña incorrecta para {Correo}", userLoginDto.Correo);
            return null;
        }
        
        logger.LogInformation("Usuario {Correo} autenticado correctamente", userLoginDto.Correo);
        return "Success";;
    }
    
}