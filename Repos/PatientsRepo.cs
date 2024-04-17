using DotNetData_Lb3.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetData_Lb3.Repos
{
    public class PatientsRepo
    {
        DatabaseContext context;

        public PatientsRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Patient>> GetPatients()
        {
            return await context.Patients.ToListAsync();
            //List<Patient> patients = new();

            //using (SqlConnection connection = new(connectionString))
            //{
            //    string sqlQuery = "SELECT patient_id, first_name, last_name, phone_number, age FROM patients";
            //    SqlCommand command = new(sqlQuery, connection);

            //    await connection.OpenAsync();
            //    SqlDataReader reader = await command.ExecuteReaderAsync();

            //    while (await reader.ReadAsync())
            //    {
            //        Patient patient = new Patient
            //        {
            //            PatientId = Convert.ToInt32(reader["patient_id"]),
            //            FirstName = reader["first_name"].ToString(),
            //            LastName = reader["last_name"].ToString(),
            //            PhoneNumber = reader["phone_number"].ToString(),
            //            Age = reader["age"].ToString()
            //        };

            //        patients.Add(patient);
            //    }

            //    await reader.CloseAsync();
            //}

            //return patients;
        }

        public async Task<bool> InsertNewPatient(Patient p)
        {
            await context.Patients.AddAsync(p);
            return await context.SaveChangesAsync() > 0;
            //using (SqlConnection connection = new(connectionString))
            //{
            //    string sqlQuery = "INSERT INTO patients VALUES(@first_name, @last_name, @age, @phone_number)";

            //    SqlCommand command = new SqlCommand(sqlQuery, connection);

            //    command.Parameters.AddWithValue("@first_name", p.FirstName);
            //    command.Parameters.AddWithValue("@last_name", p.LastName);
            //    command.Parameters.AddWithValue("@phone_number", p.PhoneNumber);
            //    command.Parameters.AddWithValue("@age", p.Age);

            //    await connection.OpenAsync();
            //    int rowsAffected = await command.ExecuteNonQueryAsync();

            //    return rowsAffected > 0;
            //}
        }
    }
}
