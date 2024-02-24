using Microsoft.EntityFrameworkCore;
using TaskManagerDuplicate.Data.Context;
using TaskManagerDuplicate.Data.Repositories.Implementation;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Service.Implementation;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EntityFrameworkContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddScoped<IToDoTaskRepository,ToDoTaskRepository>();
            builder.Services.AddScoped<IToDoTaskService,ToDoTaskService>();
            builder.Services.AddScoped<IUserService,UserService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}