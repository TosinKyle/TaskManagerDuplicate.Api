using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Domain.DbModels
{
     public class OTP : BaseEntity
     {
        [Required]
        [DataType(DataType.Text)]
        public string OTPCode { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string  Purpose { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpiryTime { get; set; }
        [DataType(DataType.Text)]
        public bool Expired { get; set; }
     }
}
