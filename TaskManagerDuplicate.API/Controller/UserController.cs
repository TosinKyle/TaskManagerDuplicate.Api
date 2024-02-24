using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; //why faint
        public UserController(IUserService userService)
        {
           _userService = userService;
        }
        [HttpGet("get-user-by-id/({id})")]
        public IActionResult GetUserById([FromRoute] string id)
        {
                if (string.IsNullOrEmpty(id))
                return BadRequest("id cannot be null or empty");   
                DisplaySingleUserDto singleUser =_userService.GetSingleUserById(id);
                if (singleUser != null) 
                    return Ok(singleUser);
                return NotFound("User not found");
        }
        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers() 
        {
         List<UserListDto> userList =_userService.GetAllUsers();
            if (userList.Count < 1)
                return NotFound("user list is empty");
              return Ok(userList);
        }
        [HttpPost("add-new-user")]
        public IActionResult AddUser([FromBody]UserCreationDto userToAdd)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Some properties are missing");     
            }
            else
            {
                var response = _userService.AddUser(userToAdd);
                if (response != null)
                {
                    return Ok($"User has been added successfully: {response}");
                }
                return null;
            }
        }
        [HttpPut("update-user/{id}")]
        public IActionResult UpdateUser([FromRoute]string userId,[FromBody]UpdateUserDto userToUpdate) 
        {
            if (!ModelState.IsValid)
                return BadRequest("Some properties are missing");
            if (string.IsNullOrEmpty(userId))
               return BadRequest("id cannot be empty"); 
                var response = _userService.UpdateUser(userToUpdate,userId);
           if (response.HasUpdated)
               return Ok(response.Message); 
               return Ok(response.Message);      
        }
        [HttpDelete("delete-user/{userId}")]
        public IActionResult DeleteUser([FromRoute]string userId) 
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("id cannot be empty");
            }
            var response = _userService.DeleteUser(userId);
            if (response.HasDeleted)
                return Ok(response.Message);
                return NotFound(response.Message);
        }
    }
}
