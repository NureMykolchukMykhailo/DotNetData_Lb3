﻿using DotNetData_Lb3.Models;
using System.Data.SqlClient;

namespace DotNetData_Lb3.Repos
{
    public class PatientsRepo
    {
        private readonly string connectionString;

        public PatientsRepo()
        {
            connectionString = Environment.GetEnvironmentVariable("DbConnection");
        }

        public async Task<List<Patient>> GetPatients()
        {
            List<Patient> patients = new();

            using (SqlConnection connection = new(connectionString))
            {
                string sqlQuery = "SELECT patient_id, first_name, last_name, phone_number, age FROM patients";
                SqlCommand command = new(sqlQuery, connection);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Patient patient = new Patient
                    {
                        PatientId = Convert.ToInt32(reader["patient_id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        PhoneNumber = reader["phone_number"].ToString(),
                        Age = reader["age"].ToString()
                    };

                    patients.Add(patient);
                }

                await reader.CloseAsync();
            }

            return patients;
        }

        public async Task<bool> InsertNewPatient(Patient p)
        {
            using (SqlConnection connection = new(connectionString))
            {
                string sqlQuery = "INSERT INTO patients VALUES(@first_name, @last_name, @age, @phone_number)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@first_name", p.FirstName);
                command.Parameters.AddWithValue("@last_name", p.LastName);
                command.Parameters.AddWithValue("@phone_number", p.PhoneNumber);
                command.Parameters.AddWithValue("@age", p.Age);

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }
}
