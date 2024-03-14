using System.ComponentModel.DataAnnotations;


namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UserLoginDto
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
