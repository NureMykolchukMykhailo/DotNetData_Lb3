using System.ComponentModel.DataAnnotations.Schema;


namespace DotNetData_Lb3.Models
{
    [Table("patients")]
    public class Patient
    {
        [Column("patient_id")]
        public int PatientId { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("age")]
        public string Age { get; set; }
        public ICollection<PatientDiscount>? Discounts { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " (" + Age + ")";
        }
    }
}
