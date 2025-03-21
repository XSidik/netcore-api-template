using System.Globalization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using netcore_api_template.Data;
using netcore_api_template.Models;

namespace netcore_api_template.Services;
public interface IUserService
{
    Task<ApiResponsePaginated<List<UserDataDto>>> All(QueryParameters queryParams);
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

    public async Task<ApiResponsePaginated<List<UserDataDto>>> All(QueryParameters queryParams)
    {
        var query = _context.Users.Where(u => u.IsDelete == false).AsNoTracking();

        if (!string.IsNullOrWhiteSpace(queryParams.Filter))
        {
            query = query.Where(u => EF.Functions.Like(u.Name, $"%{queryParams.Filter}%") ||
                                    EF.Functions.Like(u.Email, $"%{queryParams.Filter}%"));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Sort))
        {
            var isDescending = queryParams.Sort.StartsWith('-');
            var sortBy = isDescending ? queryParams.Sort[1..] : queryParams.Sort;
            sortBy = char.ToUpper(sortBy[0]) + sortBy[1..];

            query = isDescending
                ? query.OrderByDescending(x => EF.Property<object>(x, sortBy))
                : query.OrderBy(x => EF.Property<object>(x, sortBy));
        }

        var pagedData = await query.Skip((queryParams.PageNumber - 1) * queryParams.PageSize).
                        Take(queryParams.PageSize).
                        Select(u => new UserDataDto
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Email = u.Email,
                            CreatedAt = u.CreatedAt,
                            CreatedBy = u.CreatedBy,
                            CreatedByName = _context.Users.Where(x => x.Id == u.CreatedBy).FirstOrDefault()!.Name,
                            UpdatedAt = u.UpdatedAt.HasValue ? u.UpdatedAt.Value : null,
                            UpdatedBy = u.UpdatedBy,
                            UpdatedByName = _context.Users.Where(x => x.Id == u.UpdatedBy).FirstOrDefault()!.Name,
                            IsActive = u.IsActive
                        }).ToListAsync();

        return new ApiResponsePaginated<List<UserDataDto>>
        {
            Success = true,
            Message = "List user retrieved successfully.",
            Data = pagedData,
            TotalCount = await query.CountAsync(),
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize
        };
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
