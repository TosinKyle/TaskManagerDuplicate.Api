using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        public ToDoTaskService(IToDoTaskRepository taskRepository) 
        {
         _toDoTaskRepository = taskRepository;
        }
        public string AddTask(CreateTaskDto taskToAdd)
        {
            ToDoTask task = new ToDoTask();
            task.Name = taskToAdd.TaskName;
            task.Description = taskToAdd.Description;
            task.DueDate = taskToAdd.DueDate;
            bool response =_toDoTaskRepository.AddTask(task);
            if (response)
                return task.Id;
            else
                return null;
        }
        public DeleteResponseDto DeleteTask(string id)
        {
            ToDoTask task = _toDoTaskRepository.GetTask(id);
            if (task != null)
            {
                bool response =_toDoTaskRepository.DeleteTask(task);
                if (response)
                {
                    return new DeleteResponseDto
                    {
                        HasDeleted = true,
                        Message = "The task was successfully deleted"
                    };
                }
                return new DeleteResponseDto
                {
                    HasDeleted = false,
                    Message = "Something went wrong while deleting the user"
                };       
            }
            return new DeleteResponseDto { HasDeleted = false, Message = "User not found" };
        }
        public List<TaskListDto> GetAllTasks()
        {
            var taskList = _toDoTaskRepository.GetAllTasks();
            List<TaskListDto> listToDisplay = new List<TaskListDto>();
            foreach (ToDoTask task in taskList)
            {
                listToDisplay.Add(new TaskListDto
                {
                    TaskId = task.Id,
                    TaskName = task.Name,
                    DueDate = task.DueDate,
                    TaskDescription = task.Description,
                    CreatedDate = task.CreatedOn,
                    CompletionDate = task.DateOfCompletion,
                    IsCompleted = task.IsCompleted,
                }); 
            }
            return listToDisplay;
        }
        public DisplaySingleTaskDto GetSingleTask(string id)
        {
            ToDoTask task = _toDoTaskRepository.GetTask(id);
            if (task != null)
            {

                DisplaySingleTaskDto taskToDisplay = new DisplaySingleTaskDto
                {
                    TaskId = task.Id,
                    TaskName = task.Name,
                    TaskDescription = task.Description,
                    CreatedDate = task.CreatedOn,
                    DueDate = task.DueDate,
                    IsCompleted = task.IsCompleted,
                };
                    return taskToDisplay;
            }
            return null;
        }
        public UpdateResponseDto UpdateTask(string id, UpdateTaskDto taskToUpdate)
        {
            var response = _toDoTaskRepository.GetTask(id);
            if (response != null)
            {
               ToDoTask task = new ToDoTask();
               task.Name = taskToUpdate.Name;
               task.Description = taskToUpdate.TaskDescription;
               task.DueDate = taskToUpdate.DueDate;
               bool response1 = _toDoTaskRepository.UpdateTask(task);
                if (response1)
                {
                    return new UpdateResponseDto
                    {
                        HasUpdated = true,
                        Message = "Task was successfully updated"
                    };
                }
                else
                return new UpdateResponseDto
                {
                    HasUpdated = false,
                    Message = "Something went wrong while updating the task"
                };
            }
            else 
            {
                return new UpdateResponseDto
                {
                    HasUpdated = false,
                    Message = "Task was not found"
                };
            }
        }
    }
}
