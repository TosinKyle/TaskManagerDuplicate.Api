using TaskManagerDuplicate.Domain.DataTransferObjects;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IUserService
    {
        public string AddUser(UserCreationDto userToAdd);
        public UpdateResponseDto UpdateUser(UpdateUserDto userToUpdate,string userId);
        public DeleteResponseDto DeleteUser(string userId);
        public DisplaySingleUserDto GetSingleUserById(string userId);
        public List<UserListDto> GetAllUsers();
    }
}
