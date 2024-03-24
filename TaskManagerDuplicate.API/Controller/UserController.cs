using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

       [HttpGet("get-user-by-id/{id}")]
        public IActionResult GetUserById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id cannot be null or empty");
            DisplaySingleUserDto singleUser = _userService.GetSingleUserById(id);
            if (singleUser != null)
                return Ok(singleUser);
            return NotFound("User not found");
        }
        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            List<UserListDto> userList = _userService.GetAllUsers();
            if (userList.Count < 1)
                return NotFound("User list is empty");
            return Ok(userList);
        }
        [HttpPost("add-new-user")]
        public IActionResult AddUser([FromBody] UserCreationDto userToAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest("Some properties are missing");           
                var response = _userService.AddUser(userToAdd);
                if (!response.HasAdded)
                {
                    return BadRequest(response.Message);
                }
                else
                return Ok($"{response.Message}: {response.Id}");
        }
        [HttpPut("update-user/{userId}")]
        public IActionResult UpdateUser([FromRoute] string userId, [FromBody] UpdateUserDto userToUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest("Some properties are missing");
            if (string.IsNullOrEmpty(userId))
                return BadRequest("Id cannot be empty");
            var response = _userService.UpdateUser(userToUpdate, userId);
            if (response.HasUpdated)
                return Ok(response.Message);
            return Ok(response.Message);   //this or
        }
        [HttpDelete("delete-user/{userId}")]
        public IActionResult DeleteUser([FromRoute] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Id cannot be empty");
            }
            var response = _userService.DeleteUser(userId);
            if (response.HasDeleted)
                return Ok(response.Message);
            return NotFound(response.Message);
        }
        [HttpPatch("partial-user-update/{userId}")]
        public IActionResult UpdateUserPartially(string userId, PartialUserUpdateDto userToUpdate)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User id cannot be empty");
            if (!ModelState.IsValid) 
                return BadRequest("Some properties are missing");
            var response = _userService.UpdateUserPartially(userId,userToUpdate);
            if (response.HasUpdated)
                return Ok(response.Message);
            return BadRequest(response.Message);  //this
        }
    }
}
