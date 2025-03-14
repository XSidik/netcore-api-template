using Microsoft.EntityFrameworkCore;
using netcore_api_template.Data;
using netcore_api_template.Helpers;
using netcore_api_template.Models;

namespace netcore_api_template.Services;
public interface IAuthService
{
    Task<User> LoginAsync(UserLoginDto userDto);
}


public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;

    }

    public async Task<User> LoginAsync(UserLoginDto userDto)
    {
        var user = await _context.Users.Where(u => u.Email == userDto.Email).FirstOrDefaultAsync();
        if (user is null || !PasswordHasher.VerifyPassword(userDto.Password, user.Password))
            return user!;

        return user!;
    }

}
