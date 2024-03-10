using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Domain.PasswordHasher.Interface;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUserRepository userRepository,IPasswordHasher passwordHasher) 
        {
         _userRepository = userRepository;
         _passwordHasher = passwordHasher;
        }
        public string AddUser(UserCreationDto userToAdd)
        {
            var passwordHash = _passwordHasher.HashPassword(userToAdd.Password);
            var saltedPassword = _passwordHasher.SaltedPassword(userToAdd.Password);
            User userToBeAdded = new User
            {

                FirstName = userToAdd.FirstName,
                LastName = userToAdd.LastName,
                UserName = userToAdd.UserName,
                EmailAddress = userToAdd.EmailAddress,
                PhoneNumber = userToAdd.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = saltedPassword,
                ImageUrl = userToAdd.ProfilePicture
            };
            var response = _userRepository.AddUser(userToBeAdded);
            if (response)
                return userToBeAdded.Id;
            else 
                return null;
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
                    CreatedOn = user.CreatedOn
                };
                return singleUser;
            }
        }

        public string GetUserByEmail(string userEmail)
        {
           var user=_userRepository.GetUserByEmail(userEmail);
            if (user == null)
                return null;
            else
                return user.EmailAddress;
        }

        public string GetUserByPassword(string Password)
        {
            var user = _userRepository. GetUserByPassword(Password);
            if (user == null)
                return null;
            else
                return user.UserName;
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
    }
}
