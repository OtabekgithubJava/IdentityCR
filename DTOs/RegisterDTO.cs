namespace IdentityAuth.DTOs;

public class RegisterDTO
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int Age { get; set; }
    public string? Gender { get; set; }

    public IList<string> Roles { get; set; }
}