using System.ComponentModel.DataAnnotations;


namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UpdateUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
