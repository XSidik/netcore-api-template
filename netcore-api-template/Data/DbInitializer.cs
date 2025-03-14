using Microsoft.EntityFrameworkCore;
using netcore_api_template.Models;
using netcore_api_template.Helpers;

namespace netcore_api_template.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Ensure database is created and migrated
            context.Database.Migrate();

            // Seed Users (if empty)
            if (!context.Users.Any())
            {
                context.Users.AddRange(new List<User>
                {
                    new User { Name = "Admin", Email = "admin@admin.com", Password=PasswordHasher.HashPassword("admin123"), CreatedAt = DateTime.UtcNow }
                });

                context.SaveChanges();
            }
        }
    }
}
