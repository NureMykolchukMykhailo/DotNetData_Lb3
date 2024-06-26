﻿using DotNetData_Lb3.Models;
using System.Data.SqlClient;

namespace DotNetData_Lb3.Repos
{
    public class FunctionsRepo
    {
        private readonly string connectionString;

        public FunctionsRepo()
        {
            connectionString = Environment.GetEnvironmentVariable("DbConnection");
        }

        public async Task<List<TopEarningDoctor>> GetTopEarningDoctors(DateTime date)
        {
            List<TopEarningDoctor> topEarningDoctors = new();

            using (SqlConnection connection = new(connectionString))
            {
                string sqlQuery = "SELECT * FROM FindTopEarningDoctor(@date);";
                SqlCommand command = new(sqlQuery, connection);

                string date2 = date.ToString("yyyy-MM-dd");
                command.Parameters.AddWithValue("@date", date2);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    TopEarningDoctor topEarningDoctor = new()
                    {
                        DoctorId = Convert.ToInt32(reader["doctor_id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        TotalEarnins = Convert.ToDouble(reader["total_earnings"])
                    };

                    topEarningDoctors.Add(topEarningDoctor);
                }

                await reader.CloseAsync();
            }

            return topEarningDoctors;
        }

        public async Task<List<SpentByPatient>> GetSpentByPatient(string phoneNumber)
        {
            List<SpentByPatient> spentByPatient = new();

            using (SqlConnection connection = new(connectionString))
            {
                string sqlQuery = "SELECT * FROM GetSpentByPatient(@phone_number);";
                SqlCommand command = new(sqlQuery, connection);

                command.Parameters.AddWithValue("@phone_number", phoneNumber);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    SpentByPatient spent = new()
                    {
                        DoctorId = Convert.ToInt32(reader["doctor_id"]),
                        DoctorName = reader["doctor_name"].ToString(),
                        Speciality = reader["speciality"].ToString(),
                        TotalSpent = Convert.ToDouble(reader["total_spent"])
                    };

                    spentByPatient.Add(spent);
                }

                await reader.CloseAsync();
            }

            return spentByPatient;
        }

        public async Task<string?> RemoveSubstring(string? str, int start, int remove)
        {
            using (SqlConnection connection = new(connectionString))
            {
                string sqlQuery = $"SELECT dbo.RemoveSubstring('{str}', @startPosition, " +
                    "@lengthToRemove) as Result;";
                SqlCommand command = new(sqlQuery, connection);

                command.Parameters.AddWithValue("@startPosition", start);
                command.Parameters.AddWithValue("@lengthToRemove", remove);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                string? res = "";
                while (await reader.ReadAsync())
                {
                    res = reader["Result"].ToString();
                }
                await reader.CloseAsync();
                return res;
            }
        }

    }
}
