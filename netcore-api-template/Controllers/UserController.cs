using System.Security.Claims;
using AutoMapper;
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
public class UserController(
    IUserService userService,
    IMapper _mapper
    ) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers([FromQuery] QueryParameters queryParams)
    {
        try
        {
            return Ok(await userService.All(queryParams));
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<IEnumerable<string>>
                {
                    Success = false,
                    Data = null,
                    Message = "Error: " + ex.Message
                }
            );
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    Success = false,
                    Data = null,
                    Message = "Invalid user id."
                });

            var user = await userService.GetById(id);
            return Ok(
                new ApiResponse<UserDataDto>
                {
                    Success = true,
                    Data = user,
                    Message = "Get user retrieved successfully."
                }
            );
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<IEnumerable<string>>
                {
                    Success = false,
                    Data = null,
                    Message = "Error: " + ex.Message
                }
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        try
        {
            var dtCreate = _mapper.Map<User>(createUserDto);

            dtCreate.CreatedAt = DateTime.UtcNow;
            dtCreate.Password = PasswordHasher.HashPassword(dtCreate.Password);
            dtCreate.CreatedBy = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await userService.Create(dtCreate);
            // return Ok(result);
            return CreatedAtAction(
                        nameof(GetById),
                        new { id = result },
                        new ApiResponse<CreateUserDto>
                        {
                            Success = true,
                            Data = createUserDto,
                            Message = "User created successfully."
                        }
                    );
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<IEnumerable<string>>
                {
                    Success = false,
                    Data = null,
                    Message = "Error: " + ex.Message
                }
            );
        }

    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
    {
        try
        {
            if (updateUserDto.Id == Guid.Empty)
                return BadRequest(new ApiResponse<IEnumerable<string>>
                {
                    Success = false,
                    Data = null,
                    Message = "Invalid user id."
                });


            var dtUpdate = _mapper.Map<User>(updateUserDto);

            if (dtUpdate.Password != null)
                dtUpdate.Password = PasswordHasher.HashPassword(dtUpdate.Password);

            dtUpdate.UpdatedAt = DateTime.UtcNow;
            dtUpdate.UpdatedBy = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await userService.Update(dtUpdate);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<IEnumerable<string>>
                {
                    Success = false,
                    Data = null,
                    Message = "Error: " + ex.Message
                }
            );
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await userService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<IEnumerable<string>>
                {
                    Success = false,
                    Data = null,
                    Message = "Error: " + ex.Message
                }
            );
        }
    }
}
