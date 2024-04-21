

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class TaskListDto
    {
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
