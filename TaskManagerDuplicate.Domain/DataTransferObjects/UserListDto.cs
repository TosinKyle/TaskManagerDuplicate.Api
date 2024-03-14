

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UserListDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
