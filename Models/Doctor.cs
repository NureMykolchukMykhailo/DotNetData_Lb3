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
        [Column("doctor_id")]
        public int DoctorId { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("total_earnings")]
        public decimal TotalEarnins { get; set; }
    }

    public class SpentByPatient
    {
        [Column("doctor_id")]
        public int DoctorId { get; set; }
        [Column("doctor_name")]
        public string DoctorName { get; set; }
        [Column("speciality")]
        public string Speciality { get; set; }
        [Column("total_spent")]
        public decimal TotalSpent { get; set; }
    }
}
