

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UpdateTaskDto
    {
        public string Name { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
    }
}
