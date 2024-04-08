namespace DotNetData_Lb3.Models
{
    public class DoctorsSchedule
    {
        public int ScheduleId { get; set; }
        public Doctor Doctor { get; set; }
        public string DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}
