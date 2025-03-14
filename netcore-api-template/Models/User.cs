namespace netcore_api_template.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid DeletedBy { get; set; }
}

public class UserLoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UserDataDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}