using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetData_Lb3.Models
{
    public class Appointment : BsonDocument
    {
        [BsonId]
        public ObjectId AppointmentId { get; set; }
        [BsonElement("appointment_type")]
        public string AppointmentType { get; set; }
        [BsonElement("appointment_date")]
        public DateTime AppointmentDate { get; set; }
        [BsonElement("appointment_price")]
        public decimal AppointmentPrice { get; set; }
        [BsonElement("doctor_id")]
        public ObjectId DoctorId { get; set; }
        [BsonElement("patient_id")]
        public ObjectId PatientId { get; set; }
    }

    public class AppointmentFull
    {
        public ObjectId AppointmentId { get; set; }
        public string AppointmentType { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal AppointmentPrice { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
