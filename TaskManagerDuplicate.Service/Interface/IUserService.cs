using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IUserService
    {
        public Task<BaseApiResponse<UserCreationResponseDto>> AddUserAsync(UserCreationDto userToAdd);
        public Task<BaseApiResponse<UpdateResponseDto>> UpdateUserAsync(UpdateUserDto userToUpdate,string userId);
        public Task<BaseApiResponse<DeleteResponseDto>> DeleteUserAsync(string userId);
        public Task<BaseApiResponse<DisplaySingleUserDto>> GetSingleUserByIdAsync(string userId);
        public Task<BaseApiResponse<PaginatedList<UserListDto>>> GetAllUsersAsync(int page, int perPage); 
        public Task<BaseApiResponse<UpdateResponseDto>> UpdateUserPartiallyAsync(string userId, PartialUserUpdateDto userToUpdate);
        public Task<BaseApiResponse<DisplaySingleUserDto>> LoginAsync(UserLoginDto userLogin);
        public Task<BaseApiResponse<DisplaySingleUserDto>> GetSingleUserByEmailAsync(string userId);
        public Task<BaseApiResponse<UpdateResponseDto>> ActivateUserAsync(string userId);
        public Task<BaseApiResponse<UpdateResponseDto>> DeactivateUserAsync(string userId);
        public Task<BaseApiResponse<UpdateResponseDto>> RemoveRoleFromUserAsync(string userId, string roleId);
        public Task<BaseApiResponse<UpdateResponseDto>> ChangeUserRoleAsync(string userId, string roleToAddId);
        public Task<BaseApiResponse<DisplayUserFirstLastNameDto>> GetUserFirstAndLastNameAsync(string userId);
        public Task<BaseApiResponse<List<string>>> GetAllUserEmailsAsync();
        public Task<BaseApiResponse<BulkMessageDto>> SendBulkEmailToUsersAsync(List<string> userEmails);
    }
}
