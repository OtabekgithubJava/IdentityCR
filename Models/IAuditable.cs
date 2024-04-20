namespace IdentityAuth.Models;

public interface IAuditable
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public DateTimeOffset DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    
}