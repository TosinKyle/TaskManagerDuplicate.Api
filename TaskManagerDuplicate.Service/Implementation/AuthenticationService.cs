using TaskManagerDuplicate.Service.Interface;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.SharedModels;
using Microsoft.AspNetCore.Http;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Data.Enums;
using System.Linq.Expressions;
using FluentValidation;
using System.Collections;
using Microsoft.VisualBasic;
using TaskManagerDuplicate.Data.Repositories.Implementation;
using Microsoft.Extensions.Logging;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ITwoFactorAuthentication _twofactorauthentication;
        private readonly IOTPService _otpService;
        private readonly IOTPRepository _oTPRepository;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IUserRepository userRepository, IUserService userService, ITwoFactorAuthentication twoFactorAuthentication,IEmailService emailService, IOTPService otpService, IOTPRepository oTPRepository, ILogger<AuthenticationService> logger)
        {
            _userRepository = userRepository;
            _userService = userService;
            _twofactorauthentication = twoFactorAuthentication;
            _emailService = emailService;
            _otpService = otpService;
            _oTPRepository = oTPRepository;
            _logger = logger;
        }

        public async Task<BaseApiResponse<bool>> ChangePassword(string OTPCode, string newPassword, string confirmPassword)
        {

            try
            {
                var response = _otpService.ConfirmOTP(OTPCode, OTPPurpose.CHANGEPASSWORD.ToString());
                if (!response.Item1)
                    return ApiResponseHelper.BuildResponse("The OTP does not match, please try again", true, false, StatusCodes.Status200OK);
                var OTP = _oTPRepository.GetOTPByCode(OTPCode);
                if (OTP == null)
                {
                    return ApiResponseHelper.BuildResponse("The OTP does not exist", true, false, StatusCodes.Status200OK);
                }
                var user = _userRepository.GetUserByEmail(OTP.UserEmail);
                if (user == null)
                    return ApiResponseHelper.BuildResponse("The  user does not exist", true, false, StatusCodes.Status200OK);
                if (newPassword != confirmPassword)
                    return ApiResponseHelper.BuildResponse("new password and confirm password do not match, please try again", true, false, StatusCodes.Status200OK);
                var passwordHash = SecurityHelper.Encrypt(newPassword);
                user.PasswordHash = passwordHash;
                var result = _userRepository.UpdateUser(user);
                if (result)
                {
                    return ApiResponseHelper.BuildResponse("Your password has changed successfully", true, true, StatusCodes.Status200OK);
                }
                return ApiResponseHelper.BuildResponse("Something went wrong while changing the password", true, false, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BaseApiResponse<DisplaySingleUserDto>> LoginAsync(UserLoginDto userLogin)
        {
           
            try
            {
                
                _logger.LogInformation("About to start executing",userLogin);
                var response = _userRepository.GetUserByEmail(userLogin.EmailAddress);
                _logger.LogInformation("checks if user was found or not");
                if (response == null)
                    return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("User was not found", true, null, StatusCodes.Status200OK);
                if (!response.IsActive)
                    return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("User is inactive", true, null, StatusCodes.Status200OK);
               _logger.LogInformation("About to check if user is two factor enabled");
                if (response.IsTwoFactorEnabled)
                {
                    var password = SecurityHelper.Encrypt(userLogin.Password);
                    if (response.PasswordHash == password)
                    {
                        var messageToBeSent = _otpService.GenerateOTP(OTPPurpose.TW0FA.ToString(), userLogin.EmailAddress);
                        var subject = "OTP FOR LOGIN";
                        _emailService.SendEmailWithGmailClient(subject, messageToBeSent, new List<string> { response.EmailAddress });
                        return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("We have sent an OTP to your email", true, null, StatusCodes.Status200OK);
                    }
                    return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("Incorrect login details", true, null, StatusCodes.Status200OK);
                }
                return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("User is not 2FA enabled", true, null, StatusCodes.Status200OK);
           }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("An error occurred while adding the user", false, null, StatusCodes.Status500InternalServerError);
            }
               
            /*else
            {
                var response1 = SecurityHelper.Decrypt(response.PasswordHash);
                if (response1 != userLogin.Password)
                {
                    return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("User was not found", true, null, StatusCodes.Status200OK);   //what to do here
                }
                else
                {
                    var userToReturn = new DisplaySingleUserDto
                    {
                        Id = response.Id,
                        UserName = response.UserName,
                        FirstName = response.FirstName,
                        LastName = response.LastName,
                        PhoneNumber = response.PhoneNumber,
                        EmailAddress = response.EmailAddress,
                        ImageUrl = response.ImageUrl,
                        CreatedOn = response.CreatedOn,
                        IsActive = response.IsActive,
                        IsTwoFactorEnabled = response.IsTwoFactorEnabled,
                    };
                    return ApiResponseHelper.BuildResponse("User with email address was not found", true, userToReturn, StatusCodes.Status200OK);
                }
            }*/

        }

        public async Task<BaseApiResponse<string>> LoginWith2FAAsync(string userEmail, string OTP)
        {
            var user = await _userService.GetSingleUserByEmailAsync(userEmail);
            if (user.Data != null)
            {
                var response = _otpService.ConfirmOTP(OTP,OTPPurpose.TW0FA.ToString());
                if (!response.Item1)
                {
                    return ApiResponseHelper.BuildResponse<string>("User account could not be authenticated", true, null, StatusCodes.Status200OK);
                }
                else
                {
                    var token = JWTTokenHelper.Generate(user.Data);
                    if (token != null)
                    {
                        return ApiResponseHelper.BuildResponse("User has been authenticated successfully", true, token, StatusCodes.Status200OK);
                    }
                    return ApiResponseHelper.BuildResponse<string>("Something went wrong while authenticating the user", true, null, StatusCodes.Status200OK);
                }
            }
            return ApiResponseHelper.BuildResponse<string>("User was not found", true, null, StatusCodes.Status200OK);
        }

        public async Task<BaseApiResponse<bool>> RequestChangePasswordOTP(string currentPassword, string userEmail)
        {
            var response = _userRepository.GetUserByEmail(userEmail);
            if (response == null)
                return ApiResponseHelper.BuildResponse("incorrect details", false, false, StatusCodes.Status200OK);
             var password = SecurityHelper.Encrypt(currentPassword);
                if (response.PasswordHash != password)
                  return ApiResponseHelper.BuildResponse("incorrect details", false, false, StatusCodes.Status200OK);
             var response1 = _otpService.GenerateOTP(OTPPurpose.CHANGEPASSWORD.ToString(),userEmail);
                 if (string.IsNullOrEmpty(response1))
                  return ApiResponseHelper.BuildResponse("Something went wrong while generating the OTP", false, false, StatusCodes.Status400BadRequest);
              var messageToBeSent = EmailTemplateHelper.CreateForgotPasswordTemplate(response.FirstName, response1);
                   _emailService.SendEmailWithGmailClient("Your OTP for Change Password", messageToBeSent, new List<string>() { userEmail });
                  return ApiResponseHelper.BuildResponse("The OTP has been generated successfully and sent to your email", true, true, StatusCodes.Status200OK);
        }

        public async Task<BaseApiResponse<bool>> RequestForgotPasswordOTP(string userEmail)
        {
           /* try
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5)
                    {
                        throw new Exception("This is our exception");
                    }
                    else
                        _logger.LogInformation("The value of i is {loopCountValue}", i);
                }


            }
            catch (Exception ex)
            {

                _logger.LogError(ex,ex.Message);
                return ApiResponseHelper.BuildResponse("Something went wrong while changing the password", true, false, StatusCodes.Status200OK);
            }*/
            try
            {
                var response = _userRepository.GetUserByEmail(userEmail);
                if (response == null)
                    return ApiResponseHelper.BuildResponse("The email does not exist", false, false, StatusCodes.Status200OK);
                var result = _otpService.GenerateOTP(OTPPurpose.FORGOTPASSWORD.ToString(), userEmail);
                if (string.IsNullOrEmpty(result))
                    return ApiResponseHelper.BuildResponse("Something went wrong while generating the OTP", false, false, StatusCodes.Status400BadRequest);
                var messageToBeSent = EmailTemplateHelper.CreateForgotPasswordTemplate(response.FirstName,result);
                _emailService.SendEmailWithGmailClient("Your OTP for Forgot Password", messageToBeSent, new List<string>() { userEmail });
                return ApiResponseHelper.BuildResponse("The OTP has been generated successfully and sent to your email", true, true, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.BuildResponse(ex.Message,false,false,StatusCodes.Status500InternalServerError);
            }         
        }

        public async Task<BaseApiResponse<bool>> ResetPassword(string OTPCode,string newPassword, string confirmPassword)
        {
            var response = _otpService.ConfirmOTP(OTPCode,OTPPurpose.FORGOTPASSWORD.ToString());
            if (!response.Item1)
                return ApiResponseHelper.BuildResponse("The OTP does not match, please try again", true, false,StatusCodes.Status200OK);
            var OTP = _oTPRepository.GetOTPByCode(OTPCode);
            if (OTP == null)
            {
                return ApiResponseHelper.BuildResponse("The OTP does not exist", true, false, StatusCodes.Status200OK);
            }
            var user = _userRepository.GetUserByEmail(OTP.UserEmail);
            if (user == null)
                return ApiResponseHelper.BuildResponse("The  user does not exist", true, false,StatusCodes.Status200OK);
            if(newPassword!=confirmPassword)
               return ApiResponseHelper.BuildResponse("new password and confirm password do not match, please try again", true, false, StatusCodes.Status200OK);
            var passwordHash = SecurityHelper.Encrypt(newPassword);
            user.PasswordHash = passwordHash;
            var result = _userRepository.UpdateUser(user);
            if (result)
            {
                return ApiResponseHelper.BuildResponse("Your password has changed successfully", true, true, StatusCodes.Status200OK);
            }
            return ApiResponseHelper.BuildResponse("Something went wrong while changing the password", true, false, StatusCodes.Status200OK);
        }
    }

}
