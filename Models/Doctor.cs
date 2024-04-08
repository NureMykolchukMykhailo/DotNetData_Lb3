namespace DotNetData_Lb3.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Speciality { get; set; }

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
