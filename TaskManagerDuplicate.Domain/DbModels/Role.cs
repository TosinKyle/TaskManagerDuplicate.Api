﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Domain.DbModels
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}