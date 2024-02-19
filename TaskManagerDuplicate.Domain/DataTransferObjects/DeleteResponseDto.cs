using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class DeleteResponseDto
    {
        public bool HasDeleted { get; set; } 
        public string Message { get; set; } 
    }
}
