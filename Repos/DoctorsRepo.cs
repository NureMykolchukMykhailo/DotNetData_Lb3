using DotNetData_Lb3.Models;
using System.Data.SqlClient;

namespace DotNetData_Lb3.Repos
{
    public class DoctorsRepo
    {
        private readonly string connectionString;

        public DoctorsRepo()
        {
            connectionString = Environment.GetEnvironmentVariable("DbConnection");
        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new();

            using (SqlConnection connection = new(connectionString))
            {
                string sqlQuery = "SELECT doctor_id, first_name, last_name, phone_number, speciality FROM doctors";
                SqlCommand command = new(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Doctor doctor = new Doctor
                    {
                        DoctorId = Convert.ToInt32(reader["doctor_id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        PhoneNumber = reader["phone_number"].ToString(),
                        Speciality = reader["speciality"].ToString()
                    };

                    doctors.Add(doctor);
                }

                reader.Close();
            }

            return doctors;
        }

        public async Task<bool> InsertNewDoctor(Doctor d)
        {
            using (SqlConnection connection = new(connectionString))
            {
                string sqlQuery = "INSERT INTO doctors VALUES(@first_name, @last_name, @phone_number, @speciality)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@first_name", d.FirstName);
                command.Parameters.AddWithValue("@last_name", d.LastName);
                command.Parameters.AddWithValue("@phone_number", d.PhoneNumber);
                command.Parameters.AddWithValue("@speciality", d.Speciality);

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }
}
