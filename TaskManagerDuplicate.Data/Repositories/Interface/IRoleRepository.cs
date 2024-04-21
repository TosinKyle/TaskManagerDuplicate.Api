using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Repositories.Interface
{
    public interface IRoleRepository
    {
        public bool AddRole(Role roleToAdd);
       // public bool UpdateRole(Role roleToUpdate);
        public bool DeleteRole(Role roleToRemove);
        public Role GetRoleById(string roleId);
        public IQueryable<Role> GetAllRoles();
        public bool UpdateRole(Role roleToUpdate);

    }
}
