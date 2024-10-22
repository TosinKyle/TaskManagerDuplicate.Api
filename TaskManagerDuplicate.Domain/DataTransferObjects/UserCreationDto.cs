using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UserCreationDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? EmailAddress { get; set; }
        [Required]
        [Compare(nameof(ConfirmPassword))]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string? Password { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required]
        public IFormFile ProfilePicture { get; set; }
    }
}

