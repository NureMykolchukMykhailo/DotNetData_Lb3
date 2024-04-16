using System.ComponentModel.DataAnnotations.Schema;


namespace DotNetData_Lb3.Models
{
    [Table("doctor_schedule")]
    public class DoctorsSchedule
    {
        [Column("schedule_id")]
        public int ScheduleId { get; set; }
        [Column("doctor_id")]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        [Column("day_of_week")]
        public string DayOfWeek { get; set; }
        [Column("start_time")]
        public TimeSpan StartTime { get; set; }
        [Column("end_time")]
        public TimeSpan EndTime { get; set; }

    }
}
