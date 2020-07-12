using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace coretex_be.Models
{
    public class VehicleModel
    {
        [BsonElement("Id")]
        public string Id { get; set; }
        [BsonElement("NumberPlate")]
        public string NumberPlate { get; set; }
        [BsonElement("Speed")]
        public decimal Speed { get; set; }
        [BsonElement("Latitude")]
        public double Latitude { get; set; }
        [BsonElement("Longitude")]
        public double Longitude { get; set; }
    }
}