using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Helper
{
    internal class Keeps
    {
        /*         private DisplaySingleUserDto GetCurrentUser() 
         {
             var identity = HttpContext.User.Identity as ClaimsIdentity;
             if (identity != null)
             {
                 var userClaims = identity.Claims;
                 return new DisplaySingleUserDto
                 {
                     UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                     UserEmail = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                     FirstName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                     LastName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                 };
             }
             return null;
         }*/

        /*            if (response == null)                            //for controller
                    {
                        return BadRequest("");
                    }
                    else
                    {
                        var response1 = await _userService.SendBulkEmailToUsers(response);
                        if (!response1.IsSent)
                        {
                            return BadRequest(response1.Message);
                        }
                        else
                        {
                            return Ok(response1.Message);
                        }*/
        //}
               /*  if (string.IsNullOrEmpty(userId) || (string.IsNullOrEmpty(roleId)))   // for controller
          {
              return BadRequest("user id or role id cannot be empty");
          }
          else
          {
              var response = _userService.AddRoleToUser(userId, roleId);
              if (response.HasUpdated)
                  return Ok(response.Message);
              return BadRequest(response.Message);
          }*/

                   /* if (!ModelState.IsValid)                        // for controller
                 return BadRequest("Some properties are missing");
             var response = await _userService.AddUserAsync(userToAdd);
             if (!response.HasAdded)
             {
                 return BadRequest(response.Message);
             }
             else
                 return Ok(new { message=response.Message,id=response.Id, filepath = response.FilePath, filename=response.FileName });*/
                 
        
        /* if (string.IsNullOrEmpty(id))                           // for controller
                 return BadRequest("Id cannot be null or empty");
             DisplaySingleUserDto singleUser = _userService.GetSingleUserById(id);
             if (singleUser != null)
                 return Ok(singleUser);
             return NotFound("User not found");*/
    }
}
