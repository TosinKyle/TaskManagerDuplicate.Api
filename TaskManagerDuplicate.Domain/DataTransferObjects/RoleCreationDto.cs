using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class RoleCreationDto
    {
        [Required]//does data annotation work for Dtos or only models //how can i make check for empty input
        public string RoleName { get; set; }
        [Required]
        public string RoleDescription { get; set; }
    }
}
