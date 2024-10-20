

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class DisplaySingleUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public List<TaskListDto> UserTasks = new List<TaskListDto>(); 
    }
}
