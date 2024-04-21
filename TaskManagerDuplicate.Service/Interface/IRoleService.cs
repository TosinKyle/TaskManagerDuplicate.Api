
using TaskManagerDuplicate.Domain.DataTransferObjects;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IRoleService
    {
        public RoleCreationResponseDto AddRole(RoleCreationDto roleToAdd);
        public DeleteResponseDto DeleteRole(string id);  
        public DisplaySingleRoleDto GetRole(string id);
        List<RoleListDto> GetAllRoles();
        public UpdateResponseDto UpdateRole(UpdateRoleDto roleToUpdate, string roleId);
    }
}
