using Microsoft.EntityFrameworkCore;
using netcore_api_template.Models;

namespace netcore_api_template.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}
