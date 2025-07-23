using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
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
    public class AuthenticationController : BaseController
    {
        private readonly Service.Interface.IUserService _userService;//to fix ambiguous ref
        private readonly IEmailService _emailService;
        private readonly ITwoFactorAuthentication _twoFactorAuthentication;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IUserService userService, IEmailService emailService, ITwoFactorAuthentication twoFactorAuthentication, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _emailService = emailService;
            _twoFactorAuthentication = twoFactorAuthentication;
            _authenticationService = authenticationService;
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
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLoginDetails) => BuildHttpResponse(await _authenticationService.LoginAsync(userLoginDetails));

        /// <summary>
        /// This is the endpoint that logs the user in to the application and generates the JWT token.
        /// </summary>
        /// <param name="userLoginDetails"></param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server Error</response>
        /// <returns></returns>
        [HttpPost("login-with-2fa/{userEmail}/{OTP}/{timeWindow}")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2FAAsync([FromRoute] string userEmail, [FromRoute] string OTP, [FromRoute] long timeWindow) => BuildHttpResponse(await _authenticationService.LoginWith2FAAsync(userEmail, OTP));

        [HttpPost("forgot-password/{userEmail}")]

        public async Task<IActionResult> ForgotPassword([FromRoute] string userEmail) => BuildHttpResponse(await _authenticationService.RequestForgotPasswordOTP(userEmail));
        [HttpPatch("reset-password/{OTPCode}/{newPassword}/{confirmPassword}")]
        public async Task<IActionResult> ResetPassword([FromRoute] string OTPCode,[FromRoute] string newPassword,[FromRoute]string confirmPassword)
        {
            var response =await _authenticationService.ResetPassword(OTPCode,newPassword,confirmPassword);
            return BuildHttpResponse(response);
        }
        [HttpPost("request-change-password-token/{currentPassword}/{userEmail}")]
        public async Task<IActionResult> RequestChangePasswordOTP([FromRoute]string currentPassword,[FromRoute] string userEmail)
        {
            var response = await _authenticationService.RequestChangePasswordOTP(currentPassword,userEmail);
            return BuildHttpResponse(response);
        }
        [HttpPost("change-password/{OTPCode}/{newPassword}/{confirmPassword}")]
        public async Task<IActionResult> RequestChangePasswordOTP([FromRoute] string OTPCode, [FromRoute] string newPassword, [FromRoute] string confirmPassword)
        {
            var response = await _authenticationService.ChangePassword(OTPCode, newPassword, confirmPassword);
            return BuildHttpResponse(response);;
        }
    }
}
