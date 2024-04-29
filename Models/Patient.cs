using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DotNetData_Lb3.Models
{
    public class Patient
    {
        [BsonId]
        public ObjectId PatientId { get; set; }
        [BsonElement("first_name")]
        public string FirstName { get; set; }
        [BsonElement("last_name")]
        public string LastName { get; set; }
        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }
        [BsonElement("age")]
        public string Age { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " (" + Age + ")";
        }
    }
}
