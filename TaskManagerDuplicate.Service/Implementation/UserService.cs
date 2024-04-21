
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.Interface;


namespace TaskManagerDuplicate.Service.Implementation
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
         _userRepository = userRepository;
        }
        public UserCreationResponseDto AddUser(UserCreationDto userToAdd)
        {
            var userEmail = _userRepository.GetUserByEmail(userToAdd.EmailAddress);
            if (userEmail != null)
                return new UserCreationResponseDto { HasAdded = false, Message = "User already exists" };
                var passwordHash = SecurityHelper.Encrypt(userToAdd.Password);
                User userToBeAdded = new User
                {
                    FirstName = userToAdd.FirstName,
                    LastName = userToAdd.LastName,
                    UserName = userToAdd.UserName,
                    EmailAddress = userToAdd.EmailAddress,
                    PhoneNumber = userToAdd.PhoneNumber,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordHash,
                    ImageUrl = userToAdd.ProfilePicture,
                };
                var response = _userRepository.AddUser(userToBeAdded);
            if (response)
                return new UserCreationResponseDto { HasAdded = true, Message="User was successfully added", Id=userToBeAdded.Id };
            else
                return new UserCreationResponseDto { HasAdded = false, Message = "Something went wrong while adding the user" };
        }

        public DeleteResponseDto DeleteUser(string userId)
        {
            var user= _userRepository.GetUserById(userId);
            if (user != null)
            {
                bool response = _userRepository.DeleteUser(user);
                if (response)
                {
                    return new DeleteResponseDto { HasDeleted = true, Message = "User was deleted successfully" };
                }   //return object of this dto
                else
                {
                    return new DeleteResponseDto { HasDeleted = false, Message = "Something went wrong while deleting user" };
                }
            }
                DeleteResponseDto response2 = new DeleteResponseDto();
                response2.HasDeleted = false;
                response2.Message = "User was not found";
                return response2;
        }

        public List<UserListDto> GetAllUsers()
        {
            var userList =_userRepository.GetAllUsers();
            List<UserListDto> userToReturn = new List<UserListDto>();
            foreach (var user in userList)
            {
                userToReturn.Add(new UserListDto
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    UserEmail = user.EmailAddress,
                    CreatedOn = user.CreatedOn,
                });
            }
            return userToReturn;
        }

        public DisplaySingleUserDto GetSingleUserByEmail(string userEmail)
        {
            var user = _userRepository.GetUserByEmail(userEmail);
            if (user == null)
                return null;
            else
            {
                DisplaySingleUserDto singleUser = new DisplaySingleUserDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserEmail = user.EmailAddress,
                    ProfilePicture = user.ImageUrl,
                    CreatedOn = user.CreatedOn
                };
                return singleUser;
            }
        }

        public DisplaySingleUserDto GetSingleUserById(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return null;
            else
            {
                DisplaySingleUserDto singleUser = new DisplaySingleUserDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserEmail = user.EmailAddress,
                    ProfilePicture = user.ImageUrl,
                    CreatedOn = user.CreatedOn,
                    IsActive= user.IsActive
                };
                return singleUser;
            }
        }

        public DisplaySingleUserDto Login(UserLoginDto userLogin)
        {
            var response = _userRepository.GetUserByEmail(userLogin.EmailAddress);
            if (response == null)
            {
                return null;
            }
            else
            {
                var response1 = SecurityHelper.Decrypt(response.PasswordHash);
                if (response1 != userLogin.Password)
                {
                    return null;
                }
                else
                {
                    return new DisplaySingleUserDto
                    {
                        UserId = response.Id,
                        UserName = response.UserName,
                        FirstName = response.FirstName,
                        LastName = response.LastName,
                        PhoneNumber = response.PhoneNumber,
                        UserEmail = response.EmailAddress,
                        ProfilePicture = response.ImageUrl,
                        CreatedOn = response.CreatedOn,
                        IsActive= response.IsActive
                    };
                }
            }
            /* var response = _userRepository.GetUserByEmail(userLogin.EmailAddress);
             if (response == null)
             {
                return new LoginResponseDto
                {
                    IsMatched = false,
                    Message = "Login details incorrect",
                };
             }
             else 
             {
                var response1 = SecurityHelper.Decrypt(response.PasswordHash);//decrypt passwordhash
                if (response1 != userLogin.Password)
                {

                    return new LoginResponseDto
                    {
                        IsMatched = false,
                        Message = "Login details incorrect",
                    };
                }
                return new LoginResponseDto{ IsMatched = true,  Message = "User has logged in successfully" };
               
            }*/
        }
        public UpdateResponseDto UpdateUser(UpdateUserDto userToUpdate, string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null) 
            { 
                user.FirstName = userToUpdate.FirstName;
                user.LastName = userToUpdate.LastName;
                user.PhoneNumber = userToUpdate.PhoneNumber;
                user.UserName = userToUpdate.UserName;
               bool response = _userRepository.UpdateUser(user);
                if (response)
                {
                    UpdateResponseDto response1 = new UpdateResponseDto
                    {
                        HasUpdated = true,
                        Message = "User was successfully updated"
                    };
                    return response1;
                }
                else 
                {
                    UpdateResponseDto response2 = new UpdateResponseDto
                    {
                        HasUpdated = false,
                        Message = "Something went wrong while updating the user"
                    };
                    return response2;
                }
            }
            UpdateResponseDto response3 = new UpdateResponseDto
            {
                HasUpdated = false,
                Message = "User not found"
            };
            return response3;
        }

        public UpdateResponseDto UpdateUserPartially(string userId, PartialUserUpdateDto userToUpdate)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.UserName = userToUpdate.UserName;
                user.PhoneNumber = userToUpdate.PhoneNumber;
                bool response1 = _userRepository.UpdateUser(user);
                if (response1)
                {
                    UpdateResponseDto response2 = new UpdateResponseDto
                    {
                        HasUpdated = true,
                        Message = "User was successfully updated",
                    };
                    return response2;
                }
                else
                {
                    UpdateResponseDto response3 = new UpdateResponseDto
                    {
                        HasUpdated = false,
                        Message = "Something went wrong while updating the user",
                    };
                    return response3;
                }
            }
                return new UpdateResponseDto   { HasUpdated = false,  Message = "User not found"};
        }

        public UpdateResponseDto ActivateUser(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return new UpdateResponseDto { HasUpdated = false, Message = "User not found" };
                if (!user.IsActive)
                {
                  user.IsActive = true;
                  bool response = _userRepository.UpdateUser(user);
                  if (response)
                  return new UpdateResponseDto { HasUpdated = true, Message = "User has been activated" };
                  return new UpdateResponseDto { HasUpdated = false, Message = "An error occurred whileactivating user" };
                }
                return new UpdateResponseDto { HasUpdated = false, Message = "User is currently active" };           
        }

        public UpdateResponseDto DeactivateUser(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return new UpdateResponseDto { HasUpdated = false, Message = "User not found" };
                if (user.IsActive == true)
                {
                    user.IsActive = false;
                    bool response = _userRepository.UpdateUser(user);
                    if (response)
                        return new UpdateResponseDto { HasUpdated = true, Message = "User has been de-activated successfully" };
                    return new UpdateResponseDto { HasUpdated = false, Message = "An error occurred while de-activating the user" };
                }
                return new UpdateResponseDto { HasUpdated = false, Message = "User is currently inactive" };
        }
    }
}
