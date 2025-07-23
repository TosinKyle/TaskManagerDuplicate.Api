using Microsoft.EntityFrameworkCore;
using TaskManagerDuplicate.Data.Context;
using TaskManagerDuplicate.Data.Repositories.Implementation;
using TaskManagerDuplicate.Data.Repositories.Interface;
using TaskManagerDuplicate.Service.Implementation;
using TaskManagerDuplicate.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;
using Optivem.Framework.Core.Domain;
using System.Reflection;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.MappingProfiles;
using IUserService = TaskManagerDuplicate.Service.Interface.IUserService;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;

namespace TaskManagerDuplicate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {        
           try
           {
                Log.Information("Application starting up");
                var builder = WebApplication.CreateBuilder(args); //TODO: STUDY
                
                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Task Manager",
                        Version = "1.0",
                        TermsOfService = new Uri("https://www.taskmanager.ng/"),
                        Description = "This is an ASP.NET Api for managing tasks and activities.",
                        Contact = new OpenApiContact
                        {
                            Name = "Contact Authors",
                            Url = new Uri("https://www.taskmanager.ng/"),
                            Email = "tosinkyle91@gmail.com"
                        }
                    });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                       },new List<string>()
                    }
                });
                    var baseUrl = AppContext.BaseDirectory.ToString();
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
                });
                ConfigurationHelper.InstantiateConfiguration(builder.Configuration);//This line passes the configuration object from the builder into your static helper class, so that it can be accessed globally using:
                Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
                builder.Host.UseSerilog();
                builder.Services.AddDbContext<EntityFrameworkContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });
                builder.Services.AddAutoMapper(typeof(MappingProfile));
                builder.Services.AddScoped<IUserRepository, UserRepository>();
                builder.Services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
                builder.Services.AddScoped<IToDoTaskService, ToDoTaskService>();
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<IRoleRepository, RoleRepository>();
                builder.Services.AddScoped<IRoleService, RoleService>();
                builder.Services.AddScoped<IFileService, FileService>();
                builder.Services.AddScoped<IEmailService, EmailService>();
                builder.Services.AddScoped<ITwoFactorAuthentication, TwoFactorAuthentication>();
                builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
                builder.Services.AddScoped<IOTPService, OTPService>();
                builder.Services.AddScoped<IOTPRepository, OTPRepository>();
                // builder.Services.AddScoped<>();
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                        };
                    }
                   );
                var app = builder.Build();
                // var userService =  app.Services.GetService<IUserService>();//TODO: STUDY2
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment()
                    || app.Environment.IsProduction())
                {
                    app.UseSwagger(); app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
                    });
                }
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseSerilogRequestLogging();

                app.UseHttpsRedirection();

                app.MapControllers();

                app.Run();

           }
            catch (Exception ex)
           {

                Log.Fatal(ex, "The application failed to start correctly.");
           }
            finally
           {
             Log.CloseAndFlush(); //to ensure any unwritten/pending logs are written and closes it.
           }
           // Serilog.Debugging.SelfLog.Enable(msg => Console.Error.WriteLine(msg));

        }
    }
}