using DotNetData_Lb3.Repos;
using System.Data.SqlClient;

namespace DotNetData_Lb3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<DoctorsRepo>();
            builder.Services.AddScoped<PatientsRepo>();
            builder.Services.AddScoped<AppointmentsRepo>();
            builder.Services.AddScoped<DoctorsScheduleRepo>();
            builder.Services.AddScoped<FunctionsRepo>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

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