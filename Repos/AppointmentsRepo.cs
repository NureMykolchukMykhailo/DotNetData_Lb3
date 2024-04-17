using DotNetData_Lb3.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetData_Lb3.Repos
{
    public class AppointmentsRepo
    {
        DatabaseContext context;
        public AppointmentsRepo(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            return await context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).ToListAsync();

            //List<Appointment> appointments = new();

            //using (SqlConnection connection = new(connectionString))
            //{
            //    string sqlQuery = "SELECT a.appointment_type as a_t, a.appointment_date as a_d, a.appointment_price as a_p, " +
            //        "d.first_name as d_f, d.last_name as d_l, d.phone_number as d_p, d.speciality as d_s, " +
            //        "a.first_name as p_f, a.last_name as p_l, a.phone_number as p_p, a.age as p_a " +
            //        "FROM appointments as a INNER JOIN doctors as d ON a.doctor_id = d.doctor_id " +
            //        "INNER JOIN patients as a ON a.patient_id = a.patient_id; ";

            //    SqlCommand command = new(sqlQuery, connection);

            //    await connection.OpenAsync();
            //    SqlDataReader reader = await command.ExecuteReaderAsync();

            //    while (await reader.ReadAsync())
            //    {
            //        Appointment appointment = new()
            //        {
            //            AppointmentType = reader["a_t"].ToString(),
            //            AppointmentDate = (DateTime) reader["a_d"],
            //            AppointmentPrice = double.Parse(reader["a_p"].ToString()),
            //            Doctor = new()
            //            {
            //                FirstName = reader["d_f"].ToString(),
            //                LastName = reader["d_l"].ToString(),
            //                PhoneNumber = reader["d_p"].ToString(),
            //                Speciality = reader["d_s"].ToString(),
            //            },
            //            Patient = new()
            //            {
            //                FirstName = reader["p_f"].ToString(),
            //                LastName = reader["p_l"].ToString(),
            //                PhoneNumber = reader["p_p"].ToString(),
            //                Age = reader["p_a"].ToString(),
            //            }
                        
            //        };

            //        appointments.Add(appointment);
            //    }

            //    await reader.CloseAsync();
            //}

            //return appointments;
        }

        public async Task<bool> InsertNewAppointment(Appointment a)
        {
            await context.Appointments.AddAsync(a);
            return await context.SaveChangesAsync() > 0;
            //using (SqlConnection connection = new(connectionString))
            //{
            //    string sqlQuery = "INSERT INTO appointments VALUES " +
            //        "(@appointment_type, @appointment_date, @appointment_price, @doctor_id, @patient_id)";

            //    SqlCommand command = new SqlCommand(sqlQuery, connection);

            //    command.Parameters.AddWithValue("@appointment_type", a.AppointmentType);
            //    command.Parameters.AddWithValue("@appointment_date", a.AppointmentDate);
            //    command.Parameters.AddWithValue("@appointment_price", a.AppointmentPrice);
            //    command.Parameters.AddWithValue("@doctor_id", a.DoctorId);
            //    command.Parameters.AddWithValue("@patient_id", a.PatientId);

            //    await connection.OpenAsync();
            //    int rowsAffected = await command.ExecuteNonQueryAsync();

            //    return rowsAffected > 0;
            //}
        }
    }
}
