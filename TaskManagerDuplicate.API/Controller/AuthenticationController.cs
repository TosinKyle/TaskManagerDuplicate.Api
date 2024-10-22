using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// This is the endpoint that logs the user in to the application and generates the JWT token.
        /// </summary>
        /// <param name="userLoginDetails"></param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server Error</response>
        /// <returns></returns>
        /*[HttpPost("login")]
        [AllowAnonymous]*/
        /* public IActionResult Login([FromBody] UserLoginDto userLoginDetails)
         {
             var user = await _userService.LoginAsync(userLoginDetails);//to return user details
             if (user != null)
             {
                 if (user.IsActive)
                 {
                     var token = JWTTokenHelper.Generate(user);  //gen token based on user details
                     return Ok($"Here is your sign in token :{token}");
                 }
                 else
                 {
                     return BadRequest("User is inactive");
                 }
             }
             return BadRequest("Cannot generate token due to incorrect details");
         }*/
    
    }
}
