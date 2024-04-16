using System.ComponentModel.DataAnnotations.Schema;


namespace DotNetData_Lb3.Models
{
    [Table("doctors")]
    public class Doctor
    {
        [Column("doctor_id")]
        public int DoctorId { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("speciality")]
        public string Speciality { get; set; }

        public ICollection<DoctorsSchedule>? Schedules { get; set; }
        public ICollection<PatientDiscount>? Discounts { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " (" + Speciality + ")";
        }
    }

    public class TopEarningDoctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double TotalEarnins { get; set; }
    }

    public class SpentByPatient
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public double TotalSpent { get; set; }
    }
}
