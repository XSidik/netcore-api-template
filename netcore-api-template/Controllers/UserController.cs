using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcore_api_template.Data;
using netcore_api_template.Helpers;
using netcore_api_template.Models;
using netcore_api_template.Services;

namespace netcore_api_template.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userService.All();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid user id.");

        var user = await userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        user.Password = PasswordHasher.HashPassword(user.Password);
        var result = await userService.Create(user);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(User user)
    {
        if (user.Id == Guid.Empty)
            return BadRequest("Invalid user id.");

        if (user.Password != null)
            user.Password = PasswordHasher.HashPassword(user.Password);

        user.UpdatedAt = DateTime.UtcNow;
        var result = await userService.Update(user);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await userService.Delete(id);
        return Ok(result);
    }
}
