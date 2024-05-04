using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DotNetData_Lb3.Models
{
    public class Location
    {
        [BsonElement("type")] 
        public string Type { get; set; }
        [BsonElement("coordinates")]
        List<double> Coordinates { get; set; }
    }
}
