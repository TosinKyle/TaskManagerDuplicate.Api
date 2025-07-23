using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Web;
using TaskManagerDuplicate.Data.Enums;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Domain.SharedModels;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.Interface;
using IUserService = TaskManagerDuplicate.Service.Interface.IUserService;
using TaskStatus = TaskManagerDuplicate.Data.Enums.TaskStatus;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public ToDoTaskService(IToDoTaskRepository taskRepository, IMapper mapper, IUserService userService) 
        {
         _toDoTaskRepository = taskRepository;
         _mapper = mapper;
            _userService = userService;
        }
        public async Task<BaseApiResponse<TaskCreationResponseDto>> AddTaskAsync(CreateTaskDto taskToAdd)
        {
            ToDoTask task = _mapper.Map<ToDoTask>(taskToAdd);
            task.Name = taskToAdd.TaskName; //manual mapping as they do not carry same name.
            task.DueDate=task.StartDate.AddDays(2);
            task.DateOfCompletion = task.DueDate;
            task.Status = TaskStatus.status.WAITINGFORSUPPORT.ToString();
            bool response = _toDoTaskRepository.AddTask(task);
            if(response)
            {
                var taskToReturn = new TaskCreationResponseDto();
                taskToReturn.TaskId = task.Id;
                return ApiResponseHelper.BuildResponse<TaskCreationResponseDto>("Task was successfully added", true,taskToReturn,StatusCodes.Status200OK);
            }
            return ApiResponseHelper.BuildResponse<TaskCreationResponseDto>("Something went wrong while adding the task", false, null, StatusCodes.Status400BadRequest);
        }

        public async Task<BaseApiResponse<UpdateResponseDto>> UpdateToDoTaskStatusAsync(TaskStatusUpdateDto toUpdateTaskStatus, string taskId,string userId)
        {
            var task = _toDoTaskRepository.GetTask(taskId);//only repo can access the db, can service call service method
            if (task == null) 
            {
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("The task could not be found", false, null, StatusCodes.Status404NotFound);
            }
            var response1 = _userService.GetSingleUserByIdAsync(userId);
            if (response1 == null)
            {
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("The user that wants to update could not be found", false, null, StatusCodes.Status404NotFound);
            }
            else 
            { 
                task.Status = toUpdateTaskStatus.NewStatus;
                if (task.Status == "RESOLVED")
                {
                    task.IsCompleted = true;
                    task.DateOfCompletion = DateTime.Now;
                }
                bool response2 = _toDoTaskRepository.UpdateTask(task);
                if (!response2)
                {
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Something went wrong while updating the task status", false, null, StatusCodes.Status400BadRequest);
                }
                else
                {
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("The status was successfully updated", true, null, StatusCodes.Status200OK);
                }
            }
        }
        public async Task<BaseApiResponse<DeleteResponseDto>> DeleteTaskAsync(string id)
        {
            ToDoTask task = _toDoTaskRepository.GetTask(id);
            if (task != null)
            {
                bool response =_toDoTaskRepository.DeleteTask(task);
                if (response)
                {
                    return ApiResponseHelper.BuildResponse<DeleteResponseDto>("The task has been deleted successfully",true,null,StatusCodes.Status200OK);
                }
                return ApiResponseHelper.BuildResponse<DeleteResponseDto>("Something went wrong while deleting the user", false, null, StatusCodes.Status400BadRequest);

            }
            return ApiResponseHelper.BuildResponse<DeleteResponseDto>("Task was not found", false, null, StatusCodes.Status404NotFound);
        }
        public async Task<BaseApiResponse<PaginatedList<TaskListDto>>> GetAllTasksAsync(int page, int perPage,string status)
        {
                var taskList = _toDoTaskRepository.GetAllTasks().ToList();
                if (taskList.Count < 1)
                    return ApiResponseHelper
                         .BuildResponse<PaginatedList<TaskListDto>>("No task was found", false, null, StatusCodes.Status404NotFound);
            if (!string.IsNullOrEmpty(status))
            {
                var taskList1 = taskList.AsQueryable().Where(task => task.Status == status).ToList();//Where(task => task.Status == status).ToList();
                if (taskList1.Count < 1)
                {
                    return ApiResponseHelper
                            .BuildResponse<PaginatedList<TaskListDto>>($"No {status} task was found", false, null, StatusCodes.Status404NotFound);
                }
                List<TaskListDto> listToReturn = new List<TaskListDto>();
                foreach (ToDoTask task in taskList1)
                {
                    listToReturn.Add(new TaskListDto
                    {
                        TaskId = task.Id,
                        TaskName = task.Name,
                        DueDate = task.DueDate,
                        TaskDescription = task.Description,
                        CreatedDate = task.CreatedOn,
                        CompletionDate = task.DateOfCompletion,
                        IsCompleted = task.IsCompleted,
                        Status = task.Status,
                    });
                }
                var paginatedData = PaginationHelper<TaskListDto>.Paginate(listToReturn, perPage, page);
                return ApiResponseHelper
                         .BuildResponse<PaginatedList<TaskListDto>>("Here is the list of tasks", true, paginatedData, StatusCodes.Status200OK);
            }
            else 
            {
                List<TaskListDto> listToReturn = new List<TaskListDto>();
                foreach (ToDoTask task in taskList)
                {
                    listToReturn.Add(new TaskListDto
                    {
                        TaskId = task.Id,
                        TaskName = task.Name,
                        DueDate = task.DueDate,
                        TaskDescription = task.Description,
                        CreatedDate = task.CreatedOn,
                        CompletionDate = task.DateOfCompletion,
                        IsCompleted = task.IsCompleted,
                        Status = task.Status,
                    });
                }
                var paginatedData = PaginationHelper<TaskListDto>.Paginate(listToReturn, perPage, page);
                return ApiResponseHelper
                         .BuildResponse<PaginatedList<TaskListDto>>("Here is the list of tasks", true, paginatedData, StatusCodes.Status200OK);
            }

        }
        public async Task<BaseApiResponse<DisplaySingleTaskDto>> GetSingleTaskAsync(string id)
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
                return ApiResponseHelper.BuildResponse<DisplaySingleTaskDto>("The request was successful", true, taskToDisplay, StatusCodes.Status200OK);
            }
            return ApiResponseHelper.BuildResponse<DisplaySingleTaskDto>("Task could not be found", false, null, StatusCodes.Status400BadRequest);
        }
        public async Task<BaseApiResponse<UpdateResponseDto>> UpdateTaskAsync(string id, UpdateTaskDto taskToUpdate)
        {
            var task= _toDoTaskRepository.GetTask(id);
            if (task!= null)
            {
                task.Name = taskToUpdate.Name;
                task.Description = taskToUpdate.TaskDescription;
                task.DueDate = taskToUpdate.DueDate;
                bool response1 = _toDoTaskRepository.UpdateTask(task);
                if (response1)
                {
                    return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Task was successfully updated", true, null, StatusCodes.Status200OK);
                }
                return ApiResponseHelper.BuildResponse<UpdateResponseDto>("Something went wrong while updating the task", false, null, StatusCodes.Status400BadRequest);
            }
            else
            {
                return ApiResponseHelper.BuildResponse<UpdateResponseDto> ("Task was not found", false, null, StatusCodes.Status404NotFound);
            }
        }
    }
}

    
        