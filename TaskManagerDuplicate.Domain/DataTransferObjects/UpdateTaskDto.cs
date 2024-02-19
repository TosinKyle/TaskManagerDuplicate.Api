using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UpdateTaskDto
    {
        public string Name { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
    }
}
