using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserLoginDto userLogin) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Some properties are missing");
            }
            else 
            {
                var response = _userService.Login(userLogin);
                if (response.IsMatched)
                    return Ok(response.Message);
                return BadRequest(response.Message);
            }
        }
    }
}
