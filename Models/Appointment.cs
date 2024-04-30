using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetData_Lb3.Models
{
    public class Appointment
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
}
