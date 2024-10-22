using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Web.WebPages;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Domain.SharedModels;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.Interface;


namespace TaskManagerDuplicate.Service.Implementation
{
    public  class UserService : IUserService   
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, IFileService fileService, IEmailService emailService)
        {
         _userRepository = userRepository;
         _roleRepository = roleRepository;
         _mapper = mapper;
         _fileService = fileService;
         _emailService = emailService;
        }
        public async Task<BaseApiResponse<UserCreationResponseDto>> AddUserAsync(UserCreationDto userToAdd)
        {
            {
                var userEmail = _userRepository.GetUserByEmail(userToAdd.EmailAddress);
                if (userEmail != null)
                {
                    return ApiResponseHelper.BuildResponse<UserCreationResponseDto>("User already exists", true, null, StatusCodes.Status200OK);
                }
                var passwordHash = SecurityHelper.Encrypt(userToAdd.Password);
                var response = await _fileService.UploadFileToLocalServer(userToAdd.ProfilePicture);
                if (response != null)
                {
                    User user = _mapper.Map<User>(userToAdd);  //automatic mapping
                    user.ImageUrl = response.FilePath;
                    user.PasswordHash = passwordHash;  //manual mapping for prop dt dont exist in both model.
                    user.PasswordSalt = passwordHash;
                    user.FileName = response.FileName;
                    var response1 = _userRepository.AddUser(user);
                    if (response1)
                    {
                        var subject = "User Registration";
                        var messageToBeSent =EmailTemplateHelper.CreateSignUpTemplate(userToAdd.FirstName,user.UserName);
                       _emailService.SendEmailWithGmailClient(subject, messageToBeSent,new List<string> {user.EmailAddress,"tosinkyle91@gmail.com"});

                        var userToReturn = _mapper.Map<UserCreationResponseDto>(user);
                        userToReturn.FilePath = user.ImageUrl;
                        userToReturn.FileName = user.FileName;
                        userToReturn.Id = user.Id;
                        return ApiResponseHelper.BuildResponse<UserCreationResponseDto>("User was successfully added", true,userToReturn, StatusCodes.Status200OK);
                        /*return new UserCreationResponseDto { HasAdded = true, Message = "User was successfully added", Id = user.Id, FilePath = response.FilePath, FileName = response.FileName };*/
                    }                     
                    else
                        return ApiResponseHelper.BuildResponse<UserCreationResponseDto>("Something went wrong while adding the user", false, null, StatusCodes.Status400BadRequest);
                }
                return ApiResponseHelper.BuildResponse<UserCreationResponseDto>("Profile picture could not be added", false, null, StatusCodes.Status200OK);
               
            }


            /*var userEmail = _userRepository.GetUserByEmail(userToAdd.EmailAddress);
            if (userEmail != null)
                return new UserCreationResponseDto { HasAdded = false, Message = "User already exists" };
                var passwordHash = SecurityHelper.Encrypt(userToAdd.Password);
            var response = await _fileService.UploadFileToCloudinary(userToAdd.ProfilePicture);
            if (response.IsSuccessful)
            {
                User userToBeAdded = new User
                {
                    FirstName = userToAdd.FirstName,
                    LastName = userToAdd.LastName,
                    UserName = userToAdd.UserName,
                    EmailAddress = userToAdd.EmailAddress,
                    PhoneNumber = userToAdd.PhoneNumber,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordHash,
                    ImageUrl = response.ImageUrl,
                };
                var response1 = _userRepository.AddUser(userToBeAdded);
                if (response1)
                    return new UserCreationResponseDto { HasAdded = true, Message = "User was successfully added", Id = userToBeAdded.Id };
                else
                    return new UserCreationResponseDto { HasAdded = false, Message = "Something went wrong while adding the user" };
            }
            else
                return new UserCreationResponseDto { HasAdded = false, Message = "Profile picture could not be added" };*/
        }

        public async Task<BaseApiResponse<DeleteResponseDto>> DeleteUserAsync(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                bool response = _userRepository.DeleteUser(user);
                if (response)
                {
                    return ApiResponseHelper.BuildResponse<DeleteResponseDto>("User was deleted successfully", true, null, StatusCodes.Status200OK);
                }   //return object of this dto

                else
                {
                    return ApiResponseHelper.BuildResponse<DeleteResponseDto>("Something went wrong while deleting the user", true, null, StatusCodes.Status400BadRequest);
                }
            }
            return ApiResponseHelper.BuildResponse<DeleteResponseDto>("User was not found", true, null, StatusCodes.Status403Forbidden);
        }

        public async Task<BaseApiResponse<PaginatedList<UserListDto>>> GetAllUsersAsync(int page, int perPage)
        {
            var userList = _userRepository.GetAllUsers().ToList();
            List<UserListDto> userToReturn = new List<UserListDto>();

            if (userList.Count < 1)
                return ApiResponseHelper
                     .BuildResponse<PaginatedList<UserListDto>>("No user was found", false, null, StatusCodes.Status200OK);
            

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

            var paginatedData = PaginationHelper<UserListDto>.Paginate(userToReturn,perPage,page);

            return ApiResponseHelper
                .BuildResponse<PaginatedList<UserListDto>>("Users were successfully retrieved",true,paginatedData,StatusCodes.Status200OK);           
        }

        public async Task<BaseApiResponse<DisplaySingleUserDto>> GetSingleUserByEmailAsync(string userEmail)
        {
            var user = _userRepository.GetUserByEmail(userEmail);
            if (user == null)

                return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("No user was found",true,null,StatusCodes.Status200OK);
            else
            {
                DisplaySingleUserDto singleUser = _mapper.Map<DisplaySingleUserDto>(user);
                return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("User was successfully retrieved", true, singleUser, StatusCodes.Status200OK);
            }
        }

        public async Task<BaseApiResponse<DisplaySingleUserDto>> GetSingleUserByIdAsync(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("No user was found", true, null, StatusCodes.Status200OK);
            else
            {
             DisplaySingleUserDto singleUser = _mapper.Map<DisplaySingleUserDto>(user);
                return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("User was successfully retrieved", true, singleUser,StatusCodes.Status200OK);
            }
        }

        public async Task<BaseApiResponse<DisplaySingleUserDto>> LoginAsync(UserLoginDto userLogin)
        {
            var response = _userRepository.GetUserByEmail(userLogin.EmailAddress);
            if (response == null)
            {
                return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("User with email address was not found", true, null, StatusCodes.Status200OK);
            }
            else
            {
                var response1 = SecurityHelper.Decrypt(response.PasswordHash);
                if (response1 != userLogin.Password)
                {
                    return null;   //what to do here
                }
                else
                {
                    var userToReturn = new DisplaySingleUserDto {
                        Id = response.Id,
                        UserName = response.UserName,
                        FirstName = response.FirstName,
                        LastName = response.LastName,
                        PhoneNumber = response.PhoneNumber,
                        EmailAddress = response.EmailAddress,
                        ImageUrl = response.ImageUrl,
                        CreatedOn = response.CreatedOn,
                        IsActive = response.IsActive
                    };
                    return ApiResponseHelper.BuildResponse<DisplaySingleUserDto>("User with email address was not found", true, userToReturn, StatusCodes.Status200OK);
                }
            }

        }
        public async Task<BaseApiResponse<UpdateResponseDto>> UpdateUserAsync(UpdateUserDto userToUpdate, string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null) 
            {
               user= _mapper.Map<UpdateUserDto,User>(userToUpdate,user);   //is this correct
               bool response = _userRepository.UpdateUser(user);
                if (response)
                {
                    /* UpdateResponseDto response1 = new UpdateResponseDto
                     {
                         HasUpdated = true,
                         Message = "User was successfully updated"
                     };
                     return response1;*/
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User was successfully updated",true,null,StatusCodes.Status200OK);
                }
                else 
                {
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Something went wrong while updating the user", true, null, StatusCodes.Status200OK);
                }
            }
            return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User was not found", true, null, StatusCodes.Status404NotFound);
        }

        public async Task<BaseApiResponse<UpdateResponseDto>> UpdateUserPartiallyAsync(string userId, PartialUserUpdateDto userToUpdate)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.UserName = userToUpdate.UserName;
                user.PhoneNumber = userToUpdate.PhoneNumber;
                bool response1 = _userRepository.UpdateUser(user);
                if (response1)
                {
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User was successfully updated",true,null,StatusCodes.Status200OK);
                }
                else
                {
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Something went wrong while updating the user", false, null, StatusCodes.Status200OK);
                }
            }
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User was not found",false,null,StatusCodes.Status404NotFound);
        }

        public async Task<BaseApiResponse<UpdateResponseDto>> ActivateUserAsync(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User not found", false, null, StatusCodes.Status404NotFound);
            if (!user.IsActive)
            {
                user.IsActive = true;
                bool response = _userRepository.UpdateUser(user);
                if (response)
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User has been activated", true, null, StatusCodes.Status200OK);
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Something went wrong while activating the user", false, null, StatusCodes.Status400BadRequest);          
            }
            return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User is currently active", false, null, StatusCodes.Status400BadRequest);
        }
        public async Task<BaseApiResponse<UpdateResponseDto>> DeactivateUserAsync(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User not found",true,null,StatusCodes.Status200OK);
                if (user.IsActive == true)
                {
                    user.IsActive = false;
                    bool response = _userRepository.UpdateUser(user);
                    if (response)
                        return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User has been de-activated successfully", true, null, StatusCodes.Status200OK);
                return  ApiResponseHelper.BuildResponse<UpdateResponseDto>("An error occurred while de-activating the user", false, null, StatusCodes.Status200OK);
                }
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("User is currently inactive", true, null, StatusCodes.Status400BadRequest);
        }
        public async Task<BaseApiResponse<UpdateResponseDto>>  RemoveRoleFromUserAsync(string userId, string roleId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("user does not exist", false, null, StatusCodes.Status200OK);
            var role = _roleRepository.GetRoleById(roleId);
            if (role == null)
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("role does not exist", false, null, StatusCodes.Status200OK);

            if (user.RoleId!= roleId)
                {
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("role does not hold this role", false, null, StatusCodes.Status200OK);
                }
                user.RoleId = null;//why not user1.RoleId ==string.Empty;
                var res = _userRepository.UpdateUser(user);
                if(!res)
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("user role could not be removed", false, null, StatusCodes.Status200OK);
            return ApiResponseHelper.BuildResponse<UpdateResponseDto>("user role was successfully removed", true, null, StatusCodes.Status200OK);
        }

        public async Task<BaseApiResponse<UpdateResponseDto>>  ChangeUserRoleAsync(string userId, string roleToAddId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("user does not exist", false, null, StatusCodes.Status200OK);
            var role = _roleRepository.GetRoleById(roleToAddId);
                if (role == null)
                {
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("role does not exist", false, null, StatusCodes.Status200OK);
               }
                else 
                {
                    user.RoleId = roleToAddId;
                    var response=_userRepository.UpdateUser(user);
                    if (!response)
                    {
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("something went wrong while updating the user role", false, null, StatusCodes.Status200OK);
                }
                    else 
                    {
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("user role was successfully updated", true, null, StatusCodes.Status200OK);
                    }
                }
        }

        public async Task<BaseApiResponse<DisplayUserFirstLastNameDto>> GetUserFirstAndLastNameAsync(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return null;
            }
            else 
            {
                DisplayUserFirstLastNameDto userToDisplay = _mapper.Map<DisplayUserFirstLastNameDto>(user);
                return ApiResponseHelper.BuildResponse<DisplayUserFirstLastNameDto>("user names were successfully retrieved", true, userToDisplay, StatusCodes.Status200OK);
            }
        }

        public async Task<BaseApiResponse<List<string>>> GetAllUserEmailsAsync()
        {
            var response = _userRepository.GetAllUsers();
            List<string> userEmaiList = new List<string>();
            foreach (var user in response)
            {
                userEmaiList.Add(user.EmailAddress);
            }
            return ApiResponseHelper.BuildResponse<List<string>>("Successful",true,userEmaiList,StatusCodes.Status200OK);
        }

        public async Task<BaseApiResponse<BulkMessageDto>> SendBulkEmailToUsersAsync(List<string> userEmails)
        {
            var response= await GetAllUserEmailsAsync();
            if (response == null)
            {
                return ApiResponseHelper.BuildResponse<BulkMessageDto>("Message could not be sent because there are no user emails",true,null,StatusCodes.Status200OK);
            }
            else
            {
               response.Data.Add("tosinkyle91@gmail.com");
                var subject = "Welcome Message";
                var messageToBeSent =EmailTemplateHelper.SendBulkMessage();
                _emailService.SendEmailWithGmailClient(subject, messageToBeSent, response.Data);//want an explanation
                 return ApiResponseHelper.BuildResponse<BulkMessageDto>("Message has been sent successfully",true,null,StatusCodes.Status200OK);
            }
        }
    }
}
