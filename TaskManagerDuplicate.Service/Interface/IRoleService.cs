
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IRoleService
    {
        public Task<BaseApiResponse<RoleCreationResponseDto>> AddRoleAsync(RoleCreationDto roleToAdd);
        public Task<BaseApiResponse<DeleteResponseDto>>  DeleteRoleAsync(string id);  
        public Task<BaseApiResponse<DisplaySingleRoleDto>>  GetRoleAsync(string id);
        public Task<BaseApiResponse<PaginatedList<RoleListDto>>>  GetAllRolesAsync(int page,int perPage);
        public Task<BaseApiResponse<UpdateResponseDto>>  UpdateRoleAsync(UpdateRoleDto roleToUpdate, string roleId);
        public Task<BaseApiResponse<UpdateResponseDto>> AddRoleToUserAsync(string roleId,string userId);
    }
}
