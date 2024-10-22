using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Data.Context;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository 
    {
        private readonly EntityFrameworkContext _entityFrameworkContext;
        public RoleRepository(EntityFrameworkContext entityFrameworkContext) 
        {
         _entityFrameworkContext = entityFrameworkContext;
        }
        public bool AddRole(Role roleToAdd)
        {
           _entityFrameworkContext.Role.Add(roleToAdd);
            return _entityFrameworkContext.SaveChanges()>0;
        }

        public bool DeleteRole(Role roleToRemove)
        {
            _entityFrameworkContext.Role.Remove(roleToRemove);
            return _entityFrameworkContext.SaveChanges()>0; 
        }

        public IQueryable<Role> GetAllRoles() => _entityFrameworkContext.Role;
        public Role GetRoleById(string roleId)=>_entityFrameworkContext.Role.FirstOrDefault(x => x.Id == roleId);

        public Role GetRoleByRoleName(string roleName)=>_entityFrameworkContext.Role.FirstOrDefault(x => x.RoleName == roleName);

        public bool UpdateRole(Role roleToUpdate)
        {
            _entityFrameworkContext.Role.Update(roleToUpdate);
           return _entityFrameworkContext.SaveChanges()>0;  
        }
    }
}
