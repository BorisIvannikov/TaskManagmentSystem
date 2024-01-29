using Microsoft.EntityFrameworkCore;
using TaskManagmentSystem.DAL;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.DAL.Repositories;
using TaskManagmentSystem.Models;
using System.Data.SqlClient;
using TaskManagmentSystem.Service.Interfaces;
using TaskManagmentSystem.Service.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagmentSystem.Filters;

namespace TaskManagmentSystem
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region поставщик TempData
            builder.Services.AddRazorPages()
                    .AddSessionStateTempDataProvider();
            builder.Services.AddControllersWithViews()
                                .AddSessionStateTempDataProvider();

            builder.Services.AddSession();
            #endregion

            var connectionString = builder.Configuration.GetConnectionString("MSSQL");
            
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.LogTo(Console.WriteLine);
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IUserEntityRepository, UserEntityRepository>();
            builder.Services.AddScoped<IUserEntityService, UserEntityService>();

            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            
            builder.Services.AddScoped<ITaskEntityRepository, TaskEntityRepository>();
            builder.Services.AddScoped<ITaskEntityService, TaskEntityService>();
            
            builder.Services.AddScoped<IUserEntityProfileRepository, UserEntityProfileRepository>();
            builder.Services.AddScoped<IUserEntityProfileService, UserEntityProfileService>();

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IAuthorizationFilter, AuthorizeFilter>();

            builder.Services.AddControllers(option =>
            {
                option.Filters.Add<AuthorizeFilter>();
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = new PathString("/UserEntityProfile/Login");
                    option.AccessDeniedPath = new PathString("/UserEntityProfile/Login");
                });

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            /*
            app.UseSwagger();
            app.UseSwaggerUI(c=>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("swagger/v1/swagger.json", "TaskManagmentSystemAPI");
            });*/

            app.UseAuthentication();
            app.UseAuthorization();

            #region поставщик ссесии
            app.UseSession();
            #endregion
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}