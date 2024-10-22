using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class DisplayUserFirstLastNameDto
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string FullName { get; set; }
    }
}
