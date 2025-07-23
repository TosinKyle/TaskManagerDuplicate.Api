using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IAuthenticationService
     {
        public Task<BaseApiResponse<DisplaySingleUserDto>> LoginAsync(UserLoginDto userLogin);
        public Task<BaseApiResponse<string>> LoginWith2FAAsync(string userEmail, string OTP);
        public Task<BaseApiResponse<bool>> RequestForgotPasswordOTP(string userEmail);
        public Task<BaseApiResponse<bool>> ResetPassword(string OTPCode, string newPassword,string confirmPassword);
        public Task<BaseApiResponse<bool>> RequestChangePasswordOTP(string currentPassword, string userEmail);
        public Task<BaseApiResponse<bool>> ChangePassword(string OTPCode, string newPassword, string confirmPassword);

     }
}
