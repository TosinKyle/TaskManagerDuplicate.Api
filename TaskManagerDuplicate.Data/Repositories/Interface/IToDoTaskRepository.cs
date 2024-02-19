using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Repositories.Interface
{
    public interface IToDoTaskRepository
    {
        public bool AddTask(ToDoTask taskToUpdate);
        public bool UpdateTask(ToDoTask taskToUpdate);
        public bool DeleteTask(ToDoTask taskToRemove);
        public ToDoTask GetTask(string taskId);
        public IQueryable<ToDoTask> GetAllTasks();
    }
}
