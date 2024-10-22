using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class RoleListDto
    {
        public string RoleId { get; set; }
        public string RoleDescription { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
