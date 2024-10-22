using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.SharedModels;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// This endpoint is responsible for getting all users from the database
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] int page, [FromQuery] int perPage) => BuildHttpResponse(await _userService.GetAllUsersAsync(page, perPage));
        /// <summary>
        /// This endpoint is responsible for activating a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [AllowAnonymous]
        [HttpPost("activate-the-user/{id}")]
        public async Task<IActionResult> ActivateUser([FromRoute] string id)=>BuildHttpResponse(await _userService.ActivateUserAsync(id));
        /// <summary>
        /// This endpoint is responsible for de-activating a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpPost("de-activate-user/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeactivateUser([FromRoute] string id) => BuildHttpResponse(await _userService.DeactivateUserAsync(id));

        /// <summary>
        /// This endpoint is responsible for getting a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet("get-user-by-id/{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetUserById([FromRoute] string id) => BuildHttpResponse(await _userService.GetSingleUserByIdAsync(id));

        /// <summary>
        /// This endpoint is responsible for adding a user
        /// </summary>
        /// <param name="userToAdd"></param>
        /// <returns></returns>
        
        [HttpPost("add-new-user")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserAsync([FromForm] UserCreationDto userToAdd) => BuildHttpResponse(await _userService.AddUserAsync(userToAdd));

        /// <summary>
        /// This endpoint is responsible for updating a user
        /// </summary>
        /// <param name="userId"></param>
        /// /// <param name="userToUpdate"></param>
        /// <returns></returns>

        [HttpPut("update-user/{userId}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] UpdateUserDto userToUpdate)=>
           BuildHttpResponse(await _userService.UpdateUserAsync(userToUpdate, userId));
        /// <summary>
        /// This endpoint is responsible for deleting a user
        /// </summary>
        ///  <param name="userId"></param>
        /// <returns></returns>
        
        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] string userId)=> BuildHttpResponse(await _userService.DeleteUserAsync(userId));
        /// <summary>
        /// This endpoint is responsible for partially updating a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userToUpdate"></param>
        /// <returns></returns>
        
        [HttpPatch("partial-user-update/{userId}")]
        public async Task<IActionResult> UpdateUserPartiallyAsync(string userId, PartialUserUpdateDto userToUpdate)=>BuildHttpResponse(await _userService.UpdateUserPartiallyAsync(userId, userToUpdate));
        /// <summary>
        /// This endpoint is responsible for removing role from a user
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        [AllowAnonymous]
        [HttpPatch("remove-role-from-a-user/{userId}/{roleId}")]
        public async Task<IActionResult> RemoveRoleFromUserAsync(string userId, string roleId) => BuildHttpResponse(await _userService.RemoveRoleFromUserAsync(userId, roleId)); //must i put asyn cin controller
        /// <summary>
        /// This endpoint is responsible for  changing the user role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleToAddId"></param>
        /// <returns></returns>
        
        [AllowAnonymous]
        [HttpPatch("change-user-role/{userId}/{roleToAddId}")]
        public async Task<IActionResult> ChangeUserRoleAsync(string userId, string roleToAddId) => BuildHttpResponse(await _userService.ChangeUserRoleAsync(userId, roleToAddId));
        /// <summary>
        /// This endpoint is responsible for getting user's first and last name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        [AllowAnonymous]
        [HttpGet("get-user-first-and-last-name/{userId}")]
        public async Task<IActionResult> GetUserFirstAndLastNameAsync([FromRoute] string userId) => BuildHttpResponse(await _userService.GetUserFirstAndLastNameAsync(userId));
        /// <summary>
        /// This endpoint is responsible for sending out bulk email to users
        /// </summary>
        /// <param name="userEmails"></param>
        /// <returns></returns>
        
        [AllowAnonymous]
        [HttpPost("send-bulk-email-to-users")]
        public async Task<IActionResult> SendBulkEmailToUsersAsync([FromBody] List<string> userEmails) => BuildHttpResponse(await _userService.SendBulkEmailToUsersAsync(userEmails));  //from where

    }
 }

