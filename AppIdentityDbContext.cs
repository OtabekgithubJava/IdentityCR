using IdentityAuth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityAuth;

public class AppIdentityDbContext: IdentityDbContext<User>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) 
        : base(options)
    {
        
    }
}