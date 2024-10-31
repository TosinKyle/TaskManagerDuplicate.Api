using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.Implementation;
using TaskManagerDuplicate.Service.Interface;
using IUserService = TaskManagerDuplicate.Service.Interface.IUserService;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly Service.Interface.IUserService _userService;//to fix ambiguous ref
        private readonly IEmailService _emailService;
        private readonly ITwoFactorAuthentication _twoFactorAuthentication;
        public AuthenticationController(IUserService userService, IEmailService emailService,ITwoFactorAuthentication twoFactorAuthentication)
        {
            _userService = userService;
            _emailService = emailService;
            _twoFactorAuthentication = twoFactorAuthentication;
        }

        /// <summary>
        /// This is the endpoint that is responsible for logging in the user.
        /// </summary>
        /// <param name="userLoginDetails"></param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server Error</response>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDetails)
        {
            //var response = await _taskService.UpdateToDoTaskStatusAsync(toUpdateTaskStatus, taskId, userId);
            //return BuildHttpResponse(response);
            var user = await _userService.LoginAsync(userLoginDetails);//to return user details
             if (user.Data != null)
             {
                if (user.Data.IsActive)
                {
                    if (user.Data.IsTwoFactorEnabled)
                    {
                        var messageToBeSent = _twoFactorAuthentication.GenerateOTP();
                        var subject = "OTP CONFIRMATION";
                        _emailService.SendEmailWithGmailClient(subject, messageToBeSent, new List<string> { user.Data.EmailAddress });
                        return Ok($"We have sent an OTP to your email");
                    }
                        return BadRequest("User is not 2FA enabled");
                }           
                     return BadRequest("User is inactive");
             }
            return BadRequest("User could not be found");
        }

        /// <summary>
        /// This is the endpoint that logs the user in to the application and generates the JWT token.
        /// </summary>
        /// <param name="userLoginDetails"></param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server Error</response>
        /// <returns></returns>
        [HttpPost("login-with-2fa")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2FA(string userEmail, string OTP,long timeWindow)
        {
            //var response = await _taskService.UpdateToDoTaskStatusAsync(toUpdateTaskStatus, taskId, userId);
            //return BuildHttpResponse(response);
            var user = await _userService.GetSingleUserByEmailAsync(userEmail);//to return user details
            if (user != null)
            {

                var response = _twoFactorAuthentication.VerifyOTP(OTP,out timeWindow);
                if (!response)
                {
                    return BadRequest("User could not be logged in");
                }
                var token = JWTTokenHelper.Generate(user.Data);
                return Ok($"Here is your sign in token :{token}");//gen token based on user details

            }
            return BadRequest("User could not be found");
        }

    }
}
