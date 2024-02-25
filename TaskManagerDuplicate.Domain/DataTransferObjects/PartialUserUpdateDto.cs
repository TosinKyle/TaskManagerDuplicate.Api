using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class PartialUserUpdateDto
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
