using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DotNetData_Lb3.Models
{
    public class Doctor
    {
        [BsonId]
        public ObjectId DoctorId { get; set; }
        [BsonElement("first_name")]
        public string FirstName { get; set; }
        [BsonElement("last_name")]
        public string LastName { get; set; }
        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }
        [BsonElement("speciality")]
        public string Speciality { get; set; }
        [BsonElement("schedule")]
        public List<Schedule>? Schedule { get; set; }
        [BsonElement("location")]
        public Location? Location { get; set; }
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
        public decimal TotalEarnins { get; set; }
    }

    public class SpentByPatient
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
