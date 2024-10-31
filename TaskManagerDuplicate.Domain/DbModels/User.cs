using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Domain.DbModels
{
    public class User : BaseEntity
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }
        List<ToDoTask> Tasks { get; set; }
        [ForeignKey("RoleId")]
        public string? RoleId { get; set; }
        public Role? Role { get; set; }
        public string? FileName { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public string SharedSecret { get; set; }
    }
}
