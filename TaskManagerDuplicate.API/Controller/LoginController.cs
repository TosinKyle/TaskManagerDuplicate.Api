using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService; //why faint
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        private UserLoginDto AuthenticateUser(UserLoginDto Users)
        {
            var response = _userService.GetUserByPassword(Users.Password);
            var response1 = _userService.GetUserByEmail(Users.EmailAddress);
            if (response!=null && response1!=null) 
            {
                return Users;
            }
            return null;
            /*UserLoginDto _user = null;
            if (Users.EmailAddress == "tosinkyle91@gmail.com" & Users.Password=="Showbiz21@?")
            {
             _user = new UserLoginDto { EmailAddress = "tosinkyle91@gmail.com" };
                return _user;
            }
            return null;*/

        }
        private string GenerateToken(UserLoginDto users)
        {

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],_configuration["Jwt:Audience"],null,
                expires:DateTime.Now.AddMinutes(2),
                signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
        [AllowAnonymous]
        [HttpPost("user-login")]
        public IActionResult Login([FromBody] UserLoginDto users)
        {
            var user = AuthenticateUser(users);
            if (user != null) 
            {
                var token = GenerateToken(user);
                var response = Ok(new { token = token});
                return response;
            }
            else
                return NotFound("User not found");
        }
    }
}
