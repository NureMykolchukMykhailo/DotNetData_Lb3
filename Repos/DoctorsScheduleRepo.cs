using DotNetData_Lb3.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetData_Lb3.Repos
{
    public class DoctorsScheduleRepo
    {
        DatabaseContext context;
        public DoctorsScheduleRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<DoctorsSchedule>> GetDoctorsSchedules()
        {
            return await context.Schedules.Include(s => s.Doctor).ToListAsync();
            //List<DoctorsSchedule> doctorsSchedules = new();

            //using (SqlConnection connection = new(connectionString))
            //{
            //    string sqlQuery = "SELECT ds.day_of_week, ds.start_time, ds.end_time, " +
            //        "d.first_name, d.last_name, d.phone_number, d.speciality " +
            //        "FROM doctor_schedule as ds INNER JOIN doctors as d ON d.doctor_id = ds.doctor_id; ";

            //    SqlCommand command = new(sqlQuery, connection);

            //    await connection.OpenAsync();
            //    SqlDataReader reader = await command.ExecuteReaderAsync();

            //    while (await reader.ReadAsync())
            //    {
            //        DoctorsSchedule schedule = new()
            //        {
            //            Doctor = new()
            //            {
            //                FirstName = reader["first_name"].ToString(),
            //                LastName = reader["last_name"].ToString(),
            //                PhoneNumber = reader["phone_number"].ToString(),
            //                Speciality = reader["speciality"].ToString()
            //            },
            //            DayOfWeek = reader["day_of_week"].ToString(),
            //            StartTime = reader["start_time"].ToString(),
            //            EndTime = reader["end_time"].ToString()

            //        };

            //        doctorsSchedules.Add(schedule);
            //    }

            //    await reader.CloseAsync();
            //}

            //return doctorsSchedules;
        }

        public async Task<bool> InsertNewSchedule(DoctorsSchedule ds)
        {
            return false;
            //using (SqlConnection connection = new (connectionString))
            //{
            //    SqlCommand command = new("UpdateDoctorSchedule", connection)
            //    {
            //        CommandType = CommandType.StoredProcedure
            //    };

            //    command.Parameters.AddWithValue("@doctor_id", ds.Doctor.DoctorId);
            //    command.Parameters.AddWithValue("@day_of_week", ds.DayOfWeek);
            //    command.Parameters.AddWithValue("@start_time", ds.StartTime);
            //    command.Parameters.AddWithValue("@end_time", ds.EndTime);

            //    await connection.OpenAsync();
            //    try
            //    {
            //        int rowsAffected = await command.ExecuteNonQueryAsync();
            //        return rowsAffected > 0;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    finally
            //    {
            //        await connection.CloseAsync();
            //    }
            //}
        }
    }
}
