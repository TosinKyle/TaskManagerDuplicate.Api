
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class RoleService : IRoleService 
    {
        private readonly Data.Repositories.Interface.IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository) 
        {
         _roleRepository = roleRepository;
        }
        public RoleCreationResponseDto AddRole(RoleCreationDto roleToAdd)
        {
            var role = _roleRepository.GetRoleByRoleName(roleToAdd.RoleName);
            if (role!=null)
            {
                return new RoleCreationResponseDto { HasAdded = false, Message = "Role already exists" };
            }
            else 
            {
                Role roleToBeAdded = new Role
                {
                    RoleName = roleToAdd.RoleName,
                    RoleDescription = roleToAdd.RoleDescription,
                };
                var response = _roleRepository.AddRole(roleToBeAdded);
                if (response)
                    return new RoleCreationResponseDto { HasAdded = true, Message = "Role was successfully added", Id = roleToBeAdded.Id };
                else
                    return new RoleCreationResponseDto { HasAdded = false, Message = "Something went wrong while adding the role" };
            }
                
        }

        public DeleteResponseDto DeleteRole(string id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role!= null)
            {
                bool response = _roleRepository.DeleteRole(role);
                if (response)
                    return new DeleteResponseDto { HasDeleted = true, Message = "Role was deleted successfully" };
                return new DeleteResponseDto { HasDeleted = false, Message = "Something went wrong while deleting the role" };
            }
            return new DeleteResponseDto { HasDeleted = false, Message = "Role not found" };       
        }

        public List<RoleListDto> GetAllRoles()
        {
            var roleList = _roleRepository.GetAllRoles();
            List<RoleListDto> roleListDtos = new List<RoleListDto>();
            foreach (var role in roleList) 
            {
                roleListDtos.Add(new RoleListDto
                { 
                 RoleId = role.Id,
                  RoleDescription=role.RoleDescription,
                  CreatedOn= role.CreatedOn,
                }
                );                
            }
            return roleListDtos;
        }

        public DisplaySingleRoleDto GetRole(string id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null)
              return null;
            else
            {
                DisplaySingleRoleDto roleToDisplay = new DisplaySingleRoleDto
                {
                    RoleId = role.Id,
                    RoleName = role.RoleName,
                    RoleDescription = role.RoleDescription,
                };
                return roleToDisplay;
            }          
        }

        public UpdateResponseDto UpdateRole(UpdateRoleDto roleToUpdate, string roleId)
        {
            var response = _roleRepository.GetRoleById(roleId);
            if (response == null)
                return new UpdateResponseDto {HasUpdated=false, Message = "Role was not found"};
            Role role = new Role 
            {
              RoleName = roleToUpdate.RoleName,
              RoleDescription=roleToUpdate.RoleDescription,
            };
            var response1 = _roleRepository.UpdateRole(role);
            if (response1)
               return new UpdateResponseDto {HasUpdated=true, Message = "Role has been updated successfully" };
            return new UpdateResponseDto { HasUpdated = false, Message="Something went wrong while updating the role" };               
        }
    }
}
