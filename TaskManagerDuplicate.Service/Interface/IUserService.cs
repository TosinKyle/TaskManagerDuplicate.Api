using TaskManagerDuplicate.Domain.DataTransferObjects;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IUserService
    {
        public UserCreationResponseDto AddUser(UserCreationDto userToAdd);
        public UpdateResponseDto UpdateUser(UpdateUserDto userToUpdate,string userId);
        public DeleteResponseDto DeleteUser(string userId);
        public DisplaySingleUserDto GetSingleUserById(string userId);
        public List<UserListDto> GetAllUsers();
        public UpdateResponseDto UpdateUserPartially(string userId, PartialUserUpdateDto userToUpdate);
        public DisplaySingleUserDto Login(UserLoginDto userLogin);
        public DisplaySingleUserDto GetSingleUserByEmail(string userId);
        public UpdateResponseDto ActivateUser(string userId);
        public UpdateResponseDto DeactivateUser(string userId);
    }
}
