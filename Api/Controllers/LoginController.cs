using Api.Models.DTO;
using Api.Services.Catalogs;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]

public class LoginController(ILoginService loginService) : ControllerBase
{

    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Datos inválidos.");

        var result = await loginService.LoginAsync(loginDto);

        if (result != "Success")
            return Unauthorized("Usuario o contraseña incorrectos.");

        return Ok(result);
    }
    
}