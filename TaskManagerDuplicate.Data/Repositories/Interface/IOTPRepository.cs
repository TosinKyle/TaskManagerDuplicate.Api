using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Repositories.Interface
{
    public interface IOTPRepository
    {
        public bool AddOTP(OTP OTP);
        public bool UpdateOTP(OTP OTP);
        public OTP GetOTPByCode(string OTPCode);
    }
}
