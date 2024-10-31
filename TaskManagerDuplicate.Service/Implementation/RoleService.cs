
using Microsoft.AspNetCore.Http;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Domain.SharedModels;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.Interface;
using IUserService = TaskManagerDuplicate.Service.Interface.IUserService;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class RoleService : IRoleService 
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        public RoleService(IRoleRepository roleRepository,IUserService userService, IUserRepository userRepository) 
        {
         _roleRepository = roleRepository;
         _userService = userService;
         _userRepository = userRepository;
        }
        public async Task<BaseApiResponse<RoleCreationResponseDto>> AddRoleAsync(RoleCreationDto roleToAdd)
        {
            var role = _roleRepository.GetRoleByRoleName(roleToAdd.RoleName);
            if (role!=null)
            {
                return ApiResponseHelper.BuildResponse<RoleCreationResponseDto>("Role already exist",true,null,StatusCodes.Status400BadRequest);
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
                { var roleToReturn = new RoleCreationResponseDto();
                    roleToReturn.Id=roleToBeAdded.Id;
                    return ApiResponseHelper.BuildResponse<RoleCreationResponseDto>("Role was successfully added", true,roleToReturn, StatusCodes.Status201Created);
                }
                else
                    return  ApiResponseHelper.BuildResponse<RoleCreationResponseDto>("Something went wrong while adding the role", true, null, StatusCodes.Status500InternalServerError);
            }
                
        }

        public async Task<BaseApiResponse<DeleteResponseDto>> DeleteRoleAsync(string id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role!= null)
            {
                bool response = _roleRepository.DeleteRole(role);
                if (response)
                    return ApiResponseHelper.BuildResponse<DeleteResponseDto>("Role was deleted successfully", true, null, StatusCodes.Status200OK);
                return ApiResponseHelper.BuildResponse<DeleteResponseDto>("Something went wrong wile deleting the role", false, null, StatusCodes.Status500InternalServerError);
            }
            return ApiResponseHelper.BuildResponse<DeleteResponseDto>("Role was not found", true, null, StatusCodes.Status404NotFound);
        }

        public async Task<BaseApiResponse<PaginatedList<RoleListDto>>> GetAllRolesAsync(int page, int perPage)
        {
            var roleList = _roleRepository.GetAllRoles();
            List<RoleListDto> newRoleList = new List<RoleListDto>();
            foreach (var role in roleList) 
            {
                newRoleList.Add(new RoleListDto
                    { 
                     RoleId = role.Id,
                     RoleDescription=role.RoleDescription,
                     CreatedOn= role.CreatedOn,
                    }
                );                
            }
            var paginatedDate= PaginationHelper<RoleListDto>.Paginate(newRoleList, perPage, page);
            return ApiResponseHelper.BuildResponse<PaginatedList<RoleListDto>>("Kindly find below role list",true,paginatedDate,StatusCodes.Status200OK);
        }

        public async Task<BaseApiResponse<DisplaySingleRoleDto>> GetRoleAsync(string id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null)
              return ApiResponseHelper.BuildResponse<DisplaySingleRoleDto>("Role could not be found", true, null, StatusCodes.Status200OK);
            else
            {
                DisplaySingleRoleDto roleToDisplay = new DisplaySingleRoleDto
                {
                    RoleId = role.Id,
                    RoleName = role.RoleName,
                    RoleDescription = role.RoleDescription,
                };
                return ApiResponseHelper.BuildResponse<DisplaySingleRoleDto>("Kindly find the role", true, roleToDisplay, StatusCodes.Status200OK);
            }          
        }

        public async Task<BaseApiResponse<UpdateResponseDto>> UpdateRoleAsync(UpdateRoleDto roleToUpdate, string roleId)
        {
            var role = _roleRepository.GetRoleById(roleId);
            if (role == null)
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Role was not found", true, null, StatusCodes.Status200OK);

            role.RoleName = roleToUpdate.RoleName;
              role.RoleDescription = roleToUpdate.RoleDescription;

            var response1 = _roleRepository.UpdateRole(role);
            if (response1)
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Role has been updated successfully", true, null, StatusCodes.Status200OK);
            return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Something went wrong while updating the role", true, null, StatusCodes.Status500InternalServerError);
        }
        public async Task<BaseApiResponse<UpdateResponseDto>> AddRoleToUserAsync(string roleId, string userId)
        {
            var role = _roleRepository.GetRoleById(roleId);
            if (role != null)
            {
                var user = _userRepository.GetUserById(userId);
                if (user != null)
                {
                    if (user.RoleId!= roleId)
                    {
                        user.RoleId = roleId;
                        bool response= _userRepository.UpdateUser(user);
                        if(response)
                        {
                            return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Role has been added to user successfully", true, null, StatusCodes.Status200OK);
                        }
                        return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Role could not be added to the user", false, null, StatusCodes.Status400BadRequest);
                    }
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User already holds the role", true, null, StatusCodes.Status200OK);
                }
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User does not exist", true, null, StatusCodes.Status200OK);
            }
            return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Role does not exist", true, null, StatusCodes.Status200OK);
        }
    }
}

