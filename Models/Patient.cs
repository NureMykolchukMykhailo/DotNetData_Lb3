namespace DotNetData_Lb3.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Age { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " (" + Age + ")";
        }
    }
}
