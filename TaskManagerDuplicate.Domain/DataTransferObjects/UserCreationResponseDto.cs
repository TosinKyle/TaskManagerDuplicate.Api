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
        public string FilePath { get; set; }
        public string FileName { get; set; }

    }
}
