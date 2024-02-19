using Microsoft.EntityFrameworkCore;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Context
{
    public class EntityFrameworkContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<ToDoTask> ToDoTask { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder contextBuilder)
        {
            contextBuilder.UseSqlServer("Server=CSCS-HQ-PS-L025\\SQLEXPRESS;Database=TaskManagerDuplicate;TrustServerCertificate=true;Trusted_Connection=true;");
        }
    }
}
