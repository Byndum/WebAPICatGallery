using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIParcelTracking
{
    public class Parcel
    {
        [BsonId]
        public string? Id { get; set; }
        public int? Lat { get; set; }
        public int? Long { get; set; }
        public double? Weight { get; set; }
        public string? Desc { get; set; }
    }
}
