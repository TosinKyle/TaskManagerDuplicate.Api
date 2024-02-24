using TaskManagerDuplicate.Domain.DataTransferObjects;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IToDoTaskService
    {
        public string AddTask(CreateTaskDto taskToAdd);
        public DeleteResponseDto DeleteTask(string id);
        public UpdateResponseDto UpdateTask(string id,UpdateTaskDto taskToUpdate);
        public DisplaySingleTaskDto GetSingleTask(string id);
        public List<TaskListDto> GetAllTasks();
    }
}
