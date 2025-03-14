using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcore_api_template.Models;
using netcore_api_template.Services;

namespace netcore_api_template.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    ITokenService tokenService,
    IAuthService authService
    ) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
    {
        var user = await authService.LoginAsync(userDto);
        if (user is null)
            return Unauthorized("Invalid email or password.");

        var token = tokenService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }
}
