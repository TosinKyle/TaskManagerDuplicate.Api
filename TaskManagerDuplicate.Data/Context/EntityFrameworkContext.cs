using Microsoft.EntityFrameworkCore;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Data.Context
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext(DbContextOptions<EntityFrameworkContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<ToDoTask> ToDoTask { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<OTP> OTP { get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder contextBuilder)
         {
            contextBuilder.UseSqlServer("Server=CSCS-HQ-PS-L025\\SQLEXPRESS;Database=TaskManagerDuplicate;TrustServerCertificate=true;Trusted_Connection=true;");
         }
    }
}
