using System.ComponentModel.DataAnnotations;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Domain.DbModels
{
    public class ToDoTask : BaseEntity
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRecurring { get; set; }
        public string  UserId{get;set;}
        public User User { get;set;}
    }
}
