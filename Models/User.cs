using Microsoft.AspNetCore.Identity;

namespace IdentityAuth.Models;

public class User: IdentityUser, IAuditable
{
    public string FullName { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public DateTimeOffset DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}