using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class OTPService : IOTPService
    {
        private readonly string OTPexpiryTime = ConfigurationHelper.GetConfiguration()["SystemSettings:OTPExpiryTime"];
        private readonly IOTPRepository _OTPRepository;
        public OTPService(IOTPRepository OTPRepository) 
        { 
          _OTPRepository = OTPRepository;
        }
        public Tuple<bool,string> ConfirmOTP(string OTP, string purpose)
        {
            var response = _OTPRepository.GetOTPByCode(OTP);
            if (response!= null)
            {
                if (response.Purpose == purpose)
                {
                    if (DateTime.Now > response.ExpiryTime)
                        return Tuple.Create(false, "The OTP has been expired");
                    return Tuple.Create(true, " The OTP has been confirmed");
                }
                return Tuple.Create(false, " The Purpose is wrong");
            }
            return Tuple.Create(false, " The OTP does not exist");
        }
        public string GenerateOTP(string purpose, string userEmail)
        {
           
            Random Number = new Random();
            var randomNumber= Number.Next(100000,1000000);
            OTP OTPToAdd = new OTP
            {
                OTPCode = randomNumber.ToString(),
                Purpose = purpose,
                UserEmail = userEmail,
                ExpiryTime= DateTime.Now.AddMinutes(int.Parse(OTPexpiryTime)),            
            };
            var response = _OTPRepository.AddOTP(OTPToAdd);
            if (response)
                return OTPToAdd.OTPCode;
                return string.Empty;
        }
    }
}
