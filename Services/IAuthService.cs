using IdentityAuth.DTOs;
using IdentityAuth.Models;

namespace IdentityAuth.Services;

public interface IAuthService
{
    public Task<AuthDTO> GenerateToken(User user);
}
