namespace DotNetData_Lb3.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string AppointmentType { get; set; }
        public DateTime AppointmentDate { get; set; }
        public double AppointmentPrice { get; set; }
        public Doctor Doctor { get; set; }
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
