using DotNetData_Lb3.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetData_Lb3.Repos
{
    public class DoctorsRepo
    {
        DatabaseContext context;
        public DoctorsRepo(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            return await context.Doctors.ToListAsync();

            //List<Doctor> doctors = new();

            //using (SqlConnection connection = new(connectionString))
            //{
            //    string sqlQuery = "SELECT doctor_id, first_name, last_name, phone_number, speciality FROM doctors";
            //    SqlCommand command = new(sqlQuery, connection);

            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Doctor doctor = new Doctor
            //        {
            //            DoctorId = Convert.ToInt32(reader["doctor_id"]),
            //            FirstName = reader["first_name"].ToString(),
            //            LastName = reader["last_name"].ToString(),
            //            PhoneNumber = reader["phone_number"].ToString(),
            //            Speciality = reader["speciality"].ToString()
            //        };

            //        doctors.Add(doctor);
            //    }

            //    reader.Close();
            //}

            //return doctors;
        }

        public async Task<bool> InsertNewDoctor(Doctor d)
        {
           
            await context.Doctors.AddAsync(d);
            return await context.SaveChangesAsync() > 0;
            //using (SqlConnection connection = new(connectionString))
            //{
            //    string sqlQuery = "INSERT INTO doctors VALUES(@first_name, @last_name, @phone_number, @speciality)";

            //    SqlCommand command = new SqlCommand(sqlQuery, connection);

            //    command.Parameters.AddWithValue("@first_name", d.FirstName);
            //    command.Parameters.AddWithValue("@last_name", d.LastName);
            //    command.Parameters.AddWithValue("@phone_number", d.PhoneNumber);
            //    command.Parameters.AddWithValue("@speciality", d.Speciality);

            //    await connection.OpenAsync();
            //    int rowsAffected = await command.ExecuteNonQueryAsync();

            //    return rowsAffected > 0;
            //}
        }
    }
}
