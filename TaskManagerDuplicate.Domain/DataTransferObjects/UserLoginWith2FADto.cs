using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UserLoginWith2FADto
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string OTP{ get; set; }
    }
}
