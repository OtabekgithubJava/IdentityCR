using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityAuth.DTOs;
using IdentityAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAuth.Services;

public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AuthDTO> GenerateToken(User user)
        {
            if(user == null)
            {
                return new AuthDTO()
                {
                    Message = "User is Null",
                    StatusCode = 404
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings").GetSection("SecretKey").Value!);
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = [
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Name, user.FullName!),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new(JwtRegisteredClaimNames.NameId, user.Id!),
                new(JwtRegisteredClaimNames.Aud, _configuration["JWTSettings:ValidAudience"]!),
                new(JwtRegisteredClaimNames.Iss, _configuration["JWTSettings:ValidIssuer"]!),
                new(JwtRegisteredClaimNames.Exp, _configuration["JWTSettings:ExpireDate"]!),
              ];
            foreach(string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDeskriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Double.Parse(_configuration["JWTSettings:ExpireDate"]!)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            }; 
            var token = tokenHandler.CreateToken(tokenDeskriptor);

            return new AuthDTO()
            {
                Token = tokenHandler.WriteToken(token),
                Message = "Token created",
                StatusCode =  200,
                IsSuccess = true
            };

        }
    }

