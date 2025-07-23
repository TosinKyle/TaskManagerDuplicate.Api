using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Data.Context;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Repositories.Implementation
{
    public class OTPRepository : IOTPRepository 
    {
        private readonly EntityFrameworkContext _entityFrameworkContext;

        public OTPRepository(EntityFrameworkContext entityFrameworkContext)
        {
          _entityFrameworkContext = entityFrameworkContext;
        }
        public bool AddOTP(OTP OTP)
        {
           //var response =
            _entityFrameworkContext.OTP.Add(OTP);  //This code adds the OTP entity to the OtpRequest DbSet and then saves the changes to the database using SaveChanges().
            return _entityFrameworkContext.SaveChanges() > 0;
        }

        public OTP GetOTPByCode(string OTPCode)
        {
            return _entityFrameworkContext.OTP.FirstOrDefault(x=>x.OTPCode==OTPCode);
        }

        public bool UpdateOTP(OTP OTP)
        {
            _entityFrameworkContext.OTP.Update(OTP);
            return _entityFrameworkContext.SaveChanges() > 0;
        }
    }
}
