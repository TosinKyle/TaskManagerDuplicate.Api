using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Service.Interface
{
     public interface IOTPService
     {

        public string GenerateOTP(string purpose, string userEmail);
        public Tuple<bool,string> ConfirmOTP(string OTP,string purpose);
     }
}
