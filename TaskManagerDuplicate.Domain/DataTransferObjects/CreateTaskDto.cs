

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class CreateTaskDto
    {
        public string TaskName { get; set; }
        public string Description { get; set; }
      //  public DateTime DueDate { get; set; }
        public string UserId { get; set; }
        //public string CompletionDate { get; set; }
    }
}
