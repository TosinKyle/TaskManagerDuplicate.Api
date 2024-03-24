using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UserCreationResponseDto
    {
        public string Id { get; set; }
        public bool HasAdded { get; set; }
        public string Message { get; set; }
    }
}
