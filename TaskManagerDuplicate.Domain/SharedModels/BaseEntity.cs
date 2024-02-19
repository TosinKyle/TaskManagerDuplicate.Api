using System.ComponentModel.DataAnnotations;


namespace TaskManagerDuplicate.Domain.SharedModels
{
    public class BaseEntity
    {
        [Required(ErrorMessage ="This is a compulsory field")]
        public string Id { get; set; } =Guid.NewGuid().ToString();
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedOn { get; set; }
        [Required]
        public bool IsDeleted { get; set; } 
    }
}
