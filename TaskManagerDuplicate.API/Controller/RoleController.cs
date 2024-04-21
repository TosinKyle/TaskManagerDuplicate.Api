using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
         _roleService = roleService;
        }

        /*/// <summary>
        /// This endpoint is responsible for adding a new role to the database
        /// </summary>
        /// <param name="roleToBeAdded"></param>
        /// <response></response>
        /// <response></response>
        /// <response></response>
        /// <returns></returns>*/
        
        [HttpPost("add-new-role")]
        public IActionResult AddRole([FromBody] RoleCreationDto roleToBeAdded) 
        {
            if (!ModelState.IsValid)
                return BadRequest("Some properties are missing");
                var response = _roleService.AddRole(roleToBeAdded);
                if (response.HasAdded)
                    return Ok($"{response.Message}:{response.Id}");
                return BadRequest(response.Message);
        }
        [HttpDelete("delete-role/{roleId}")]
        public IActionResult DeleteRole([FromRoute]string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
                return BadRequest("Role id cannot be empty");
            var response = _roleService.DeleteRole(roleId);
                if (response.HasDeleted)
                    return Ok(response.Message);
                return BadRequest(response.Message);
        }
        [HttpGet("get-role-by-id/{roleId}")]
        public IActionResult GetRoleById([FromRoute]string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
                return BadRequest("Role id cannot be null");
            var response=_roleService.GetRole(roleId);
            if (response!=null)
                return Ok(response);
            return BadRequest("Role was not found");
        }
        [HttpGet("get-all-roles")]
        public IActionResult GetAllRoles() 
        {
         List<RoleListDto> roleList = _roleService.GetAllRoles();
            if (roleList.Count<1)
             return BadRequest("No role was found");
            return Ok(roleList);
        }
        [HttpPatch("update-role")]
        public IActionResult UpdateRole(UpdateRoleDto roleToUpdate, string roleId) 
        {
                if (string.IsNullOrEmpty(roleId))
                    return BadRequest("id cannot be empty");
            if (!ModelState.IsValid)
                return BadRequest("Some properties are missing");
                var response = _roleService.UpdateRole(roleToUpdate,roleId);
                if (response.HasUpdated)
                    return Ok(response.Message); 
                return Ok(response.Message);
        }
    }
}

