using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Service.Interface
{
     public interface ITwoFactorAuthentication
     {
        public string GenerateOTP();
        public bool VerifyOTP(string OTP,out long timeWindowUsed, VerificationWindow window = null);
     }
}
