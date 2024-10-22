using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IToDoTaskService
    {
        public Task<BaseApiResponse<TaskCreationResponseDto>> AddTaskAsync(CreateTaskDto taskToAdd);
        public Task<BaseApiResponse<DeleteResponseDto>> DeleteTaskAsync(string id);
        public Task<BaseApiResponse<UpdateResponseDto>> UpdateTaskAsync(string id,UpdateTaskDto taskToUpdate);
        public Task<BaseApiResponse<DisplaySingleTaskDto>> GetSingleTaskAsync(string id);
        public Task<BaseApiResponse<PaginatedList<TaskListDto>>> GetAllTasksAsync(int page, int perPage);
        public Task<BaseApiResponse<UpdateResponseDto>> UpdateToDoTaskStatusAsync(TaskStatusUpdateDto toUpdateTaskStatus, string taskId, string userId);

    }
}
