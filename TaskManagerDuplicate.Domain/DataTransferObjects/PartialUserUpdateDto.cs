
using System.ComponentModel.DataAnnotations;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class PartialUserUpdateDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
