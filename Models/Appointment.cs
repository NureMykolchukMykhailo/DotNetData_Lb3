using System.ComponentModel.DataAnnotations.Schema;


namespace DotNetData_Lb3.Models
{
    [Table("appointments")]
    public class Appointment
    {
        [Column("appointment_id")]
        public int AppointmentId { get; set; }

        [Column("appointment_type")]
        public string AppointmentType { get; set; }

        [Column("appointment_date")]
        public DateTime AppointmentDate { get; set; }

        [Column("appointment_price")]
        public decimal AppointmentPrice { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

        [Column("patient_id")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }

    public class AppointmentAdding
    {
        public int AppointmentId { get; set; }
        public string AppointmentType { get; set; }
        public DateTime AppointmentDate { get; set; }
        public double AppointmentPrice { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

    }
}
