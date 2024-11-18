using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIParcelTracking
{
    public class Parcel
    {
        [BsonId]
        public string? Id { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public double? Weight { get; set; }
        public string? Desc { get; set; }
    }
}
