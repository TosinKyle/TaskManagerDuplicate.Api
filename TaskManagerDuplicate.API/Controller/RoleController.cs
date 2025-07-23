using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
         _roleService = roleService;
        }

        /// <summary>
        /// This endpoint is responsible for adding a new role to the database
        /// </summary>
        /// <param name="roleToBeAdded"></param>
        /// <returns></returns>
        
        [HttpPost("role-creation")]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleCreationDto roleToBeAdded) 
        {
            var response = await _roleService.AddRoleAsync(roleToBeAdded);
            return BuildHttpResponse(response);
            /*if (!ModelState.IsValid)
                return BadRequest("Some properties are missing");
                var response = _roleService.AddRole(roleToBeAdded);
                if (response.HasAdded)
                    return Ok($"{response.Message}:{response.Id}");
                return BadRequest(response.Message);*/

        }
        /// <summary>
        /// This endpoint is responsible for deleting a role in the database
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] string roleId) => BuildHttpResponse(await _roleService.DeleteRoleAsync(roleId));
        /// <summary>
        /// This endpoint is responsible for getting a role by inputting the role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        
        [HttpGet("get-role-by-id/{roleId}")]
        public async Task<IActionResult> GetRoleById([FromRoute]string roleId)
        {
            var response = await _roleService.GetRoleAsync(roleId);
            return BuildHttpResponse(response);
        }
        /// <summary>
        /// This endpoint is responsible for getting all roles from the database
        /// </summary>
        /// <param name="page"></param>
        /// /// <param name="perPage"></param>
        /// <returns></returns>
        
        [HttpGet("role-list")]
        public async Task<IActionResult> GetAllRoles([FromQuery]int page,[FromQuery] int perPage) 
        {
           var roleList = await _roleService.GetAllRolesAsync(page, perPage);// normally can i do PaginatedList<roleList>
           return BuildHttpResponse(roleList);
        }
        /// <summary>
        /// This endpoint is responsible for updating a role
        /// </summary>
        /// <param name="roleToUpdate"></param>
        /// /// <param name="roleId"></param>
        /// <returns></returns>
        
        [HttpPatch("role-update/{roleId}")]
        public async Task<IActionResult> UpdateRole([FromBody]UpdateRoleDto roleToUpdate, [FromRoute]string roleId) 
        {
            var response= await _roleService.UpdateRoleAsync(roleToUpdate, roleId);
            return BuildHttpResponse(response);
        }
        /// <summary>
        /// This endpoint is responsible for adding role to a user
        /// </summary>
        /// <param name="roleId"></param>
        /// /// <param name="userId"></param>
        /// <returns></returns>
        
    }
}

