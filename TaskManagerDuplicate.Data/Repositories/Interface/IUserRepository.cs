using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Repositories.Interface
{
    public interface IUserRepository
    {
        public bool AddUser(User userToAdd);
        public bool UpdateUser(User userToUpdate);
        public bool DeleteUser(User userToRemove);
        public User GetUserById(string userId);
        public IQueryable<User> GetAllUsers();
        public bool PartialUserUpdate(User userToBeUpdated);
    }
}
