using System;
using TaskManagerDuplicate.Data.Context;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Repositories.Implementation
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly EntityFrameworkContext _entityFrameworkContext;
        public ToDoTaskRepository(EntityFrameworkContext entityFrameworkContext) 
        {
         _entityFrameworkContext = entityFrameworkContext;
        }
        public bool AddTask(ToDoTask taskToUpdate)
        {
            _entityFrameworkContext.ToDoTask.Add(taskToUpdate);
            return  _entityFrameworkContext.SaveChanges()>0;
        }
        public bool DeleteTask(ToDoTask taskToRemove)
        {
            _entityFrameworkContext.ToDoTask.Remove(taskToRemove);
            return _entityFrameworkContext.SaveChanges() > 0;

        }
        public IQueryable<ToDoTask> GetAllTasks()=> _entityFrameworkContext.ToDoTask;
        public ToDoTask GetTask(string taskId) => _entityFrameworkContext.ToDoTask.FirstOrDefault(task=>task.Id == taskId);
        public bool UpdateTask(ToDoTask taskToUpdate)
        {
            _entityFrameworkContext.Update(taskToUpdate);
            return _entityFrameworkContext.SaveChanges() > 0;
        }
    }
}
