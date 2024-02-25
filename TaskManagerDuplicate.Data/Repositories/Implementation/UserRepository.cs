using TaskManagerDuplicate.Data.Context;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly EntityFrameworkContext _entityFrameworkContext;
        public UserRepository(EntityFrameworkContext entityFrameworkContext)
        {
         _entityFrameworkContext = entityFrameworkContext;
        }
        public bool AddUser(User userToAdd)
        {
            _entityFrameworkContext.User.Add(userToAdd);
           return _entityFrameworkContext.SaveChanges()>0;
        }

        public bool DeleteUser(User userToRemove)
        {
            _entityFrameworkContext.User.Remove(userToRemove);
            return _entityFrameworkContext.SaveChanges()>0;
        }

        public IQueryable<User> GetAllUsers()=> _entityFrameworkContext.User;
        public User GetUserById(string userId)=> _entityFrameworkContext.User.FirstOrDefault(x => x.Id == userId);

        public bool PartialUserUpdate(User userToBeUpdated)
        {
             _entityFrameworkContext.User.Update(userToBeUpdated);
             return _entityFrameworkContext.SaveChanges() > 0;
           /*     _entityFrameworkContext.User.Update(userToBeUpdated);
            bool response= _entityFrameworkContext.SaveChanges()>0;
            if (response)
             return true;
            return false;*/
        }

        public bool UpdateUser(User userToUpdate)
        {
            _entityFrameworkContext.User.Update(userToUpdate);
            return _entityFrameworkContext.SaveChanges() > 0;
        }
    }
}
