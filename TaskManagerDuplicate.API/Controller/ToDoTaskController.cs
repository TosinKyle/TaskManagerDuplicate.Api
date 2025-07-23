
using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.SharedModels;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : BaseController
    {
        private readonly IToDoTaskService _taskService;
        public ToDoTaskController(IToDoTaskService taskService)
        {
            _taskService = taskService;
        }
        /// <summary>
        /// This endpoint is responsible for creating a ToDoTask object
        /// </summary>
        /// <param name="taskToAdd"></param>
        /// <returns></returns>
        [HttpPost("new-task-creation")]
        public async Task<IActionResult> AddTaskAsync([FromBody] CreateTaskDto taskToAdd) => BuildHttpResponse(await _taskService.AddTaskAsync(taskToAdd));
        /// <summary>
        /// This endpoint is responsible for updating the status of a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        /// <param name="toUpdateTaskStatus"></param>
        /// <returns></returns>
        [HttpPost("status-update/{userId}/{taskId}")]
        public async Task<IActionResult> UpdateToDoTaskStatusAsync([FromBody] TaskStatusUpdateDto toUpdateTaskStatus, [FromRoute]string taskId, [FromRoute]string userId) 
        { 
            var response= await _taskService.UpdateToDoTaskStatusAsync(toUpdateTaskStatus, taskId, userId);
            return BuildHttpResponse(response);        
        }
        /// <summary>
        /// This endpoint is responsible for updating a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskToUpdate"></param>
        /// <returns></returns>
        [HttpPatch("task-update/{taskId}")]
        public async Task<IActionResult> UpdateTaskAsync([FromRoute] string taskId, [FromBody] UpdateTaskDto taskToUpdate)
        { 
         var response = await _taskService.UpdateTaskAsync(taskId, taskToUpdate);
         return BuildHttpResponse(response);
        }
        /// <summary>
        /// This endpoint is responsible for deleting a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTaskAsync([FromRoute] string taskId) 
        { 
         var response=await _taskService.DeleteTaskAsync(taskId);
         return BuildHttpResponse(response);
        }
        /// <summary>
        /// This endpoint is responsible for getting a single task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("get-single-task/{taskId}")]
        public async Task<IActionResult> GetSingleTaskAsync([FromRoute] string taskId) 
        {
         var response = await _taskService.GetSingleTaskAsync(taskId);
         return BuildHttpResponse(response);
        }
        /// <summary>
        /// This endpoint is responsible for getting all tasks
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("task-list")]
        public async Task<IActionResult> GetAllTasksAsync([FromQuery] int page, [FromQuery] int perPage, [FromQuery]string? status)
        {
         var response = await _taskService.GetAllTasksAsync(page,perPage,status);
         return BuildHttpResponse(response);
        }

    }
}
