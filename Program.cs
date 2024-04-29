using DotNetData_Lb3.Repos;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DotNetData_Lb3.Models;

namespace DotNetData_Lb3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession();

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<DoctorsRepo>();
            builder.Services.AddScoped<PatientsRepo>();
            builder.Services.AddScoped<AppointmentsRepo>();
            builder.Services.AddScoped<DoctorsScheduleRepo>();
            builder.Services.AddScoped<FunctionsRepo>();

            builder.Services.BuildServiceProvider()
                .GetRequiredService<DoctorsRepo>()
                .CreateIndexes();

            builder.Services.BuildServiceProvider()
                .GetRequiredService<PatientsRepo>()
                .CreateIndexes();

            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Doctor}/{action=doctors}/{id?}");
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Doctor}/{action=doctors}/{id?}");
            //});


            app.Run();
        }
    }
}