using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerDuplicate.Domain.DataTransferObjects;


namespace TaskManagerDuplicate.Helper
{
    public static class JWTTokenHelper 
    {
        private static IConfiguration _config;
        public static void InstantiateConfiguration(IConfiguration config) =>_config = config;
        public static string Generate(DisplaySingleUserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
             new Claim(ClaimTypes.NameIdentifier,user.UserName),
             new Claim(ClaimTypes.Email,user.UserEmail),
             new Claim(ClaimTypes.GivenName,user.FirstName),
             new Claim(ClaimTypes.Surname, user.LastName),
             //new Claim(ClaimTypes.Role,user.Role),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
