using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class PartialUserUpdateDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
