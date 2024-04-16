using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetData_Lb3.Models
{
    [Table("PatientDiscounts")]
    public class PatientDiscount
    {
        [Column("patientID")]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [Column("doctorID")]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        [Column("discount")]
        public float Discount { get; set; }
    }
}
