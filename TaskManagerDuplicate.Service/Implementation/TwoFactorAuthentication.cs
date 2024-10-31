using OtpNet;
using TaskManagerDuplicate.Service.Interface;
using TaskManagerDuplicate.Data.Repositories.Interface;
using Microsoft.AspNetCore.DataProtection;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class TwoFactorAuthentication : ITwoFactorAuthentication
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly Totp _totp;

        public TwoFactorAuthentication(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
            string secretKey = "JBSWY3DPEHPK3PXP";
            byte[] bytes = Base32Encoding.ToBytes(secretKey);
            _totp = new Totp(bytes, mode: OtpHashMode.Sha512, step: 300, totpSize: 6);
        }
        public string GenerateOTP()
        {
            string secretKey = "JBSWY3DPEHPK3PXP";
            byte[] bytes = Base32Encoding.ToBytes(secretKey);
            var newOTP = new Totp(bytes, mode: OtpHashMode.Sha512, step: 300, totpSize: 6);
            var result = newOTP.ComputeTotp(DateTime.UtcNow);
            var response = result.ToString();
            return $"Your OTP is : {response}";
        }
        public bool VerifyOTP(string OTP, out long timeWindowUsed, VerificationWindow window = null)
        { 
            bool valid = _totp.VerifyTotp(OTP,out timeWindowUsed,window);
            if (valid)
            {
                return true;
            }
            return false;
        }

    }
}


