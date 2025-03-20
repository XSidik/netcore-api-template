using AutoMapper;
using Microsoft.EntityFrameworkCore;
using netcore_api_template.Data;
using netcore_api_template.Models;

namespace netcore_api_template.Services;
public interface IUserService
{
    Task<List<UserDataDto>> All();
    Task<UserDataDto> GetById(Guid id);
    Task<bool> Create(User user);
    Task<bool> Update(User user);
    Task<bool> Delete(Guid id);
}


public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(
        ApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserDataDto>> All()
    {
        var data = await _context.Users.Where(u => u.IsDelete == false).OrderByDescending(u => u.UpdatedAt != null ? u.UpdatedAt : u.CreatedAt).ToListAsync();
        var dataDto = _mapper.Map<List<UserDataDto>>(data);
        return dataDto;
    }

    public async Task<UserDataDto> GetById(Guid id)
    {
        var data = await _context.Users.Where(u => u.IsDelete == false && u.Id == id).FirstOrDefaultAsync();
        var dataDto = _mapper.Map<UserDataDto>(data);
        return dataDto;
    }

    public async Task<bool> Create(User user)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Update(User user)
    {
        try
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser == null)
                return false;

            // Preserve the original Create value
            user.CreatedAt = existingUser.CreatedAt;
            user.CreatedBy = existingUser.CreatedBy;

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
                return false;

            existingUser.IsActive = false;
            existingUser.IsDelete = true;
            existingUser.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
